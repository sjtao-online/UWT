using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.TagHelpers.Basic;
using UWT.Templates.Models.Templates.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Lists
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("listtable")]
    public class ListTableTagHelper : UwtTagHelper
    {
        public ListTableTagHelper(IHtmlHelper html)
            : base(html)
        {
        }
        public IToPageViewModel Model { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "layui-card-body");
            output.Content.SetHtmlContent(RenderRazorView("/Views/TagHelpers/Lists/ListTables.cshtml", Model));
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
