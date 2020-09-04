using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UWT.Templates.Models.TagHelpers.Forms
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [HtmlTargetElement("form-item")]
    public class FormItemTagHelper : Basic.UwtTagHelper
    {
        public Interfaces.IFormItemModel TargetTemplate { get; set; }
        public object TargetObject { get; set; }

        public FormItemTagHelper(IHtmlHelper html) : base(html)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = UWT.Templates.Models.Consts.HtmlConst.DIV;
            string className = "";
            if (TargetTemplate.IsInline)
            {
                className += "layui-inline";
            }
            else
            {
                className += "layui-form-item";
                if (TargetTemplate.IsFullWidth)
                {
                    className += " layui-form-text";
                }
            }
            if (!string.IsNullOrEmpty(TargetTemplate.GroupItemName))
            {
                className += " groupitem-" + TargetTemplate.GroupItemName;
                if (!string.IsNullOrEmpty(TargetTemplate.GroupName))
                {
                    className += " group-" + TargetTemplate.GroupName;
                }
                else
                {
                    className += " group--default";
                }
            }
            className += " uwt-form-item";
            output.Attributes.Add("class", className);
            output.Attributes.Add("data-title", TargetTemplate.Title);
            output.Attributes.Add("data-itemtype", TargetTemplate.ItemType);
            output.Attributes.Add("data-name", TargetTemplate.Name);
            output.Attributes.Add("data-required", TargetTemplate.IsRequired ? "1" : "0");
            output.Content.SetHtmlContent(this.RenderRazorView("/Views/TagHelpers/Forms/FormItem.cshtml", new Basic.ItemTagHelperModelT<Interfaces.IFormItemModel>()
            {
                TargetObject = this.TargetObject,
                TargetTemplate = TargetTemplate
            }));
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
