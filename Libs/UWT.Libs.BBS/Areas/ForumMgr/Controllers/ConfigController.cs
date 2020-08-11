using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.ForumMgr.Models;
using UWT.Libs.BBS.Models;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [ForumAreaRoute(null, "/ForumMgr")]
    [AuthUser]
    public class ConfigController : Controller
        , ITemplateController
    {
        [UwtMethod("属性配置")]
        public IActionResult Index()
        {
            using (var db = this.GetDB())
            {
                ViewBag.BbsConfig = (from it in db.TableConfig() select new KeyValuePair<string, string>(it.Key, it.Value)).ToDictionary(m => m);
            }
            return View();
        }
    }
}
