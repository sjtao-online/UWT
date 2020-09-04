using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Normals.Versions
{
    /// <summary>
    /// 版本管理器
    /// </summary>
    public abstract class VersionMgrController : Controller
        , IListToPage<IDbVersionTable, VersionListItemModel>
        , IFormToPage<VersionAddModel>
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult Index()
        {
            this.ActionLog();
            return this.ListResult(it=>new VersionListItemModel()
            {
                Id = it.Id,
                Name =it.Name,
                BuildTime = it.BuildTime.ToShowText(),
                PublishTime = it.BuildTime.ToShowText(),
                Valid = it.Valid,
                Version = it.Version
            }).View();
        }
        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<VersionAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] VersionAddModel model, string handler)
        {
            this.ActionLog();
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<VersionAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbVersionTable>();
                table.UwtInsertWithInt32(new Dictionary<string, object>()
                {
                    [nameof(IDbVersionTable.Name)] = model.Name,
                    [nameof(IDbVersionTable.Logs)] =model.Logs,
                    [nameof(IDbVersionTable.BuildTime)] = model.BuildTime,
                    [nameof(IDbVersionTable.Type)] = model.Type,
                    [nameof(IDbVersionTable.Version)] = model.Version,
                    [nameof(IDbVersionTable.VersionNo)] = model.VersionNo,
                    [nameof(IDbVersionTable.Path)] = model.FilePath,
                    [nameof(IDbVersionTable.Valid)] = handler == "publish",
                    [nameof(IDbVersionTable.PublishTime)] = DateTimeOffset.Now.LocalDateTime
                });
            });
            return this.Success();
        }

        [HttpPost]
        public virtual object Publish(int id)
        {
            this.ActionLog();
            this.UsingDb(db =>
            {
                db.UwtGetTable<IDbVersionTable>().UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbVersionTable.Valid)] = true,
                    [nameof(IDbVersionTable.PublishTime)] = DateTimeOffset.Now.LocalDateTime
                });
            });
            return this.Success();
        }

        [HttpPost]
        public virtual object PublishRemove(int id)
        {
            this.ActionLog();
            this.UsingDb(db =>
            {
                db.UwtGetTable<IDbVersionTable>().UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbVersionTable.Valid)] = false
                });
            });
            return this.Success();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    [FormModel("/${VersionMgrController}/AddModel")]
    [FormHandler("发布", Handler = "publish")]
    [FormHandler("保存", Handler = "save")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class VersionAddModel
    {
        [FormItem("名称")]
        [FormItems.Text(MaxLength = 20)]
        public string Name { get; set; }
        [FormItem("版本号", IsRequire = true)]
        [FormItems.Text(Regex = RegexConst.Version2)]
        public string Version { get; set; }
        [FormItem("版本号", FormItemType.Integer)]
        [FormItems.Integer]
        public int VersionNo { get; set; }
        [FormItem("日志")]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.AreaText, MaxLength = 800)]
        public string Logs { get; set; }
        [FormItem("编译时间", FormItemType.DateTime)]
        [FormItems.DateTime()]
        public DateTime BuildTime { get; set; }
        [FormItem("类型")]
        public string Type { get; set; }
        [FormItem("包", FormItemType.File)]
        [FormItems.File(MaxSize = FileSizeConst.MB * 100)]
        public string FilePath { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    /// <summary>
    /// 版本号列表模型
    /// </summary>
    [ListViewModel("版本列表")]
    public class VersionListItemModel
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("名称")]
        public string Name { get; set; }
        [ListColumn("版本号")]
        public string Version { get; set; }
        [ListColumn("发布时间")]
        public string PublishTime { get; set; }
        [ListColumn("编译时间")]
        public string BuildTime { get; set; }
        [ListColumn("状态")]
        public string StatusText
        {
            get
            {
                return Valid ? "已发布" : "未发布";
            }
        }
        public bool Valid { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                if (Valid)
                {
                    handles.Add(HandleModel.BuildPublish("/${VersionMgrController}/Publish?id=" + Id));
                }
                else
                {
                    handles.Add(HandleModel.BuildPublish("/${VersionMgrController}/PublishRemove?id=" + Id));
                }
                return handles;
            }
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    /// <summary>
    /// 版本表模型
    /// </summary>
    public interface IDbVersionTable : IDbTableBase
    {
        /// <summary>
        /// 版本名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 版本号(显示用)
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 版本号(排序用)
        /// </summary>
        public int VersionNo { get; set; }
        /// <summary>
        /// 日志说明
        /// </summary>
        public string Logs { get; set; }
        /// <summary>
        /// 下载路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// 编译时间
        /// </summary>
        public DateTime BuildTime { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        public bool Valid { get; set; }
    }
}
