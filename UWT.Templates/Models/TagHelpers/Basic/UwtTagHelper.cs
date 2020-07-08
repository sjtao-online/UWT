using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.TagHelpers.Basic
{
    /// <summary>
    /// UWTTagHelper所有基类
    /// </summary>
    public class UwtTagHelper : TagHelper
    {
        /// <summary>
        /// Html
        /// </summary>
        public IHtmlHelper Html { get; internal set; }
        bool HtmlInit = false;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public UwtTagHelper(IHtmlHelper html)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            this.Html = html;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public ViewContext ViewContext { get; set; }
        public Templates.TagHelpers.TagHelperTemplateModel TemplateModel { get; set; }
        protected IHtmlContent RenderRazorView<TDataModel>(string viewPath, TDataModel model)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            if (!HtmlInit)
            {
                (Html as HtmlHelper).Contextualize(ViewContext);
            }
            return Html.Partial(viewPath, new Templates.TagHelpers.TagHelperDataModel<TDataModel>()
            {
                DataModel = model,
                TemplateModel = TemplateModel
            }, ViewContext.ViewData);
        }
    }
}
