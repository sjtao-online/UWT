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
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("MenuGroup")]
    public class MenuGroupTagHelper : UwtTagHelper
    {
        public List<MenuItemModel> MenuGroup { get; set; }
        public List<IconUrlTitleIdModel> BreadcrumbList { get; set; }
        public MenuGroupTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            output.Attributes.Add("class", "side-nav");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Layouts/MenuGroup.cshtml", new MenuGroupModel()
            { 
                MenuGroup = MenuGroup,
                BreadcrumbList = BreadcrumbList
            }));
        }
    }
    public class MenuGroupModel
    {
        public List<MenuItemModel> MenuGroup { get; internal set; }
        public List<IconUrlTitleIdModel> BreadcrumbList { get; internal set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
