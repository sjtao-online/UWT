using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Common;
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

        [Route("/Helpers/Detail/{**path}")]
        public IActionResult HelperDetail(string path, int id)
        {
            using (var db = this.GetDB())
            {
                var helper = db.UwtGetTable<IDbHelperTable>();
                var q = (from it in helper where it.Valid select new
                {
                    it.Title,
                    it.Id,
                    it.Content,
                    it.PublishTime,
                    it.Summary,
                    it.Author,
                    it.Url
                });
                if (string.IsNullOrEmpty(path))
                {
                    q = q.Where(it => it.Id == id);
                }
                else
                {
                    q = q.Where(it => it.Url.Contains(";/" + path.ToLower() + ";"));
                }
                q = q.Take(1);
                if (q.Count() != 0)
                {
                    var h = q.First();
                    this.ViewBag.HelperTitle = h.Title;
                    this.ViewBag.HelperContent = h.Content;
                    this.ViewBag.HelperPublishTime = h.PublishTime;
                    this.ViewBag.HelperSummary = h.Summary;
                    this.ViewBag.HelperAuthor = h.Author;
                }
                else
                {
                    return View("NotFount");
                }
            }
            ViewBag.Title = $"/{path} - 详情";
            return View();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
