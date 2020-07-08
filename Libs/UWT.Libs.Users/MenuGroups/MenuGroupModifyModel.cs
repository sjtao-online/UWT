using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Users.MenuGroups
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [FormModel("/MenuGroups/ModifyModel")]
    public class MenuGroupModifyModel : MenuGroupAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
