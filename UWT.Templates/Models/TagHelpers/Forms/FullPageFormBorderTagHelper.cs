using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Forms
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    /// <summary>
    /// 用于注册登录等的外边框
    /// </summary>
    [HtmlTargetElement("full-page-form-border")]
    public class FullPageFormBorderTagHelper : TagHelper
    {
        /// <summary>
        /// 标签式的标题为空为不显示
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 方式
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            output.Attributes.Add("class", "login layui-anim layui-anim-up");
            if (!string.IsNullOrEmpty(Title))
            {
                output.Content.AppendHtml("<div class='message'>" + Title + "</div><div id='darkbannerwrap'></div>");
            }
            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml($"<form method=\"post\" class=\"layui-form\" data-url='{Url}' data-method='{Method}' id='{Id}'>");
            output.Content.AppendHtml(content);
            output.Content.AppendHtml("</form>");
        }
    }
}
