using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;
using UWT.Libs.BBS.Models;
using UWT.Libs.Users.Users;
using LinqToDB;
using UWT.Templates.Models.Basics;
using UWT.Libs.BBS.Areas.ForumMgr.Models;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [AuthUser, ForumAreaRoute("用户管理")]
    public class UsersController : Controller
        , IListToPage<Models.Users.UserListItemModel, Models.Users.UserListItemModel>
    {
        public IActionResult Index()
        {
            using (var db = this.GetDB())
            {
                var q = from it in db.TableUser()
                        join a in db.UwtGetTable<IDbAccountTable>() on it.AId equals a.Id
                        select new Models.Users.UserListItemModel()
                        {
                            Id = it.Id,
                            Nickname = it.Nickname,
                            Account = a.Account,
                            JoinTime = it.JoinTime.ToShowText(),
                            Status = it.Auths
                        };
                return this.ListResult(m => new Models.Users.UserListItemModel(), q, callback: p=>
                {
                    foreach (Models.Users.UserListItemModel item in p.Items)
                    {
                        item.Status = "废物";
                    }
                }).View();
            }
        }

        [HttpPost]
        [UwtMethod("违规昵称")]
        public virtual object NicknameBreak(int id)
        {
            using (var db = this.GetDB())
            {
                var q = (from it in db.TableUser() where it.Id == it.Id && it.Valid select it.Id).Take(1);
                if (q.Count() == 0)
                {
                    return this.ItemNotFound();
                }
                db.TableUser().Update(m => m.Id == id, m => new UwtBbsUser()
                {
                    Nickname = "违规昵称" + Uwtid.NewUwtid().ToStringZ2()
                });
            }
            return this.Success();
        }

        [HttpPost]
        [UwtMethod("禁言")]
        public virtual object BanWords(int id)
        {
            using (var db = this.GetDB())
            {
                var q = (from it in db.TableUser() where it.Id == it.Id && it.Valid select it.Auths).Take(1);
                if (q.Count() == 0)
                {
                    return this.ItemNotFound();
                }
                var auth = q.First();
                db.TableUser().Update(m => m.Id == id, m => new UwtBbsUser()
                {
                    Auths = auth
                });
            }
            return this.Success();
        }

        [HttpPost]
        [UwtMethod("解除禁言")]
        public virtual object LiftBanWorks(int id)
        {
            using (var db = this.GetDB())
            {
                var q = (from it in db.TableUser() where it.Id == it.Id && it.Valid select it.Auths).Take(1);
                if (q.Count() == 0)
                {
                    return this.ItemNotFound();
                }
                var auth = q.First();
                db.TableUser().Update(m => m.Id == id, m => new UwtBbsUser()
                {
                    Auths = auth
                });
            }
            return this.Success();
        }
    }
}
