using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Users.MenuGroups
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [FormModel("/MenuGroups/AddModel")]
    public class MenuGroupAddModel
    {
        [FormItem("名称")]
        [FormItems.Text]
        public string Name { get; set; }
        [FormItem("说明")]
        [FormItems.Text]
        public string Desc { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
