using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.TagHelpers.Basic;
using UWT.Templates.Models.Templates.Layouts;

namespace UWT.Templates.Models.TagHelpers.Layouts
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    /// <summary>
    /// 快捷栏
    /// </summary>
    [HtmlTargetElement("quick-bar")]
    public class QuickBarTagHelper : UwtTagHelper
    {
        /// <summary>
        /// 快捷栏项列表
        /// </summary>
        public List<MenuItemModel> QuickList { get; set; }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public QuickBarTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (QuickList == null || QuickList.Count == 0)
            {
                return;
            }
            output.TagName = "ul";
            output.Attributes.Add("class", "layui-nav quick-list");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Layouts/QuickBar.cshtml", QuickList));
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
