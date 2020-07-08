using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.TagHelpers.Basic
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class ItemTagHelperModelT<TTemplate>
    {
        public TTemplate TargetTemplate { get; set; }
        public object TargetObject { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
