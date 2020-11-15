using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.ForumMgr.Models;
using UWT.Libs.BBS.Models;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [AuthUser, ForumAreaRoute("等级")]
    public class LevelsController : Controller
        , IListToPage<Models.Levels.LevelListItemModel>
        , IFormToPage<Models.Levels.LevelAddModel>
        , IFormToPage<Models.Levels.LevelModifyModel>
    {
        public IActionResult Index()
        {
            this.SetTitle("等级管理");
            this.AddHandler("添加", ".Add");
            using (var db = this.GetDB())
            {
                var q = from it in db.TableUserLevel()
                        join t in db.TableUserLevelType() on it.TypeId equals t.Id
                        select new Models.Levels.LevelListItemModel()
                        {
                            Id = it.Id,
                            Name = it.Name,
                            TypeName = t.Name,
                            TypeId = t.Id,
                            Exp = it.Exp,
                        };
                return this.ListResult(q, m => new Models.Levels.LevelListItemModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    TypeName = m.TypeName,
                    Exp = m.Exp,
                    TypeId = m.TypeId,
                }).View();
            }
        }

        public virtual IActionResult Add()
        {
            return this.FormResult<Models.Levels.LevelAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] Models.Levels.LevelAddModel model)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<Models.Levels.LevelAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var table = db.TableUserLevel();
                table.Insert(() => new Libs.BBS.Models.UwtBbsUserLevel()
                {
                    Name = model.Name,
                    Avatar =model.Icon,
                    Exp = model.Exp,
                    TypeId = model.TypeId,
                });
            }
            return this.Success();
        }


    }

}
