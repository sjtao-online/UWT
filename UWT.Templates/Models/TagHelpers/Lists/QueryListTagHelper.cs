using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.TagHelpers.Lists
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("query-list")]
    public class QueryListTagHelper : Basic.UwtTagHelper
    {
        public List<IFilterBasicModel> QueryList { get; set; }

        public QueryListTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            output.Attributes.Add("class", "layui-card-body uwt-query-list");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Lists/QueryList.cshtml", QueryList));
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
