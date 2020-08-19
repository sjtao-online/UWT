using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("crumb")]
    public class CrumbTagHelper : TagHelper
    {
        /// <summary>
        /// 面包屑列表
        /// </summary>
        public List<UrlTitleIdModel> CrumbList { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("bread_crumb", HtmlEncoder.Default);
            TagBuilder home = new TagBuilder("a");
            home.Attributes.Add("href", "/BBS");
            var homeContent = new TagBuilder("span");
            homeContent.InnerHtml.AppendHtml("首页");
            home.InnerHtml.AppendHtml(homeContent);
            output.Content.AppendHtml(home);
            foreach (var item in CrumbList)
            {
                var space = new TagBuilder("span");
                space.AddCssClass("space");
                space.InnerHtml.Append(">");
                output.Content.AppendHtml(space);
                var crumb = new TagBuilder("a");
                crumb.Attributes.Add("href", item.Url);
                crumb.InnerHtml.Append(item.Title);
                output.Content.AppendHtml(crumb);
            }
        }
    }
}
