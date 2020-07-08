using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Helpers.Models
{
    [FormModel("/HelperMgr/ModifyModel", Title = "帮助编辑")]
    [FormHandler("保存", "save")]
    [FormHandler("发布", "publish")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class HelperModifyModel : HelperAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
