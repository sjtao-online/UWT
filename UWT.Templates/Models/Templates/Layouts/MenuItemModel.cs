using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.Layouts
{
    /// <summary>
    /// 菜单
    /// URL与Children同时存在时隐藏子项
    /// </summary>
    public class MenuItemModel : UserMenuItem
    {
        /// <summary>
        /// 悬停提示
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 子项列表
        /// </summary>
        public List<MenuItemModel> Children { get; set; }
    }
}
