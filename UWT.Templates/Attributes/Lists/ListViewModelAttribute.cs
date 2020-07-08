using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Lists
{
    /// <summary>
    /// 列表项模型特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ListViewModelAttribute : Attribute
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 列表名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 标头颜色
        /// red,green,blue,purple,orange2,pink2,light-blue,brown
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 批量操作Key
        /// </summary>
        public string BatchKey { get; set; }
        /// <summary>
        /// 列表项模型特性
        /// </summary>
        /// <param name="title">标题</param>
        public ListViewModelAttribute(string title = null)
        {
            this.Title = title;
        }
    }
}
