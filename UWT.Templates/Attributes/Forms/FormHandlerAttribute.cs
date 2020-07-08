using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// 表单操作器
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class FormHandlerAttribute : Attribute
    {
        /// <summary>
        /// 按钮标题
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Handler { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Styles { get; set; }
        /// <summary>
        /// 执行的JS脚本代码<br/>
        /// 若return false为放弃提交动作
        /// </summary>
        public string JSCallback { get; set; }
        /// <summary>
        /// 表单处理器
        /// </summary>
        /// <param name="title">按钮标题</param>
        /// <param name="handler">附加的操作类型,在QueryString中使用handler参数传递</param>
        public FormHandlerAttribute(string title, string handler = null)
        {
            Title = title;
            Handler = handler;
        }
    }
}
