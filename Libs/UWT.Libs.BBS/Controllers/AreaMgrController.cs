using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Users;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Controllers
{
    [AuthUser]
    public class AreaMgrController : Controller
        , IListToPage<Models.AreaMgr.AreaMgrListItemModel, Models.AreaMgr.AreaMgrListItemModel>
        , IFormToPage<Models.AreaMgr.AreaMgrAddModel>
    {
        public IActionResult Index()
        {
            using (var db = this.GetDB())
            {
                var q = from it in db.GetTable<Models.UwtBbsArea>()
                        join mgr in db.GetTable<Models.UwtBbsUser>() on it.MgrUserId equals mgr.Id
                        join p in db.GetTable<Models.UwtBbsArea>() on it.PId equals p.Id into all
                        from pp in all.DefaultIfEmpty()
                        select new Models.AreaMgr.AreaMgrListItemModel()
                        {
                            Id = it.Id,
                            Name = it.Name,
                            MgrName = mgr.Nickname,
                            PName = pp.Name,
                            Status = ""
                        };
                return this.ListResult(m=> new Models.AreaMgr.AreaMgrListItemModel()
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
            return this.FormResult<Models.AreaMgr.AreaMgrAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] Models.AreaMgr.AreaMgrAddModel model)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<Models.AreaMgr.AreaMgrAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<Models.UwtBbsArea>();
                table.Insert(() => new Models.UwtBbsArea()
                {
                    PId = model.PId,
                    Name = model.Name,
                    Summary = model.Summary,
                    Icon = model.Icon,
                    Desc = model.Desc,
                    MgrUserId = model.MgrId,
                    Apply = "publish",
                    Status = "show"
                });
            }
            return this.Success();
        }
    }
}
