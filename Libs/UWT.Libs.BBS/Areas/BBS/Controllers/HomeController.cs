using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute]
    public class HomeController : Controller
    {
        [Route("/BBS")]
        [Route("/BBS/Home")]
        [Route("/BBS/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
