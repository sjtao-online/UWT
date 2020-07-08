using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Models.Templates.FormTrees;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Users.MenuGroups
{
    /// <summary>
    /// �˵��������
    /// </summary>
    [AuthUser]
    public class MenuGroupsController : Controller
        , IListToPage<IDbMenuGroupTable, MenuGroupListItemModel>
        , IFormToPage<MenuGroupAddModel>
        , IFormToPage<MenuGroupModifyModel>
        , IFormTreeToPage<MenuGroupModifyTreeModel>
    {
        /// <summary>
        /// ���б�
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Index()
        {
            this.ActionLog();
            this.AddHandler("���", ".Add");
            return this.ListResult(m => new MenuGroupListItemModel()
            {
                Name = m.Name,
                Id = m.Id,
                Desc = m.Desc,
                PageCount = m.PageCount,
                AuthCount = m.AuthCount
            }, m => m.Valid).View();
        }

        /// <summary>
        /// ���ҳ��
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<MenuGroupAddModel>().View();
        }

        /// <summary>
        /// ���API
        /// </summary>
        /// <param name="model">���ģ��</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<object> AddModel([FromBody]MenuGroupAddModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<MenuGroupAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbMenuGroupTable>();
                table.UwtInsertWithInt32(new Dictionary<string, object>()
                {
                    [nameof(IDbMenuGroupTable.Name)] = model.Name,
                    [nameof(IDbMenuGroupTable.Desc)] = model.Desc,
                    [nameof(IDbMenuGroupTable.PageCount)] = 0,
                    [nameof(IDbMenuGroupTable.AuthCount)] = 0
                });
            });
            return this.Success();
        }

        /// <summary>
        /// �޸�ҳ��
        /// </summary>
        /// <param name="id">�޸�Id</param>
        /// <returns></returns>
        public virtual IActionResult Modify(int id)
        {
            this.ActionLog();
            MenuGroupModifyModel modify = null;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbMenuGroupTable>();
                var q = from it in table
                        where it.Id == id && it.Valid
                        select new MenuGroupModifyModel
                        {
                            Id = it.Id,
                            Name = it.Name,
                            Desc = it.Desc
                        };
                if (q.Count() != 0)
                {
                    modify = q.First();
                }
            });
            if (modify == null)
            {
                return this.ItemNotFound();
            }
            return this.FormResult<MenuGroupModifyModel>(modify).View();
        }

        /// <summary>
        /// �޸Ľӿ�
        /// </summary>
        /// <param name="model">�޸�ģ��</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody]MenuGroupModifyModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<MenuGroupModifyModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbMenuGroupTable>();
                table.UwtUpdate(model.Id, new Dictionary<string, object>()
                {
                    [nameof(IDbMenuGroupTable.Name)] = model.Name,
                    [nameof(IDbMenuGroupTable.Desc)] = model.Desc
                });
            });
            return this.Success();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IActionResult ModifyTree(int id)
        {
            this.ActionLog();
            var list = this.GetTreeModelList<MenuGroupModifyTreeModel, IDbMenuGroupItemTable, int>(m => new MenuGroupModifyTreeModel()
            {
                Id = m.Id,
                Title = m.Title,
                Desc = m.Desc,
                Icon = m.Icon,
                Url = m.Url
            }, m => m.GroupId == id && m.Pid == 0 && m.Valid, m => r => m.Id == r.Pid, m => m.Index);
            return this.FormTreeResult(id, new Dictionary<string, object>() { ["Title"] = "�޸Ĳ˵���", ["Url"] = "/Home/Index" }, "Title", list).View();
        }


        /// <summary>
        /// �������ӿ�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object ModifyTreeModel([FromBody]ModifyTreeModelTemplate<MenuGroupModifyTreeModel> model)
        {
            this.ActionLog();
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbMenuGroupItemTable>();
                this.UpdateTreeDb2<IDbMenuGroupItemTable, MenuGroupModifyTreeModel>((model, templateId, parentId, index) =>
                {
                    return new Dictionary<string, object>
                    {
                        [nameof(IDbMenuGroupItemTable.Title)] = model.Title,
                        [nameof(IDbMenuGroupItemTable.Desc)] = model.Desc,
                        [nameof(IDbMenuGroupItemTable.GroupId)] = templateId,
                        [nameof(IDbMenuGroupItemTable.Pid)] = parentId,
                        [nameof(IDbMenuGroupItemTable.Url)] = model.Url,
                        [nameof(IDbMenuGroupItemTable.Valid)] = true,
                        [nameof(IDbMenuGroupItemTable.Index)] = index,
                        [nameof(IDbMenuGroupItemTable.Icon)] = model.Icon,
                        [nameof(IDbMenuGroupItemTable.Tooltip)] = model.Tooltip ?? ""
                    };
                }, (model, templateId, parentId, index) =>
                {
                    return new Dictionary<string, object>
                    {
                        [nameof(IDbMenuGroupItemTable.Title)] = model.Title,
                        [nameof(IDbMenuGroupItemTable.Desc)] = model.Desc,
                        [nameof(IDbMenuGroupItemTable.GroupId)] = templateId,
                        [nameof(IDbMenuGroupItemTable.Pid)] = parentId,
                        [nameof(IDbMenuGroupItemTable.Url)] = model.Url,
                        [nameof(IDbMenuGroupItemTable.Valid)] = true,
                        [nameof(IDbMenuGroupItemTable.Index)] = index,
                        [nameof(IDbMenuGroupItemTable.Icon)] = model.Icon,
                        [nameof(IDbMenuGroupItemTable.Tooltip)] = model.Tooltip ?? ""
                    };
                }, model.TreeModel, model.Id, 0, table);
                foreach (var item in model.DelIdList)
                {
                    table.UwtUpdate(item, new Dictionary<string, object>()
                    {
                       [nameof(IDbMenuGroupItemTable.Valid)] = false
                    });
                }
            });
            return this.Success();
        }
        /// <summary>
        /// ɾ���˵���
        /// </summary>
        /// <param name="id">�˵���Id</param>
        /// <returns></returns>
        [HttpPost]
        public virtual object Del(int id)
        {
            bool notfound = false;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbMenuGroupTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbMenuGroupTable.Valid)] = false
                });
            });
            return notfound ? this.Error(ErrorCode.Item_NotFound) : this.Success();
        }
    }

    /// <summary>
    /// ������ģ��
    /// </summary>
    /// <typeparam name="TTreeModel"></typeparam>
    public class ModifyTreeModelTemplate<TTreeModel>
    {
        /// <summary>
        /// ���
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ɾ��Id�б�
        /// </summary>
        public List<int> DelIdList { get; set; }
        /// <summary>
        /// ��ģ��
        /// </summary>
        public List<TTreeModel> TreeModel { get; set; }
    }
    /// <summary>
    /// �˵���Formģ��
    /// </summary>
