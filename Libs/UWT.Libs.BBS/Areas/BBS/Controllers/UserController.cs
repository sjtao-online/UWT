using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Libs.BBS.Areas.BBS.Models;
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
            return View();
        }

        public IActionResult Profile(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
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
            return View();
        }

        public IActionResult Follow(int uid)
        {
            if (!FillProfile(uid))
            {
                return NoProfile();
            }
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
