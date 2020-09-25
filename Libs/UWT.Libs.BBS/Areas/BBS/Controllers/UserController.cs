using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Libs.BBS.Areas.Forums.Models.Users;
using UWT.Libs.BBS.Areas.Forums.Services;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute]
    public class UserController : Controller
    {
        UserService service = new UserService();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            service.Dispose();
        }
        string mTitle = "";
        [Route("/BBS/User/{uid}")]
        public IActionResult Index(int uid)
        {
            mTitle = "主页";
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.Fans = service.GetFans<UserLightInfo>(uid, 0, 9);
            ViewBag.FansCount = service.GetLastPageCount();
            return View();
        }

        public IActionResult Profile(int uid)
        {
            mTitle = "资料";
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.UserProperties = new UserService().GetProperties(uid);
            return View();
        }

        public IActionResult Topic(int uid)
        {
            mTitle = "帖子";
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            return View();
        }

        public IActionResult Fans(int uid, int pageIndex, int pageSize)
        {
            mTitle = "粉丝";
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            var fs = service.GetFans<UserSimpleInfo>(uid, pageIndex, pageSize);
            ViewBag.Fans = fs;
            ViewBag.PageSelector = new PageSelectorModel()
            {
                ItemTotal = service.GetLastPageCount(),
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? service.GetDefaultPageSize(): pageSize,
                CurrentPageCount = fs.Count,
                UrlBase = $"/BBS/User/Fans?uid={uid}"
            };
            return View();
        }

        public IActionResult Follow(int uid, int pageIndex, int pageSize)
        {
            mTitle = "关注";
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            var fs = service.GetFollows<UserSimpleInfo>(uid, pageIndex, pageSize);
            ViewBag.Follows = fs;
            ViewBag.PageSelector = new PageSelectorModel()
            {
                ItemTotal = service.GetLastPageCount(),
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? service.GetDefaultPageSize() : pageSize,
                CurrentPageCount = fs.Count,
                UrlBase = $"/BBS/User/Follow?uid={uid}"
            };
            return View();
        }

        [AuthBBS]
        public IActionResult ModifyProperties()
        {
            service.GetPropertyConfig();
            return View();
        }

        [HttpPost, AuthBBS]
        public object ModifyProperties([FromBody]Dictionary<string, string> pairs)
        {

            return this.Success();
        }

        private IActionResult NoProfile()
        {
            return View("NoProfile");
        }

        private bool FillProfile(int uid)
        {
            ViewData["body-class"] = "user-bg-2";
            var profile = new UserService().Find<UserProfileModel>(uid, (db, info)=>
            {
                info.TouchCount = 100;
            });
            if (profile == null)
            {
                return false;
            }
            ViewBag.Profile = profile;
            ViewBag.Title = string.Format(BBSEx.BbsConfigModel.Titles.UserSpace, profile.NickName, mTitle);
            return true;
        }
    }
}
