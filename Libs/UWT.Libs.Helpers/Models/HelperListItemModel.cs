using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.Helpers.Models
{
    [ListViewModel("帮助列表")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class HelperListItemModel : IdModel
    {
        [ListColumn("序号", ColumnType = ColumnType.MIndex)]
        [System.Text.Json.Serialization.JsonIgnore]
        public int Index { get; set; }
        [ListColumn("标题")]
        public string Title { get; set; }
        [ListColumn("摘要")]
        public string Summary { get; set; }
        [ListColumn("发布日期")]
        public string Publish { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
