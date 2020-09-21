using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Libs.BBS.Areas.Forums.Models.Users;
using UWT.Libs.BBS.Areas.Forums.Services;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute]
    public class UserController : Controller
    {
        string mTitle = "";
        [Route("/BBS/User/{uid}")]
        public IActionResult Index(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.Fans = new UserService().GetFans<UserLightInfo>(uid);
            return View();
        }

        public IActionResult Profile(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.UserProperties = new UserService().GetProperties(uid);
            return View();
        }

        public IActionResult Topic(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            return View();
        }

        public IActionResult Fans(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.Fans = new UserService().GetFans<UserSimpleInfo>(uid);
            return View();
        }

        public IActionResult Follow(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
            ViewBag.Follows = new UserService().GetFollows<UserSimpleInfo>(uid);
            return View();
        }

        private IActionResult NoProfile()
        {
            return View("NoProfile");
        }

        private bool FillProfile(int uid)
        {
            ViewData["body-class"] = "user-bg";
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
