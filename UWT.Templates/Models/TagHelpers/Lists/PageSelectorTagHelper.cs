using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Lists
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("page-selector")]
    public class PageSelectorTagHelper : Basic.UwtTagHelper
    {
        public Interfaces.IPageSelectorModel Model { get; set; }
        public Dictionary<string, string> EmptyListMap { get; set; }
        public PageSelectorTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Model.ItemTotal == 0)
            {
                output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
                if (EmptyListMap != null && EmptyListMap.ContainsKey("t"))
                {
                    switch (EmptyListMap["t"])
                    {
                        case "t":
                            output.Attributes.Add("style", "text-align: center;");
                            output.Content.Append(EmptyListMap["text"]);
                            break;
                        case "v":
                            output.Content.SetHtmlContent(this.RenderRazorView(EmptyListMap["path"], Model));
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    output.Attributes.Add("style", "text-align: center;");
                    output.Content.Append("暂无数据");
                }
            }
            else
            {
                output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
                output.Attributes.Add("class", "layui-card-body uwt-page-selector");
                output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Lists/PageSelector.cshtml", Model));
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
