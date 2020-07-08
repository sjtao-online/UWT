using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.TagHelpers.Basic;
using UWT.Templates.Models.Templates.Layouts;

namespace UWT.Templates.Models.TagHelpers.Layouts
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    /// <summary>
    /// 面包屑
    /// </summary>
    [HtmlTargetElement("breadcrumb")]
    public class BreadcrumbTagHelper : UwtTagHelper
    {
        /// <summary>
        /// 布局模型
        /// </summary>
        public LayoutModel Layout { get; set; }
        /// <summary>
        /// 面包屑
        /// </summary>
        public List<IconUrlTitleIdModel> List { get; set; }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public BreadcrumbTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "x-nav breadcrumb");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Layouts/Breadcrumb.cshtml", new BreadcrumbModel()
            { 
                Layout = Layout,
                List = List
            }));
        }
    }
    public class BreadcrumbModel
    {
        public LayoutModel Layout { get; set; }
        public List<IconUrlTitleIdModel> List { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
