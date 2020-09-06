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
        [Route("/BBS/Area/{areaId:int}")]
        public IActionResult Index(int areaId, string order, int pageIndex, int pageSize = 20)
        {
            bool isPostdate = ViewBag.IsPostDate = order?.ToLower() == "postdate";
            var areainfo = new AreaService().GetAresInfoSubAreaList(areaId);
            ViewBag.AreaInfo = areainfo;
            ViewBag.TopicList = new TopicService().List(areaId, isPostdate, pageIndex, pageSize);
            ViewBag.Title = areainfo.Title;
            ViewBag.PageSelector = new PageSelectorModel()
            {
                CurrentPageCount = 10,
                ItemTotal = 200,
                PageIndex = pageIndex,
                PageSize = pageSize,
                UrlBase = "/BBS/Area/" + areaId + "?" + (isPostdate ? "&order=postdate" : "")
            };
            return View();
        }

        public IActionResult Topic(int id, int pageIndex)
        {
            return View();
        }
    }
}
