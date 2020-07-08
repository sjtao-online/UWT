using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Helpers.Models;
using UWT.Libs.Users;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Helpers.Controllers
{
    /// <summary>
    /// 帮助管理控制器
    /// </summary>
    [AuthUser]
    public class HelperMgrController : Controller
        , IListToPage<IDbHelperTable, HelperMgrListItemModel>
        , IFormToPage<HelperAddModel>
        , IFormToPage<HelperModifyModel>
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
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

        public virtual IActionResult Add()
        {
            return this.FormResult<HelperAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] HelperAddModel model, string handler)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<HelperAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var map = HandleFormModel(model, handler, db);
                map[nameof(IDbHelperTable.CreatorId)] = this.GetClaimValue("AccountId", 0);
                db.UwtGetTable<IDbHelperTable>().UwtInsertWithInt32(map);
            });
            return this.Success();
        }

        public virtual IActionResult Modify(int id)
        {
            HelperModifyModel modify = null;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var q = from it in table
                        where it.Id == id && it.Valid
                        select new
                        {
                            it.Id,
                            it.Title,
                            it.Content,
                            it.Summary,
                            it.Author,
                            it.Url
                        };
                if (q.Count() != 0)
                {
                    var info = q.First();
                    var quids = from it in db.UwtGetTable<UWT.Libs.Users.Roles.IDbModuleTable>() where info.Url.Contains(";" + it.Url + ";") select it.Id;
                    modify = new HelperModifyModel()
                    {
                        Id = info.Id,
                        Author = info.Author,
                        Content = info.Content,
                        Summary = info.Summary,
                        Title = info.Title,
                        Url = quids.ToList()
                    };
                }
            });
            if (modify == null)
            {
                return this.ItemNotFound();
            }
            return this.FormResult(modify).View();
        }

        [HttpPost]
        
        public virtual async Task<object> ModifyModel([FromBody] HelperModifyModel model, string handler)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var map = HandleFormModel(model, handler, db);
                db.UwtGetTable<IDbHelperTable>().UwtUpdate(model.Id, map);
            });
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
        public virtual object Publish(int id)
        {
            bool notfound = false;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.PublishTime)] = (DateTime?)DateTime.Now
                });
            });
            return notfound ? this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound) : this.Success();
        }

        [HttpPost]
        public virtual object PublishRemove(int id)
        {
            bool notfound = false;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.PublishTime)] = (DateTime?)null
                });
            });
            return notfound ? this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound) : this.Success();
        }

        [HttpPost]
        public virtual object Del(int id)
        {
            bool notfound = false;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbHelperTable>();
                var o = from it in table where it.Id == id select 1;
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbHelperTable.Valid)] = false
                });
            });
            return notfound ? this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound) : this.Success();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
