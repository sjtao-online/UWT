using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// TagBuilder扩展
    /// </summary>
    public static class TagBuilderEx
    {
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="tag">本体</param>
        /// <param name="attr">属性名</param>
        /// <param name="value">值</param>
        /// <returns>返回本体</returns>
        public static TagBuilder UwtAppendAttrbite(this TagBuilder tag, string attr, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                tag.Attributes.Add(attr, value);
            }
            return tag;
        }
    }
}
