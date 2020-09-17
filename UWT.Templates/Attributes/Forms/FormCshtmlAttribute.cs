using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// 表单页添加纯显示或表单外功能
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class FormCshtmlAttribute : Attribute
    {
        /// <summary>
        /// 位置信息
        /// </summary>
        public FormCshtmlPosition Position { get; set; }
        /// <summary>
        /// 视图路径
        /// </summary>
        public string CshtmlPath { get; set; }
        /// <summary>
        /// 表单页添加纯显示或表单外功能
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="cshtml">路径</param>
        public FormCshtmlAttribute(FormCshtmlPosition position, string cshtml)
        {
            Position = position;
            CshtmlPath = cshtml;
        }
    }
    /// <summary>
    /// 位置信息
    /// </summary>
    public enum FormCshtmlPosition
    {
        /// <summary>
        /// 顶部
        /// </summary>
        Header,
        /// <summary>
        /// 底部，但在操作之前
        /// </summary>
        FooterHandleBegin,
        /// <summary>
        /// 底部，但在操作之后
        /// </summary>
        FooterHandleEnd
    }
}
