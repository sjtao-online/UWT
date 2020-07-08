using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.TagHelpers
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class TagHelperDataModel<TModel>
    {
        public TModel DataModel { get; set; }
        public TagHelperTemplateModel TemplateModel { get; set; }
        public TagHelperDataModel()
        {
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
