using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Routes;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [UwtRoute("ForumMgr", "/[area]")]
    public class ConfigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
