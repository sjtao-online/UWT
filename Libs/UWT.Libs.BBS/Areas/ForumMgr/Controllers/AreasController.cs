using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Users;
using UWT.Libs.BBS.Areas.ForumMgr.Models.Areas;
using UWT.Libs.BBS.Models;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;
using UWT.Libs.BBS.Areas.ForumMgr.Models;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [AuthUser, ForumAreaRoute("版块管理")]
    public class AreasController : Controller
        , IListToPage<AreaMgrListItemModel, AreaMgrListItemModel>
        , IFormToPage<AreaMgrAddModel>
    {
        public IActionResult Index()
        {
            this.AddHandler("添加", ".Add");
            using (var db = this.GetDB())
            {
                var q = from it in db.GetTable<UwtBbsArea>()
                        join mgr in db.GetTable<UwtBbsUser>() on it.MgrUserId equals mgr.Id
                        join p in db.GetTable<UwtBbsArea>() on it.PId equals p.Id into all
                        from pp in all.DefaultIfEmpty()
                        select new AreaMgrListItemModel()
                        {
                            Id = it.Id,
                            Name = it.Name,
                            MgrName = mgr.Nickname,
                            PName = pp.Name,
                            Status = ""
                        };
                return this.ListResult(m=> new AreaMgrListItemModel()
                {
                    Id = m.Id,
                    MgrName = m.MgrName,
                    Name = m.Name,
                    PName = m.PName,
                    Status = m.Status
                }, q).View();
            }
        }
        public virtual IActionResult Add()
        {
            return this.FormResult<AreaMgrAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] AreaMgrAddModel model)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<UwtBbsArea>();
                table.Insert(() => new UwtBbsArea()
                {
                    PId = model.PId,
                    Name = model.Name,
                    Summary = model.Summary,
                    Icon = model.Icon,
                    Desc = model.Desc,
                    MgrUserId = model.MgrId,
                    Apply = model.Apply ? "approved" : "publish",
                    Status = "show"
                });
            }
            return this.Success();
        }
    }
}
