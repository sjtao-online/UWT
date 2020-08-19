using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Libs.BBS.Areas.Forums.Services;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute]
    public class AreaController : Controller
    {
        [Route("/BBS/Area/{areaId}")]
        public IActionResult Index(int areaId, int pageIndex, int pageSize)
        {
            ViewBag.AreaInfo = new AreaService().GetAresInfoSubAreaList(areaId);
            ViewBag.TopicList = new TopicService().List(areaId, pageIndex, pageSize);
            return View();
        }
    }
}
