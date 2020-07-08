using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.Forms
{
    /// <summary>
    /// 不符合标准的错误信息
    /// </summary>
    public class FormValidModel
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
