﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.TagHelpers.Lists
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("handle-list")]
    public class HandleListTagHelper : Basic.UwtTagHelper
    {
        public HandleListTagHelper(IHtmlHelper html)
            : base(html)
        {
        }
        public List<HandleModel> Model { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "layui-card-body uwt-handle-list");
            output.Content.SetHtmlContent(RenderRazorView("/Views/TagHelpers/Lists/HandleList.cshtml", Model));
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
