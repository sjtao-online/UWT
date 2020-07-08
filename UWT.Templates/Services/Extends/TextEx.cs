using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Services.Converts;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 转换文本
    /// </summary>
    public static class TextEx
    {
        /// <summary>
        /// 通用字典
        /// </summary>
        internal static Dictionary<string, string> CommonConstRDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 库专用字典
        /// </summary>
        internal static Dictionary<string, Dictionary<string, string>> LibConstRDictionary = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// 计算文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string RCalcText(this string text, string assembly = null)
        {
            if (text == null)
            {
                return null;
            }
            if (text == string.Empty)
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(assembly) && LibConstRDictionary.ContainsKey(assembly))
            {
                var st = new StringTemplateConverter<string>(LibConstRDictionary[assembly]);
                text = st.ReplacePlaceholder(text);
            }
            var stcom = new StringTemplateConverter<string>(CommonConstRDictionary);
            return stcom.ReplacePlaceholder(text);
        }
    }
}
