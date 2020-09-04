using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Layouts
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("layout-border")]
    public class LayoutBorderTagHelper : TagHelper
    {
        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            output.Attributes.Add("class", "layui-row layui-col-space15");
            var content = await output.GetChildContentAsync();
            var newContent = new DefaultTagHelperContent();
            newContent.SetHtmlContent(content.GetContent());
            TagBuilder card = new TagBuilder(UWT.Templates.Models.Consts.HtmlConst.DIV);
            card.AddCssClass("layui-card");
            card.InnerHtml.AppendHtml(newContent);
            TagBuilder col12 = new TagBuilder(UWT.Templates.Models.Consts.HtmlConst.DIV);
            col12.AddCssClass("layui-col-md12");
            col12.InnerHtml.AppendHtml(card);
            output.Content.SetHtmlContent(col12);
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
