using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Libs.BBS.Areas.Forums.Services;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute]
    public class AreaController : Controller
    {
        AreaService AreaService = null;
        public AreaController()
        {
            AreaService = new AreaService();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            AreaService.Dispose();
        }
        [Route("/BBS/Area/{areaId:int}")]
        public IActionResult Index(int areaId, string order, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = BBSEx.BbsConfigModel.PageConfig.Default.PageSize;
            }
            bool isPostdate = ViewBag.IsPostDate = order?.ToLower() == "postdate";
            var areainfo = AreaService.GetAresInfoSubAreaList(areaId);
            ViewBag.CrumbList = AreaService.GetCrumbListFromAreaId(areaId);
            ViewBag.AreaInfo = areainfo;
            ViewBag.TopicList = new TopicService().List(areaId, isPostdate, pageIndex, pageSize);
            ViewBag.Title = areainfo.Title;
            ViewBag.PageSelector = new PageSelectorModel()
            {
                CurrentPageCount = 10,
                ItemTotal = 500,
                PageIndex = pageIndex,
                PageSize = pageSize,
                UrlBase = "/BBS/Area/" + areaId + "?" + (isPostdate ? "&order=postdate&" : "")
            };
            return View();
        }

        public IActionResult Topic(int id, int areaId, int pageIndex)
        {
            List<TopicItemModel> topicList = new List<TopicItemModel>();
            Dictionary<string, Forums.Models.Users.UserSimpleInfo> userInfoMap = new Dictionary<string, Forums.Models.Users.UserSimpleInfo>();
            List<UrlTitleIdModel> crumbList = AreaService.GetCrumbListFromTopicId(id, areaId);
            ViewBag.AreaId = areaId;
            ViewBag.TopicList = topicList;
            ViewBag.UserInfoMap = userInfoMap;
            ViewBag.CrumbList = crumbList;
            return View();
        }

        public IActionResult CreateTopic(int id)
        {
            ViewBag.AreaId = id;
            return View();
        }
    }
}
