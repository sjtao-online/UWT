using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Services.Converts;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Normals.News
{
    /// <summary>
    /// 文章管理器
    /// 必须重写ModifyProperties方法，用于编辑文章属性
    /// </summary>
    /// <typeparam name="TDbNewsTable"></typeparam>
    public abstract class NewsController<TDbNewsTable> : Controller
        , IListToPage<TDbNewsTable, NewsListItemModel>
        , IFormToPage<NewsAddModel>
        , IFormToPage<NewsModifyModel>
        where TDbNewsTable: class, IDbNewsTable, new ()
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public virtual IActionResult Index()
        {
            this.ActionLog();
            return this.ListResult(m=>new NewsListItemModel()
            {
                Id = m.Id,
                Title = m.Title,
                Summary = m.Summary
            }).View();
        }

        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<NewsAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody]NewsAddModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<NewsAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsTable>();
                table.Insert(() => new TDbNewsTable()
                {
                    Title = model.Title,
                    Content = model.Content,
                    Summary = new HtmlToText(50).Convert(model.Content)
                });
            });
            return this.Success();
        }
        public virtual IActionResult Modify(int id)
        {
            this.ActionLog();
            NewsModifyModel modify = null;
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsTable>();
                var q = (from it in table
                         where it.Id == id && it.Valid
                         select new NewsModifyModel
                         {
                             Id = it.Id,
                             Title = it.Title,
                             Content = it.Content
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
            return this.FormResult<NewsModifyModel>(modify).View();
        }


        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody]NewsModifyModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<NewsModifyModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbNewsTable>();
                table.Update(m => m.Id == model.Id, m => new TDbNewsTable()
                {
                    Title = model.Title,
                    Content = model.Content,
                    Summary = new HtmlToText(50).Convert(model.Content)
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
                var table = db.GetTable<TDbNewsTable>();
                var o = (from it in table where it.Id == id select 1).Take(1);
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.Update(m => m.Id == id, m => new TDbNewsTable()
                {
                    Valid = false
                });
            });
            return notfound ? this.Error(ErrorCode.Item_NotFound) : this.Success();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        /// <summary>
        /// 编辑文章属性页面
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public abstract IActionResult ModifyProperties(int id);
    }
    /// <summary>
    /// 文章数据库模型
    /// </summary>
    public interface IDbNewsTable : IDbTableBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        string Summary { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
    [FormModel("/${NewsController}/AddModel", HandleBtnsInTitleBar = true)]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class NewsAddModel
    {
        [FormItem("标题", TitleIsFullScreen = true)]
        [FormItems.Text]
        public string Title { get; set; }
        [FormItem("内容", FormItemType.Text, TitleIsFullScreen = true)]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.RichText)]
        public string Content { get; set; }
    }
    [FormModel("/${NewsController}/ModifyModel")]
    public class NewsModifyModel : NewsAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
    [ListViewModel]
    class NewsListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("标题")]
        public string Title { get; set; }
        [ListColumn("摘要")]
        public string Summary { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<Templates.Models.Templates.Commons.HandleModel> handles = new List<Templates.Models.Templates.Commons.HandleModel>();
                handles.Add(HandleModel.BuildNavigate("编辑内容", "/${NewsController}/Modify?id=" + Id));
                handles.Add(HandleModel.BuildNavigate( "编辑属性", "/${NewsController}/ModifyProperties?id=" + Id));
                handles.Add(HandleModel.BuildDel("/${NewsController}/Del?id=" + Id));
                return handles;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}