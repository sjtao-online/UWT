using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Libs.BBS.Areas.Forums.Models.Areas;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("area-group")]
    public class AreaGroupTagHelper : TagHelper
    {
        /// <summary>
        /// 区域信息
        /// </summary>
        public AreaModel AreaInfo { get; set; }
        /// <summary>
        /// 显示方式
        /// </summary>
        public SubAreaListShowCate ShowCategory { get; set; }
        /// <summary>
        /// 最后回复URL格式
        /// </summary>
        public string LastCommentUrlFormat { get; set; } = "/BBS/Topic/{1}?comment={0}";
        /// <summary>
        /// 区域页面
        /// </summary>
        public string AreaUrlFormat { get; set; } = "/BBS/Area/{0}";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //  主体
            output.TagName = "div";
            output.AddClass("area-group", HtmlEncoder.Default);
            var title = new TagBuilder("div");
            title.AddCssClass("title");

            #region 标题
            var titleA = new TagBuilder("A");
            titleA.InnerHtml.Append(AreaInfo.Title);
            titleA.Attributes.Add("href", string.Format(AreaUrlFormat, AreaInfo.Id));
            title.InnerHtml.AppendHtml(titleA);
            #endregion
            output.Content.AppendHtml(title);

            var content = new TagBuilder("div");
            content.AddCssClass("content");
            output.Content.AppendHtml(content);
            #region 子版块
            switch (ShowCategory)
            {
                case SubAreaListShowCate.List:
                    output.AddClass("list", HtmlEncoder.Default);
                    var table = new TagBuilder("table");
                    table.Attributes.Add("style", "width: 100%");
                    foreach (var item in AreaInfo.Children)
                    {
                        var areaItem = new TagBuilder("tr");
                        areaItem.AddCssClass("area-item");
                        //  Logo Div
                        var logoDiv = new TagBuilder("td");
                        logoDiv.AddCssClass("area-logo");
                        var logoImg = new TagBuilder("img");
                        logoImg.Attributes.Add("src", item.Icon);
                        logoDiv.InnerHtml.AppendHtml(logoImg);
                        areaItem.InnerHtml.AppendHtml(logoDiv);
                        //  Info
                        var info = new TagBuilder("td");
                        info.AddCssClass("area-info");
                        var subTitle = new TagBuilder("div");
                        subTitle.AddCssClass("title");
                        subTitle.InnerHtml.Append(item.Title);
                        info.InnerHtml.AppendHtml(subTitle);
                        var summary = new TagBuilder("div");
                        summary.AddCssClass("summary");
                        summary.InnerHtml.Append(item.Summary);
                        info.InnerHtml.AppendHtml(summary);
                        areaItem.InnerHtml.AppendHtml(info);
                        //  Count
                        var cnt = new TagBuilder("td");
                        cnt.AddCssClass("area-count");
                        var tcnt = new TagBuilder("span");
                        tcnt.AddCssClass("topic");
                        tcnt.InnerHtml.Append(item.TopicCount.ToString());
                        cnt.InnerHtml.AppendHtml(tcnt);
                        cnt.InnerHtml.Append(" / ");
                        var ccnt = new TagBuilder("span");
                        ccnt.AddCssClass("comment");
                        ccnt.InnerHtml.Append(item.CommentCount.ToString());
                        cnt.InnerHtml.AppendHtml(ccnt);
                        areaItem.InnerHtml.AppendHtml(cnt);
                        //  Last
                        var last = new TagBuilder("td");
                        last.AddCssClass("area-last");
                        if (item.LastComment != null)
                        {
                            var topic = new TagBuilder("div");
                            topic.InnerHtml.Append(item.LastComment.UserName);
                            last.InnerHtml.AppendHtml(topic);
                            var lastInfo = new TagBuilder("div");
                            lastInfo.AddCssClass("last-info");
                            lastInfo.InnerHtml.Append("最后回复：");
                            var usera = new TagBuilder("a");
                            usera.InnerHtml.Append(item.LastComment.UserName);
                            usera.Attributes.Add("href", "/BBS/userinfo/" + item.LastComment.UserId);
                            lastInfo.InnerHtml.AppendHtml(usera);
                            lastInfo.InnerHtml.Append(" ");
                            var lastTime = new TagBuilder("a");
                            lastTime.Attributes.Add("href", string.Format(LastCommentUrlFormat, item.LastComment.Id, item.Id));
                            lastTime.InnerHtml.Append(ToDateTimeShowText(item.LastComment.CommentTime));
                            lastInfo.InnerHtml.AppendHtml(lastTime);
                            last.InnerHtml.AppendHtml(lastInfo);
                        }
                        areaItem.InnerHtml.AppendHtml(last);
                        table.InnerHtml.AppendHtml(areaItem);
                    }
                    content.InnerHtml.AppendHtml(table);
                    break;
                case SubAreaListShowCate.Tile:
                    output.AddClass("tile", HtmlEncoder.Default);
                    foreach (var item in AreaInfo.Children)
                    {
                        var areaItem = new TagBuilder("div");
                        areaItem.AddCssClass("area-item");
                        //  Logo Div
                        var logoDiv = new TagBuilder("div");
                        logoDiv.AddCssClass("area-logo");
                        var logoImg = new TagBuilder("img");
                        logoImg.Attributes.Add("src", item.Icon);
                        logoDiv.InnerHtml.AppendHtml(logoImg);
                        areaItem.InnerHtml.AppendHtml(logoDiv);
                        //  Info
                        var info = new TagBuilder("div");
                        info.AddCssClass("area-info");
                        var subTitle = new TagBuilder("div");
                        subTitle.AddCssClass("title");
                        subTitle.InnerHtml.Append(item.Title);
                        info.InnerHtml.AppendHtml(subTitle);
                        info.InnerHtml.AppendFormat("主题：{0},帖子：{1}", item.TopicCount, item.CommentCount);
                        if (item.LastComment != null)
                        {
                            info.InnerHtml.AppendHtml("<br/>");
                            info.InnerHtml.Append("最后回复：");
                            var lastComment = new TagBuilder("A");
                            lastComment.InnerHtml.Append(ToDateTimeShowText(item.LastComment.CommentTime));
                            lastComment.Attributes.Add("href", string.Format(LastCommentUrlFormat, item.LastComment.Id, item.Id));
                            info.InnerHtml.AppendHtml(lastComment);
                        }
                        areaItem.InnerHtml.AppendHtml(info);
                        content.InnerHtml.AppendHtml(areaItem);
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }

        static string ToDateTimeShowText(DateTime dt)
        {
            var days = DateTime.Now.Date - dt;
            if (days.TotalDays < 3)
            {
                string s = dt.ToString("HH:mm");
                if (days.TotalDays < 1)
                {
                    return s;
                }
                else if (days.TotalDays < 2)
                {
                    return "昨天" + s;
                }
                else
                {
                    return "前天" + s;
                }
            }
            else if (days.TotalDays > DateTime.Now.DayOfYear)
            {
                return dt.ToString("MM-dd HH:mm");
            }
            else
            {
                return dt.ToString("yyyy-MM-dd HH:mm");
            }
        }
    }

    public enum SubAreaListShowCate
    {
        List,
        Tile,
    }
}
