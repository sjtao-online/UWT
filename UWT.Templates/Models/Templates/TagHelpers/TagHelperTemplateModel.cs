using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.TagHelpers
{
    /// <summary>
    /// 模板模型
    /// </summary>
    public class TagHelperTemplateModel
    {
        /// <summary>
        /// 自定义信息
        /// </summary>
        public Dictionary<string, string> CustomPairs { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// JS文件添加列表
        /// </summary>
        public List<Dictionary<string, string>> AddJsList { get; set; } = new List<Dictionary<string, string>>();
        /// <summary>
        /// CSS文件添加列表
        /// </summary>
        public List<Dictionary<string, string>> AddCssList { get; set; } = new List<Dictionary<string, string>>();
        /// <summary>
        /// JS文本列表
        /// </summary>
        public List<string> ScriptList { get; set; } = new List<string>();
        /// <summary>
        /// 渲染脚本列表
        /// </summary>
        /// <returns></returns>
        public IHtmlContent RenderScriptList()
        {
            if (ScriptList.Count == 0)
            {
                return new HtmlString("");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            foreach (var item in ScriptList)
            {
                sb.Append(item);
            }
            sb.Append("</script>");
            return new HtmlString(sb.ToString());
        }
        /// <summary>
        /// 渲染添加的脚本文件
        /// </summary>
        /// <returns></returns>
        public IHtmlContent RenderAddJSList()
        {
            HtmlContentBuilder list = new HtmlContentBuilder();
            foreach (var item in AddJsList)
            {
                if (item.ContainsKey(""))
                {
                    item.Add("src", item[""]);
                    item.Remove("");
                }
                if (!item.ContainsKey("src"))
                {
                    continue;
                }
                if (!item.ContainsKey("type"))
                {
                    item.Add("type", "text/javascript");
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                TagBuilder builder = new TagBuilder("script");
                foreach (var attr in item)
                {
                    builder.Attributes.Add(attr.Key, attr.Value);
                }
                list.AppendHtml(builder);
            }
            return list;
        }
        /// <summary>
        /// 渲染添加的CSS文件
        /// </summary>
        /// <returns></returns>
        public IHtmlContent RenderAddCSSList()
        {
            HtmlContentBuilder list = new HtmlContentBuilder();
            foreach (var item in AddCssList)
            {
                if (item.ContainsKey(""))
                {
                    item.Add("href", item[""]);
                    item.Remove("");
                }
                if (!item.ContainsKey("href"))
                {
                    continue;
                }
                if (!item.ContainsKey("rel"))
                {
                    item.Add("rel", "stylesheet");
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                TagBuilder builder = new TagBuilder("link");
                foreach (var attr in item)
                {
                    builder.Attributes.Add(attr.Key, attr.Value);
                }
                list.AppendHtml(builder);
            }
            return list;
        }
    }
}
