using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// Form项的分组
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class FormItemGroupAttribute : Attribute
    {
        /// <summary>
        /// 组名，一个Form只一个组时可以没有名称<br/>
        /// 名称只可包括数字字母下划线
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 组项名<br/>
        /// 当DisplayGroup的选项与组项名相同时显示，不同时隐藏<br/>
        /// 组项名只可包括数字字母下划线
        /// </summary>
        public string GroupItemName { get; set; }
        /// <summary>
        /// Form项的分组
        /// </summary>
        /// <param name="grouItemName">组项名</param>
        /// <param name="groupName">组名</param>
        public FormItemGroupAttribute(string grouItemName, string groupName = null)
        {
            GroupItemName = grouItemName;
            GroupName = groupName;
        }
    }
}
