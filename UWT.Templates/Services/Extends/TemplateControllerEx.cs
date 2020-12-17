using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Services.Caches;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 通用模板扩展方法
    /// </summary>
    public static class TemplateControllerEx
    {
        internal const string HandlersConstKey = "Handlers";
        internal const string FiltersConstKey = "Filters";
        internal const string TempateViewDataKey = "_template_key";
        internal const string EmptyListKey = "_empty_list_key";
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static ITemplateController SetTitle(this ITemplateController controller, string title)
        {
            if (title == null)
            {
                return controller;
            }
            if (controller.GetController().ViewData.ContainsKey("SubTitleFormat"))
            {
                controller.GetController().ViewBag.Title = string.Format(controller.GetController().ViewBag.SubTitleFormat, title);
            }
            else
            {
                controller.GetController().ViewBag.Title = title;
            }
            return controller;
        }
        /// <summary>
        /// 初始化标题格式化<br/>
        /// 本格式化不影响Layout中的标题格式化，属于下级格式化
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="subTitleFormat">标题格式</param>
        /// <returns></returns>
        public static ITemplateController InitTitleFormat(this ITemplateController controller, string subTitleFormat)
        {
            controller.GetController().ViewBag.SubTitleFormat = subTitleFormat;
            return controller;
        }
        /// <summary>
        /// 获得控制器
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <returns></returns>
        public static Controller GetController(this ITemplateController controller)
        {
            return controller as Controller;
        }
        /// <summary>
        /// 添加导航链接
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public static ITemplateController AddHandler(this ITemplateController controller, string title, string url, Type controllerType = null)
        {
            controller.AddViewDataList(HandlersConstKey, controller.CreateHandleModel(HandleModel.BuildNavigate(title, url), controllerType));
            return controller;
        }
        internal static void AddViewDataList<TItem>(this ITemplateController templateController, string key, TItem listItem)
        {
            List<TItem> addList = null;
            Controller controller = templateController.GetController();
            if (controller.ViewData.ContainsKey(key))
            {
                addList = controller.ViewData[key] as List<TItem>;
            }
            else
            {
                controller.ViewData[key] = addList = new List<TItem>();
            }
            addList.Add(listItem);
        }
        internal static void ChangeViewData<TData>(this ITemplateController templateController, string key, TData data)
        {
            Controller controller = templateController.GetController();
            controller.ViewData[key] = data;
        }
        internal static HandleModel CreateHandleModel(this ITemplateController templateController, HandleModel model, Type type = null)
        {
            if (type == null)
            {
                type = templateController.GetType();
            }
            string target = (string)model.Target;
            target = ModelCache.RechangeUrl(type, target);
            if (target.StartsWith("."))
            {
                var controler = templateController.GetController();
                var index = controler.Request.Path.Value.LastIndexOf('/');
                if (index != -1)
                {
                    target = controler.Request.Path.Value.Substring(0, index + 1) + target.Substring(1);
                }
            }
            model.Target = target;
            if (string.IsNullOrEmpty(model.Class))
            {
                model.Class = HandleModel.ClassBtnDefault;
            }
            return model;
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="listTemplate"></param>
        /// <param name="action"></param>
        public static void UsingDb(this ITemplateController listTemplate, Action<LinqToDB.Data.DataConnection> action)
        {
            using (var db = UWT.Templates.Models.Database.DbCreator.CreateDb())
            {
                action(db);
            }
        }
        /// <summary>
        /// 打开数据库并使用数据表
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="templateController"></param>
        /// <param name="action"></param>
        public static void UsingDb<TTable>(this ITemplateController templateController, Action<LinqToDB.Data.DataConnection, LinqToDB.ITable<TTable>> action)
            where TTable : class, IDbTableBase
        {
            using (var db = Models.Database.DbCreator.CreateDb())
            {
                action(db, db.UwtGetTable<TTable>());
            }
        }
        /// <summary>
        /// 获得数据库对象<br/>
        /// 需要自己using (var db = this.GetDB())
        /// </summary>
        /// <param name="templateController"></param>
        /// <returns></returns>
        public static LinqToDB.Data.DataConnection GetDB([System.Diagnostics.CodeAnalysis.AllowNull] this ITemplateController templateController)
        {
            return Models.Database.DbCreator.CreateDb();
        }

        /// <summary>
        /// 获得数据库对象<br/>
        /// 需要自己using (var db = TemplateControllerEx.GetDB())
        /// </summary>
        /// <returns></returns>
        public static LinqToDB.Data.DataConnection GetDB()
        {
            return Models.Database.DbCreator.CreateDb();
        }

        /// <summary>
        /// 添加额外的JS
        /// </summary>
        /// <param name="templateController">模板控制器</param>
        /// <param name="jsPath">js路径</param>
        /// <param name="appendAttributes">附加属性</param>
        /// <returns></returns>
        public static ITemplateController AppendJS(this ITemplateController templateController, string jsPath, Dictionary<string, string> appendAttributes = null)
        {
            var map = new Dictionary<string, string>();
            map.Add(HtmlConst.SRC, jsPath);
            map.Add(HtmlConst.TYPE, HtmlConst.TYPE_JS);
            if (appendAttributes != null)
            {
                foreach (var item in appendAttributes)
                {
                    if (map.ContainsKey(item.Key))
                    {
                        map.Remove(item.Key);
                    }
                    map.Add(item.Key, item.Value);
                }
            }
            AppendObjectToViewData(templateController, AppendJsList, HtmlConst.SCRIPT, map);
            return templateController;
        }

        /// <summary>
        /// 添加额外的CSS
        /// </summary>
        /// <param name="templateController">模板控制器</param>
        /// <param name="cssPath">css路径</param>
        /// <param name="appendAttributes">js路径</param>
        /// <returns></returns>
        public static ITemplateController AppendCSS(this ITemplateController templateController, string cssPath, Dictionary<string, string> appendAttributes = null)
        {
            var map = new Dictionary<string, string>();
            map.Add(HtmlConst.HREF, cssPath);
            map.Add(HtmlConst.REL, HtmlConst.STYLESHEET);
            map.Add(HtmlConst.TYPE, HtmlConst.TYPE_CSS);
            if (appendAttributes != null)
            {
                foreach (var item in appendAttributes)
                {
                    if (map.ContainsKey(item.Key))
                    {
                        map.Remove(item.Key);
                    }
                    map.Add(item.Key, item.Value);
                }
            }
            AppendObjectToViewData(templateController, AppendCssList, HtmlConst.LINK, map);
            return templateController;
        }
        internal const string AppendCssList = "__append_css_list__";
        internal const string AppendJsList = "__append_js_list__";
        private static void AppendObjectToViewData(ITemplateController templateController, string key, string tagName, Dictionary<string, string> map)
        {
            var vd = templateController.GetController().ViewData;
            List<string> tags = null;
            if (vd.ContainsKey(key))
            {
                tags = vd[key] as List<string>;
            }
            else
            {
                vd[key] = tags = new List<string>();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            sb.Append(tagName);
            foreach (var item in map)
            {
                sb.AppendFormat(" {0}=\"{1}\"", item.Key, item.Value);
            }
            sb.Append("></");
            sb.Append(tagName);
            sb.Append(">");
            tags.Add(sb.ToString());
        }
    }
}
