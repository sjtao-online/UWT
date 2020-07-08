using LinqToDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Models.Templates.FormTrees;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 表单树型扩展
    /// </summary>
    public static class FormTreePageEx
    {
        /// <summary>
        /// 表单树型页面
        /// </summary>
        /// <typeparam name="TFormTreeModel">表单树模型</typeparam>
        /// <param name="page">控制器</param>
        /// <param name="parentId">父Id</param>
        /// <param name="newDefault">新建默认对象</param>
        /// <param name="titleProperyName">标题属性名</param>
        /// <param name="model">模型对象</param>
        /// <returns></returns>
        public static IPageResult FormTreeResult<TFormTreeModel>(this IFormTreeToPage<TFormTreeModel> page, int parentId, Dictionary<string, object> newDefault, string titleProperyName, List<TFormTreeModel> model = null)
            where TFormTreeModel : FormTreeModelBasic<TFormTreeModel>
        {
            var controller = page.GetController();
            var formTreeModel = new FormViewModel()
            {
                Item = model,
                FormModel = ModelCache.GetModelFromType(typeof(TFormTreeModel), ModelCache.FormModel)
            };
            controller.ViewBag.ParentId = parentId;
            controller.ViewBag.FileManagerOptional = ServiceCollectionEx.FileManagerOptional;
            controller.ViewBag.ChooseId2Name = ServiceCollectionEx.ChooseId2Name;
            controller.ViewBag.ChooseIds2Names = ServiceCollectionEx.ChooseIds2Names;
            controller.ViewBag.ModelType = typeof(TFormTreeModel);
            controller.ViewData.Model = formTreeModel;
            controller.ViewBag.NewDefault = newDefault;
            controller.ViewBag.TitlePropertyName = titleProperyName;
            return PageTemplateKeyConst.GetPageResult(controller, PageTemplateKeyConst.TemplateFormTreeKey);
        }
        private static void FillResult<TFormTreeModel, TDbTreeTableModel, TOrder>(ref List<TFormTreeModel> result, ITable<TDbTreeTableModel> table,
            Expression<Func<TDbTreeTableModel, TFormTreeModel>> selector,
            Func<TFormTreeModel, Expression<Func<TDbTreeTableModel, bool>>> otherWhereBuilder,
            Expression<Func<TDbTreeTableModel, TOrder>> order)
            where TFormTreeModel : FormTreeModelBasic<TFormTreeModel>
            where TDbTreeTableModel : class
        {
            foreach (var item in result)
            {
                var list = table.Where(otherWhereBuilder(item)).OrderBy(order).Select(selector).ToList();
                FillResult(ref list, table, selector, otherWhereBuilder, order);
                item.Children = list;
            }
        }
        /// <summary>
        /// 获得模型树
        /// </summary>
        /// <typeparam name="TFormTreeModel"></typeparam>
        /// <typeparam name="TDbTreeTableModel"></typeparam>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">控制器</param>
        /// <param name="selector">选择器</param>
        /// <param name="topWhere">顶层条件</param>
        /// <param name="otherWhereBuilder">其它层条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public static List<TFormTreeModel> GetTreeModelList<TFormTreeModel, TDbTreeTableModel, TOrder>(this IFormTreeToPage<TFormTreeModel> page, 
            Expression<Func<TDbTreeTableModel, TFormTreeModel>> selector, 
            Expression<Func<TDbTreeTableModel, bool>> topWhere,
            Func<TFormTreeModel, Expression<Func<TDbTreeTableModel, bool>>> otherWhereBuilder,
            Expression<Func<TDbTreeTableModel, TOrder>> order)
            where TFormTreeModel : FormTreeModelBasic<TFormTreeModel>
            where TDbTreeTableModel : class
        {
            List<TFormTreeModel> result = null;
            page.UsingDb(db =>
            {
                var table = db.UwtGetTable<TDbTreeTableModel>();
                result = table.Where(topWhere).OrderBy(order).Select(selector).ToList();
                FillResult(ref result, table, selector, otherWhereBuilder, order);
            });
            return result;
        }
        /// <summary>
        /// 更新树2 用于已经打开数据库
        /// </summary>
        /// <typeparam name="TTable">表</typeparam>
        /// <typeparam name="TTreeModel">树模型</typeparam>
        /// <param name="page">控制器</param>
        /// <param name="update">更新</param>
        /// <param name="insert">插入</param>
        /// <param name="trees">树</param>
        /// <param name="tempateId">模板Id</param>
        /// <param name="parentId">父Id</param>
        /// <param name="table">表对象</param>
        public static void UpdateTreeDb2<TTable, TTreeModel>(this ITemplateController page, Func<TTreeModel, int, int, Expression<Func<TTable, TTable>>> update, Func<TTreeModel, int, int, Expression<Func<TTable>>> insert, List<TTreeModel> trees, int tempateId, int parentId, ITable<TTable> table)
            where TTable : class, IDbTableBase
            where TTreeModel : FormTreeModelBasic<TTreeModel>
        {
            foreach (var item in trees)
            {
                int cid = 0;
                if (item.Id > 0)
                {
                    cid = item.Id;
                    table.Update(m => m.Id == item.Id, update(item, tempateId, parentId));
                }
                else
                {
                    cid = table.InsertWithInt32Identity(insert(item, tempateId, parentId));
                }
                if (item.Children != null && item.Children.Count > 0)
                {
                    UpdateTreeDb2(page, update, insert, item.Children, tempateId, cid, table);
                }
            }
        }
        /// <summary>
        /// 更新树2 用于已经打开数据库
        /// </summary>
        /// <typeparam name="TTable">表</typeparam>
        /// <typeparam name="TTreeModel">树模型</typeparam>
        /// <param name="page">控制器</param>
        /// <param name="update">更新</param>
        /// <param name="insert">插入</param>
        /// <param name="trees">树</param>
        /// <param name="tempateId">模板Id</param>
        /// <param name="parentId">父Id</param>
        /// <param name="table">表对象</param>
        public static void UpdateTreeDb2<TTable, TTreeModel>(this ITemplateController page, Func<TTreeModel, int, int, int, Dictionary<string, object>> update, Func<TTreeModel, int, int, int, Dictionary<string, object>> insert, List<TTreeModel> trees, int tempateId, int parentId, ITable<TTable> table)
            where TTable : class, IDbTableBase
            where TTreeModel : FormTreeModelBasic<TTreeModel>
        {
            for (int i = 0; i < trees.Count; i++)
            {
                var item = trees[i];
                int cid = 0;
                if (item.Id > 0)
                {
                    cid = item.Id;
                    table.UwtUpdate(item.Id, update(item, tempateId, parentId, i));
                }
                else
                {
                    cid = table.UwtInsertWithInt32(insert(item, tempateId, parentId, i));
                }
                if (item.Children != null && item.Children.Count > 0)
                {
                    UpdateTreeDb2(page, update, insert, item.Children, tempateId, cid, table);
                }
            }
        }
        /// <summary>
        /// 更新树
        /// </summary>
        /// <typeparam name="TTable">表</typeparam>
        /// <typeparam name="TTreeModel">树模型</typeparam>
        /// <param name="page">控制器</param>
        /// <param name="update">更新</param>
        /// <param name="insert">插入</param>
        /// <param name="trees">树</param>
        /// <param name="templateId">模板Id</param>
        public static void UpdateTreeDb<TTable, TTreeModel>(this ITemplateController page, Func<TTreeModel, int, int, Expression<Func<TTable, TTable>>> update, Func<TTreeModel, int, int, Expression<Func<TTable>>> insert, List<TTreeModel> trees, int templateId)
            where TTable : class, IDbTableBase
            where TTreeModel : FormTreeModelBasic<TTreeModel>
        {
            page.UsingDb(db =>
            {
                var table = db.UwtGetTable<TTable>();
                UpdateTreeDb2<TTable, TTreeModel>(page, update, insert, trees, templateId, 0, table);
            });
        }
    }
}
