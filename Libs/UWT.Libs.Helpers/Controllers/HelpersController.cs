using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Helpers.Models;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Helpers.Controllers
{
    /// <summary>
    /// 帮助显示控制器
    /// </summary>
    [UwtNoRecordModule]
    public class HelpersController : Controller
        , IListToPage<IDbHelperTable, HelperListItemModel>
        , ITemplateController
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult Index()
        {
            var hlist = this.ListObjectResult(m => new HelperListItemModel()
            {
                Id = m.Id,
                Title = m.Title,
                Publish = m.PublishTime.ToShowText(),
                Summary = m.Summary
            }, m => m.Valid && m.PublishTime != null);
            ViewBag.HList = hlist;
            return View();
        }
        [Route("/Helpers/Detail/{a}/{b}/{c=_}")]
        public IActionResult HelperDetail(string a, string b, string c)
        {
            string url = null;
            //  是否为没有区域的URL
            if (c == "_")
            {
                url = $"/{a}/{b}";
            }
            else
            {
                url = $"/{a}/{b}/{c}";
            }
            using (var db = this.GetDB())
            {
                var helper = db.UwtGetTable<IDbHelperTable>();
                var q = (from it in helper where it.Url.Contains(";" + url + ";") && it.Valid select it).Take(1);
                if (q.Count() != 0)
                {
                    var h = q.First();
                    this.ViewBag.HelperTitle = h.Title;
                    this.ViewBag.HelperContent = h.Content;
                    this.ViewBag.HelperPublishTime = h.PublishTime;
                    this.ViewBag.HelperSummary = h.Summary;
                    this.ViewBag.HelperAuthor = h.Author;
                }
            }
            ViewBag.Title = url + " - 详情";
            return View();
        }

        public IActionResult Detail(int id)
        {
            using (var db = this.GetDB())
            {
                var helper = db.UwtGetTable<IDbHelperTable>();
                var q = (from it in helper where it.Id == id && it.Valid select it).Take(1);
                if (q.Count() != 0)
                {
                    var h = q.First();
                    ViewBag.HelperTitle = h.Title;
                    ViewBag.HelperContent = h.Content;
                    ViewBag.HelperPublishTime = h.PublishTime;
                    ViewBag.HelperSummary = h.Summary;
                    ViewBag.HelperAuthor = h.Author;
                }
            }
            return View("HelperDetail");
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
