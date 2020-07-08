using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Helpers.Models
{
    [FormModel("/HelperMgr/AddModel", Title = "添加帮助")]
    [FormHandler("保存", "save")]
    [FormHandler("发布", "publish")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class HelperAddModel
    {
        [FormItem("标题", IsRequire = true)]
        [FormItems.Text]
        public string Title { get; set; }
        [FormItem("显示作者", Tooltip = "随便写想显示成什么")]
        [FormItems.Text(MaxLength = 20)]
        public string Author { get; set; }
        [FormItem("对应URL", FormItemType.ChooseId, Tooltip = "哪个URL的帮助")]
        [FormItems.ChooseIdFromTable("${ModulesTableName}", NameColumnName = "url", Where = "type = 'page'")]
        public List<int> Url { get; set; }
        [FormItem("摘要")]
        [FormItems.Text(MaxLength = 255)]
        public string Summary { get; set; }
        [FormItem("内容")]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.RichText)]
        public string Content { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
