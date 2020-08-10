﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Routes;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [UwtRoute("BBS")]
    [UwtNoRecordModule]
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
