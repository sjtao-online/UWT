using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Details
{
    /// <summary>
    /// 详情Razor条目
    /// </summary>
    [HtmlTargetElement("detail-item")]
    public class DetailItemTagHelper : Basic.UwtTagHelper
    {
        /// <summary>
        /// 目标
        /// </summary>
        public Interfaces.IDetailItemModel TargetTemplate { get; set; }
        /// <summary>
        /// 实体
        /// </summary>
        public object TargetObject { get; set; }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public DetailItemTagHelper(IHtmlHelper html) : base(html)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
        }

        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            output.Attributes.Add("class", "layui-form");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Details/DetailItem.cshtml", new Basic.ItemTagHelperModelT<Interfaces.IDetailItemModel>()
            {
                TargetObject = this.TargetObject,
                TargetTemplate = this.TargetTemplate
            }));
        }
    }
}
