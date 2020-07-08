using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Templates.Layouts
{
    /// <summary>
    /// 用户下拉菜单列表
    /// </summary>
    public class UserMenuItem : IconTitleIdModel
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 消息标识(红点)
        /// null为不显示
        /// ""为显示一个实心红点
        /// 其它值为显示文字 文字长度不建议超过4
        /// </summary>
        public string RedPoint { get; set; }
    }
}
