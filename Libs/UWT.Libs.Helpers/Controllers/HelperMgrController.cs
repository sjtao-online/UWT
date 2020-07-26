using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Helpers.Models;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Helpers.Controllers
{
    /// <summary>
    /// 帮助管理控制器
    /// </summary>
    [AuthUser]
    [UwtControllerName("帮助管理")]
    public class HelperMgrController : Controller
        , IListToPage<IDbHelperTable, HelperMgrListItemModel>
        , IFormToPage<HelperAddModel>
        , IFormToPage<HelperModifyModel>
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        [UwtMethod("列表")]
        public IActionResult Index()
        {
            this.AddHandler("添加", ".Add");
            return this.ListResult(m => new HelperMgrListItemModel()
            {
                Id = m.Id,
                Publish = m.PublishTime.ToShowText(),
                Summary = m.Summary,
                Title = m.Title
            }).View();
        }

        [UwtMethod("添加")]
        public virtual IActionResult Add()
        {
            return this.FormResult<HelperAddModel>().View();
        }

        [UwtMethod("添加")]
        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] HelperAddModel model, string handler)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<HelperAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var map = HandleFormModel(model, handler, db);
                map[nameof(IDbHelperTable.CreatorId)] = this.GetClaimValue("AccountId", 0);
                db.UwtGetTable<IDbHelperTable>().UwtInsertWithInt32(map);
            }
            return this.Success();
        }

        [UwtMethod("编辑")]
        public virtual IActionResult Modify(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var q = (from it in table
                         where it.Id == id && it.Valid
                         select new
                         {
                             it.Id,
                             it.Title,
                             it.Content,
                             it.Summary,
                             it.Author,
                             it.Url
                         }).Take(1);
                if (q.Count() == 0)
                {
                    return this.ItemNotFound();
                }
                var info = q.First();
                var quids = from it in db.UwtGetTable<UWT.Libs.Users.Roles.IDbModuleTable>() where info.Url.Contains(";" + it.Url + ";") select it.Id;
                return this.FormResult(new HelperModifyModel()
                {
                    Id = info.Id,
                    Author = info.Author,
                    Content = info.Content,
                    Summary = info.Summary,
                    Title = info.Title,
                    Url = quids.ToList()
                }).View();
            }
        }

        [HttpPost]
        [UwtMethod("编辑")]
        public virtual async Task<object> ModifyModel([FromBody] HelperModifyModel model, string handler)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var map = HandleFormModel(model, handler, db);
                db.UwtGetTable<IDbHelperTable>().UwtUpdate(model.Id, map);
            }
            return this.Success();
        }

        Dictionary<string, object> HandleFormModel(HelperAddModel model, string handle, LinqToDB.Data.DataConnection db)
        {
            var userId = this.GetClaimValue("AccountId", 0);
            if (string.IsNullOrEmpty(model.Author))
            {
                model.Author = this.GetClaimValue("Account");
            }
            var qurls = from it in db.UwtGetTable<UWT.Libs.Users.Roles.IDbModuleTable>() where model.Url.Contains(it.Id) select it.Url;
            string urls = ";";
            foreach (var item in qurls)
            {
                urls += item + ";";
            }
            var map = new Dictionary<string, object>()
            {
                [nameof(IDbHelperTable.Author)] = model.Author,
                [nameof(IDbHelperTable.Content)] = model.Content,
                [nameof(IDbHelperTable.ModifyId)] = userId,
                [nameof(IDbHelperTable.Title)] = model.Title,
                [nameof(IDbHelperTable.Summary)] = model.Summary,
                [nameof(IDbHelperTable.Url)] = urls,
            };
            if (handle == "publish")
            {
                map[nameof(IDbHelperTable.PublishTime)] = (DateTime?)DateTime.Now;
            }
            return map;
        }

        [HttpPost]
        [UwtMethod("发布")]
        public virtual object Publish(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound);
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.PublishTime)] = (DateTime?)DateTime.Now
                });
                return this.Success();
            }
        }

        [HttpPost]
        [UwtMethod("撤下")]
        public virtual object PublishRemove(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound);
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.PublishTime)] = (DateTime?)null
                });
                return this.Success();
            }
        }

        [HttpPost]
        [UwtMethod("删除")]
        public virtual object Del(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = (from it in table where it.Id == id select 1).Take(1);
                if (o.Count() == 0)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound);
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.Valid)] = false
                });
                return  this.Success();
            };
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
