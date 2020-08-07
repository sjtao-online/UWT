using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UWT.Libs.BBS.Controllers
{
    public class AreaMgrController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
