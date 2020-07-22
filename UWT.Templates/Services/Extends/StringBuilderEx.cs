using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// StringBuild扩展方法
    /// </summary>
    public static class StringBuilderEx
    {
        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="sb">StringBuilder主体</param>
        /// <param name="appendText">要添加的内容，为null或空不添加</param>
        /// <param name="format">格式化字符串，为空为直接添加内容</param>
        public static void UwtAppend(this StringBuilder sb, string appendText, string format = null)
        {
            if (!string.IsNullOrEmpty(appendText))
            {
                if (format == null)
                {
                    sb.Append(appendText);
                }
                else
                {
                    sb.AppendFormat(format, appendText);
                }
            }
        }
    }
}