#pragma warning disable CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
    [FormModel("/MenuGroups/ModifyTreeModel")]
    public class MenuGroupModifyTreeModel : FormTreeModelBasic<MenuGroupModifyTreeModel>
    {
        [FormItem("����")]
        [FormItems.Text]
        public string Title { get; set; }
        [FormItem("��ͣ��ʾ")]
        [FormItems.Text]
        public string Tooltip { get; set; }
        [FormItem("��ע")]
        [FormItems.Text]
        public string Desc { get; set; }
        [FormItem("ͼ��", FormItemType.SimpleSelect)]
        [FormItems.SimpleSelect(0, Builder = typeof(IconSimpleSelectorBuilder))]
        public string Icon { get; set; }
        [FormItem("Url", FormItemType.ChooseId)]
        //[FormItems.Text]
        [FormItems.ChooseIdFromTable("${ModulesTableName}", NameColumnName = "url", Where = "type = 'page'")]
        public int Url { get; set; }
    }
#pragma warning restore CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
    /// <summary>
    /// ͼ��ѡ����
    /// </summary>
    public class IconSimpleSelectorBuilder : SelectItemBuilderBasic
    {
        /// <summary>
        /// ͼ���б�
        /// </summary>
        public static List<NameKeyModel> IconList { get; set; }
#pragma warning disable CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
        public override List<NameKeyModel> BuildItemList()
#pragma warning restore CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա�� XML ע��
        {
            if (IconList == null)
            {
                return new List<NameKeyModel>()
                {
                    new NameKeyModel()
                    {
                        Key = "",
                        Name = "��"
                    }
                };
            }
            return IconList;
        }
    }
}