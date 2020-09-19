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
        [Route("/BBS/User/{uid}")]
        public IActionResult Index(int uid)
        {
            FillProfile(uid);
            return View();
        }

        private void FillProfile(int uid)
        {
            ViewBag.Profile = new UserService().Find(uid);
        }
    }
}
