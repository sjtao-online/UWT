using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UWT.Templates.Services.Converts
{
    /// <summary>
    /// 字符串模板转换器
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class StringTemplateConverter<TValue>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="map"></param>
        public StringTemplateConverter(Dictionary<string, TValue> map = null)
        {
            SafeMap = map ?? new Dictionary<string, TValue>();
        }
        /// <summary>
        /// 映射字典
        /// </summary>
        public Dictionary<string, TValue> SafeMap { get; private set; }
        /// <summary>
        /// 回调方法
        /// 用户处理特殊情况
        /// 默认不处理
        /// </summary>
        public StringTemplateCheckValue<TValue> CheckValue { get; set; }
        /// <summary>
        /// 替换占位符
        /// </summary>
        /// <param name="text">原文本</param>
        /// <returns>替换文本</returns>
        public string ReplacePlaceholder(string text)
        {
            string result = new string(text.ToCharArray());
            const string r = @"\$\{[\u4E00-\u9FA5A-Za-z_\$][\u4E00-\u9FA5A-Za-z0-9_]*\}";
            Regex regex = new Regex(r);
            if (CheckValue == null)
            {
                result = regex.Replace(result, DefaultMatchEval);
            }
            else
            {
                result = regex.Replace(result, CheckValueMatchEval);
            }
            var emptyRegexReplace = new Regex(@"\[.*" + r + @".*\]");
            result = emptyRegexReplace.Replace(result, string.Empty);
            return result;
        }
        private string CheckValueMatchEval(Match m)
        {
            var key = m.Value.Substring(2, m.Value.Length - 3);
            if (SafeMap.ContainsKey(key))
            {
                return CheckValue(key, SafeMap[key]);
            }
            else
            {
                return m.Value;
            }
        }
        private string DefaultMatchEval(Match m)
        {
            var key = m.Value.Substring(2, m.Value.Length - 3);
            if (SafeMap.ContainsKey(key))
            {
                return SafeMap[key]?.ToString();
            }
            else
            {
                return m.Value;
            }
        }
    }
    /// <summary>
    /// 默认值回调委托
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">占位符</param>
    /// <param name="value">值</param>
    /// <returns>新值</returns>
    public delegate string StringTemplateCheckValue<TValue>(string key, TValue value);
}
