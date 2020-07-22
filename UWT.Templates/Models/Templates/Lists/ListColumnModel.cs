using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
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
        /// 目标
        /// </summary>
        public string Target { get; internal set; }
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
                case ColumnType.Text:
                    return new StringHtmlContent(value.ToString());
                case ColumnType.Summary:
                    return new StringHtmlContent(value.ToString());
                case ColumnType.Image:
                    return html.Raw($"<img style='height: 20px' src='{value}'>");
                case ColumnType.Handle:
                    if (Property.PropertyType == typeof(HandleModel))
                    {
                        return html.Raw(HandleString(value as HandleModel, ref tagHelperTemplateModel));
                    }
                    else if (Property.PropertyType == typeof(List<HandleModel>))
                    {
                        StringBuilder builder = new StringBuilder("<div class='hidden-sm hidden-xs btn-group'>");
                        foreach (var itemHandle in value as List<HandleModel>)
                        {
                            builder.Append(HandleString(itemHandle, ref tagHelperTemplateModel));
                        }
                        builder.Append("</div>");
                        return html.Raw(builder.ToString());
                    }
                    else if (Property.PropertyType == typeof(HandleModelBasic))
                    {
                        return html.Raw(HandleString(value as HandleModelBasic, ref tagHelperTemplateModel));
                    }
                    else if (Property.PropertyType == typeof(List<HandleModelBasic>))
                    {
                        StringBuilder builder = new StringBuilder("<div class='hidden-sm hidden-xs btn-group'>");
                        foreach (var itemHandle in value as List<HandleModelBasic>)
                        {
                            builder.Append(HandleString(itemHandle, ref tagHelperTemplateModel));
                        }
                        builder.Append("</div>");
                        return html.Raw(builder.ToString());
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

        private string HandleString(HandleModelBasic handleModelBasic, ref TagHelperTemplateModel tagHelperTemplateModel)
        {
            string target = "";
            switch (handleModelBasic.Type)
            {
                case HandleType.EvalJS:
                    var uwtid = Uwtid.NewUwtid().ToStringZ2();
                    if (tagHelperTemplateModel.ScriptList == null)
                    {
                        tagHelperTemplateModel.ScriptList = new List<string>();
                    }
                    tagHelperTemplateModel.ScriptList.Add($"function func{uwtid}(){{{ModelCache.RechangeUrl(Property.ReflectedType, handleModelBasic.Target)}}}");
                    target = uwtid;
                    break;
                case HandleType.PopupDlg:
                    target = PopupDlgRechange(handleModelBasic.Target, Property.ReflectedType);
                    break;
                case HandleType.Navigate:
                case HandleType.ApiGet:
                case HandleType.ApiPost:
                case HandleType.Download:
                    target = ModelCache.RechangeUrl(Property.ReflectedType, handleModelBasic.Target);
                    break;
                case HandleType.Comfirm:
                    target = LoopRechangeTarget(handleModelBasic.Target, Property.ReflectedType);
                    break;
                default:
                    break;
            }
            return BuildButton(handleModelBasic, target);
        }

        string BuildButton(HandleModelBasic handle, string target)
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

        string HandleString(HandleModel itemHandle, ref TagHelperTemplateModel tagHelperTemplateModel)
        {
            string target = ModelCache.RechangeUrl(Property.ReflectedType, itemHandle.Target);
            switch (itemHandle.Type.ToLower())
            {
                case HandleModel.TypeTagEvalJS:
                    var uwtid = Uwtid.NewUwtid().ToStringZ2();
                    if (tagHelperTemplateModel.ScriptList == null)
                    {
                        tagHelperTemplateModel.ScriptList = new List<string>();
                    }
                    tagHelperTemplateModel.ScriptList.Add($"function func{uwtid}(){{{target}}}");
                    return $"<button type='button' onclick='javascript:func{uwtid}()' class='layui-btn layui-btn-xs {itemHandle.Class}'>{itemHandle.Title}</button>";
                case HandleModel.TypeTagDownload:
                    if (string.IsNullOrEmpty(target))
                    {
                        return $"<a class='layui-btn layui-btn-xs' disabled>{itemHandle.Title}</a>";
                    }
                    return $"<a href='{target}' target='_blank' download='{itemHandle.AskTooltip}' class='layui-btn layui-btn-xs {itemHandle.Class}'>{itemHandle.Title}</a>";
                case HandleModel.TypeTagComfirm:
                    target = LoopRechangeTarget(target, Property.ReflectedType);
                    return $"<button type='button' data-type='{itemHandle.Type}' data-ask='{itemHandle.AskTooltip}' data-target='{target}' class='layui-btn layui-btn-xs {itemHandle.Class} handle-item'>{itemHandle.Title}</button>";
                default:
                    return $"<button type='button' data-type='{itemHandle.Type}' data-ask='{itemHandle.AskTooltip}' data-target='{target}' class='layui-btn layui-btn-xs {itemHandle.Class} handle-item'>{itemHandle.Title}</button>";
            }
        }

        private string PopupDlgRechange(string target, Type reflectedType)
        {
            var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(target);
            dic["url"] = ModelCache.RechangeUrl(reflectedType, dic["url"]);
            return JsonSerializer.Serialize(dic);
        }

        private string LoopRechangeTarget(string target, Type reflectedType)
        {
            List<ChildrenHandleModel> childrens = JsonSerializer.Deserialize<List<ChildrenHandleModel>>(target);
            foreach (var item in childrens)
            {
                if (item.Type == HandleModel.TypeTagComfirm)
                {
                    item.Target = LoopRechangeTarget(item.Target, reflectedType);
                }
                else
                {
                    item.Target = ModelCache.RechangeUrl(reflectedType, item.Target);
                }
            }
            return JsonSerializer.Serialize(childrens);
        }
    }
}
