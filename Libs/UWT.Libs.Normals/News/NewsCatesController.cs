using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Services.Extends;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UWT.Libs.Normals.News
{
    /// <summary>
    /// 文章类型管理
    /// </summary>
    /// <typeparam name="TDbNewsCateTable"></typeparam>
    public class NewsCatesController<TDbNewsCateTable> : Controller
        , IListToPage<TDbNewsCateTable, NewsCateListItemModel>
        , IFormToPage<NewsCateAddModel>
        , IFormToPage<NewsCateModifyModel>
        where TDbNewsCateTable: class, IDbNewsCateTable, new()
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult Index()
        {
            this.ActionLog();
            this.AddHandler("添加", "/${NewsCatesController}/Add", typeof(NewsCatesController<>));
            return this.ListResult(m => new NewsCateListItemModel()
            {
                Id = m.Id,
                Desc = m.Desc,
                Title = m.Title,
                Icon = m.MiniIcon,
            }, m=> m.Valid).View();
        }



        public object GetParentTree()
        {
            this.ActionLog();
            List<HasChildrenNameKeyModel> all = new List<HasChildrenNameKeyModel>();
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsCateTable>();
                FillNewsCateTableToList(table, ref all, 0);
            });
            return this.Success(all);
        }

        private void FillNewsCateTableToList(ITable<TDbNewsCateTable> table, ref List<HasChildrenNameKeyModel> all, int parentId)
        {
            var qList = from it in table
                        where it.Valid && it.PId == parentId
                        select new
                        {
                            it.Id,
                            it.Title,
                        };
            if (qList.Count() == 0)
            {
                all = null;
            }
            else
            {
                foreach (var item in qList)
                {
                    List<HasChildrenNameKeyModel> children = new List<HasChildrenNameKeyModel>();
                    FillNewsCateTableToList(table, ref children, item.Id);
                    all.Add(new HasChildrenNameKeyModel()
                    {
                        Key = item.Id.ToString(),
                        Name = item.Title,
                        Children = children
                    });
                }
            }
        }

        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<NewsCateAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody]NewsCateAddModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<NewsCateAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsCateTable>();
                table.Insert(() => new TDbNewsCateTable()
                {
                    PId = model.PId,
                    Desc = model.Desc,
                    LargeIcon = model.Icon,
                    MiniIcon = model.Icon,
                    Icon = model.Icon,
                    SubTitle = model.SubTitle,
                    Title = model.Title
                });
            });
            return this.Success();
        }


        [HttpPost]
        public virtual object Del(int id)
        {
            this.ActionLog();
            bool notfound = false;
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsCateTable>();
                var o = (from it in table where it.Id == id select 1).Take(1);
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.Update(m => m.Id == id, m => new TDbNewsCateTable()
                {
                    Valid = false
                });
            });
            return notfound ? this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound) : this.Success();
        }

        public virtual IActionResult Modify(int id)
        {
            this.ActionLog();
            NewsCateModifyModel modify = null;
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsCateTable>();
                var q = (from it in table
                         where it.Id == id && it.Valid
                         select new NewsCateModifyModel
                         {
                             Id = it.Id,
                             Title = it.Title,
                             SubTitle = it.SubTitle,
                             Desc = it.Desc,
                             Icon = it.Icon,
                             LargeIcon = it.LargeIcon,
                             MiniIcon = it.MiniIcon,
                             PId = it.PId
                         }).Take(1);
                if (q.Count() != 0)
                {
                    modify = q.First();
                }
            });
            if (modify == null)
            {
                return this.ItemNotFound();
            }
            return this.FormResult<NewsCateModifyModel>(modify).View();
        }

        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody]NewsCateModifyModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<NewsCateModifyModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsCateTable>();
                table.Update(m => m.Id == model.Id, m => new TDbNewsCateTable()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    MiniIcon = model.MiniIcon,
                    Desc = model.Desc,
                    Icon = model.Icon,
                    LargeIcon = model.LargeIcon,
                    PId = model.PId
                });
            });
            return this.Success();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    /// <summary>
    /// 文章类型数据库模型
    /// </summary>
    public interface IDbNewsCateTable : IDbTableBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        string SubTitle { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 父类型
        /// </summary>
        int PId { get; set; }
        /// <summary>
        /// 大图标
        /// </summary>
        string LargeIcon { get; set; }
        /// <summary>
        /// 中图标
        /// </summary>
        string Icon { get; set; }
        /// <summary>
        /// 小图标
        /// </summary>
        string MiniIcon { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
    [ListViewModel]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class NewsCateListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("图标")]
        public string Icon { get; set; }
        [ListColumn("标题")]
        public string Title { get; set; }
        [ListColumn("说明")]
        public string Desc { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                handles.Add(HandleModel.BuildModify("/${NewsCatesController}/Modify?Id=" + Id));
                return handles;
            }
        }
    }
    [FormModel("/${NewsCatesController}/AddModel")]
    public class NewsCateAddModel
    {
        [FormItem("标题")]
        [FormItems.Text(MaxLength = 255)]
        public string Title { get; set; }
        [FormItem("副标题")]
        [FormItems.Text(MaxLength = 255)]
        public string SubTitle { get; set; }
        [FormItem("说明")]
        [FormItems.Text(MaxLength = 255, TextCate = FormItems.TextAttribute.Cate.AreaText)]
        public string Desc { get; set; }
        [FormItem("图标(大)", FormItemType.File, Tooltip = "${LargeIconTip}", IsRequire = true)]
        [FormItems.File(MaxSize = FileSizeConst._2MB, FileType = FileTypeConst.Image)]
        public string LargeIcon { get; set; }
        [FormItem("图标(中)", FormItemType.File, Tooltip = "${IconTip}")]
        [FormItems.File(MaxSize = FileSizeConst._2MB, FileType = FileTypeConst.Image)]
        public string Icon { get; set; }
        [FormItem("图标(小)", FormItemType.File, Tooltip = "${MiniIconTip}")]
        [FormItems.File(MaxSize = FileSizeConst._2MB, FileType = FileTypeConst.Image)]
        public string MiniIcon { get; set; }
        [FormItem("父类", FormItemType.ChooseId)]
        [FormItems.ChooseIdFromTable("${NewsCateTableName}", IdColumnName = "${NewsCateIdColumnName}", NameColumnName= "${NewsCateNameColumnName}", ParentIdColumnName = "${NewsCateParentIdColumnName}")]
        public int PId { get; set; }
    }

    [FormModel("/${NewsCatesController}/ModifyModel")]
    public class NewsCateModifyModel : NewsCateAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
