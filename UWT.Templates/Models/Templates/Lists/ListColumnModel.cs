using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Models.Templates.TagHelpers;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Models.Templates.Lists
{
    class ListColumnModel : IListColumnModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否忽略此列(也就是不为此项生成列)
        /// </summary>
        public bool IsIgnore { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public ColumnType ColumnType { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public System.Reflection.PropertyInfo Property { get; set; }
        /// <summary>
        /// 样式属性
        /// </summary>
        public string Styles { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; internal set; }
        /// <summary>
        /// 宽度信息
        /// </summary>
        public ICellWidth Width { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public IListItemExBasicModel ModelEx { get; set; }
        public IHtmlContent GetRawValue(object item, ref TagHelperTemplateModel tagHelperTemplateModel, IHtmlHelper html)
        {
            var value = Property.GetValue(item);
            if (value == null)
            {
                return new StringHtmlContent("");
            }
            switch (ColumnType)
            {
                case ColumnType.Link:
                    var a = new TagBuilder("A");
                    if (Property.PropertyType == typeof(ListColumnLinkModel))
                    {
                        var lm = value as ListColumnLinkModel;
                        a.Attributes.Add("href", lm.Url);
                        a.InnerHtml.Append(lm.Title);
                        a.UwtAppendAttrbite("title", lm.Tooltip);
                        if (lm.IsNewTableOpen)
                        {
                            a.Attributes.Add("target", "_blank");
                        }
                        return a;
                    }
                    else
                    {
                        a.Attributes.Add("href", value.ToString());
                        a.InnerHtml.Append(value.ToString());
                        return a;
                    }
                case ColumnType.Text:
                    if (Property.PropertyType == typeof(ListColumnTextModel))
                    {
                        var tm = value as ListColumnTextModel;
                        TagBuilder span = new TagBuilder("SPAN");
                        span.InnerHtml.Append(tm.Title);
                        StringBuilder sb = new StringBuilder();
                        sb.UwtAppend(tm.Background, "background: '{0}';");
                        sb.UwtAppend(tm.BorderRadius, "border-radius: '{0}';");
                        sb.UwtAppend(tm.Padding, "padding: '{0}';");
                        sb.UwtAppend(tm.FontColor, "color: '{0}';");
                        span.UwtAppendAttrbite("style", sb.ToString());
                        return span;
                    }
                    return new StringHtmlContent(value.ToString());
                case ColumnType.Summary:
                    return new StringHtmlContent(value.ToString());
                case ColumnType.Image:
                    return html.Raw($"<img style='height: 20px' src='{value}'>");
                case ColumnType.Handle:
                    if (Property.PropertyType == typeof(List<HandleModel>))
                    {
                        StringBuilder builder = new StringBuilder("<div class='hidden-sm hidden-xs btn-group'>");
                        foreach (var itemHandle in value as List<HandleModel>)
                        {
                            builder.Append(HandleString(itemHandle, ref tagHelperTemplateModel));
                        }
                        builder.Append("</div>");
                        return html.Raw(builder.ToString());
                    }
                    else if (Property.PropertyType == typeof(HandleModel))
                    {
                        return html.Raw(HandleString(value as HandleModel, ref tagHelperTemplateModel));
                    }
                    break;
                case ColumnType.Cshtml:
                    var cshtml = ModelEx as ICshtmlEx;
                    return html.Partial(cshtml.CshtmlPath, value);
                default:
                    break;
            }
            return new StringHtmlContent((string)Property.GetValue(item));
        }

        private string HandleString(HandleModel handleModelBasic, ref TagHelperTemplateModel tagHelperTemplateModel)
        {
            string target = "";
            switch (handleModelBasic.Type)
            {
                case HandleType.EvalJS:
                    var guid = Guid.NewGuid().ToStringZ2();
                    if (tagHelperTemplateModel.ScriptList == null)
                    {
                        tagHelperTemplateModel.ScriptList = new List<string>();
                    }
                    tagHelperTemplateModel.ScriptList.Add($"function func{guid}(){{{ModelCache.RechangeUrl(Property.ReflectedType, (string)handleModelBasic.Target)}}}");
                    target = guid;
                    break;
                case HandleType.PopupDlg:
                case HandleType.Download:
                    target = RechangeDictionaryUrl(handleModelBasic.Target, Property.ReflectedType);
                    break;
                case HandleType.Navigate:
                case HandleType.ApiGet:
                case HandleType.ApiPost:
                    target = ModelCache.RechangeUrl(Property.ReflectedType, (string)handleModelBasic.Target);
                    break;
                case HandleType.Comfirm:
                case HandleType.MultiButtons:
                    target = LoopRechangeTarget2(handleModelBasic.Target, Property.ReflectedType);
                    break;
                default:
                    break;
            }
            return BuildButton(handleModelBasic, target);
        }

        string BuildButton(HandleModel handle, string target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<button type='button'");
            sb.Append("class='layui-btn layui-btn-xs uwt-handle-item ");
            sb.UwtAppend(handle.Class);
            sb.Append("'");
            sb.UwtAppend(target, "data-target='{0}'");
            sb.UwtAppend(handle.Styles, " style='{0}'");
            sb.UwtAppend(handle.Tooltip, " title='{0}'");
            sb.UwtAppend(handle.Type.ToString(), "data-type='{0}'");
            sb.UwtAppend(handle.AskContent, "data-ask='{0}'");
            sb.Append(">");
            sb.Append(handle.Title);
            sb.Append("</button>");
            return sb.ToString();
        }

        private string RechangeDictionaryUrl(object target, Type reflectedType)
        {
            var dic = (Dictionary<string, string>)target;
            dic["url"] = ModelCache.RechangeUrl(reflectedType, dic["url"]);
            return JsonSerializer.Serialize(dic);
        }

        private string LoopRechangeTarget2(object target, Type reflectedType)
        {
            List<HandleModel> childrens = (List<HandleModel>)target;
            foreach (var item in childrens)
            {
                if (item.Type == HandleType.Comfirm || item.Type == HandleType.MultiButtons)
                {
                    item.Target = LoopRechangeTarget2(item.Target, reflectedType);
                }
                else if (item.Type == HandleType.PopupDlg)
                {
                    item.Target = RechangeDictionaryUrl(item.Target, reflectedType);
                }
                else
                {
                    item.Target = ModelCache.RechangeUrl(reflectedType, (string)item.Target);
                }
            }
            return JsonSerializer.Serialize(childrens);
        }
    }
}
