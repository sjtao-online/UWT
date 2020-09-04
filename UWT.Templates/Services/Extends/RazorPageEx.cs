using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Layouts;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 页面扩展
    /// </summary>
    public static class RazorPageEx
    {
        /// <summary>
        /// 默认布局
        /// </summary>
        internal const string LayoutFileName_HPlus = "/Views/Layouts/_LayoutUWT_HPlus.cshtml";
        /// <summary>
        /// HPlus风格
        /// </summary>
        internal const string LayoutFileName_XAdmin = "/Views/Layouts/_LayoutUWT_XAdmin.cshtml";
        /// <summary>
        /// ACE风格
        /// </summary>
        internal const string LayoutFileName_Ace = "/Views/Layouts/_LayoutUWT_Ace.cshtml";
        /// <summary>
        /// 帮助布局
        /// </summary>
        internal const string LayoutFileName_Help = "/Views/Layouts/_LayoutHelper.cshtml";
        internal const string CustomLayoutKey = "__temp_custom_layout_key__";
        /// <summary>
        /// 获得资源的相对目录
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="razor"></param>
        /// <returns></returns>
        public static string GetResourcePath<TModel>(this RazorPage<TModel> razor)
        {
#if DEBUG
            return "/_content/" + Assembly.GetExecutingAssembly().GetName().Name;
#else
            return string.Empty;
#endif
        }
        /// <summary>
        /// 获得文件管理相关信息
        /// </summary>
        /// <param name="razor"></param>
        /// <returns></returns>
        public static FileManagerOptional GetFileManagerOptional(this RazorPage razor)
        {
            return ServiceCollectionEx.FileManagerOptional;
        }
        /// <summary>
        /// object转换为可区间的类型
        /// </summary>
        /// <typeparam name="TRangeValue"></typeparam>
        /// <param name="razor"></param>
        /// <param name="r"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static Range<TRangeValue> ChangeToRangeT<TRangeValue>(this RazorPage razor, object r, PropertyInfo propertyInfo)
        {
            if (r != null && propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GenericTypeArguments.Length == 1)
            {
                return new Range<TRangeValue>()
                {
                    Max = (TRangeValue)((dynamic)propertyInfo.GetValue(r)).Max,
                    Min = (TRangeValue)((dynamic)propertyInfo.GetValue(r)).Min,
                };
            }
            return null;
        }
        /// <summary>
        /// 获得当前布局信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="razor"></param>
        /// <returns></returns>
        public static LayoutModel GetCurrentLayoutModel<TModel>(this RazorPage<TModel> razor)
        {
            return LayoutModel.GetCurrentLayout(razor.Context);
        }
        /// <summary>
        /// 渲染主题
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="razor"></param>
        /// <returns></returns>
        public static IHtmlContent RenderTheme<TModel>(this RazorPage<TModel> razor)
        {
            var layout = razor.GetCurrentLayoutModel();
            if (!string.IsNullOrEmpty(layout.Skin))
            {
                string skinCssFileName = "";
                if (layout.Skin.StartsWith("theme"))
                {
                    skinCssFileName = string.Format("{0}/admins/css/{1}.css", razor.GetResourcePath(), layout.Skin);
                }
                else
                {
                    skinCssFileName = layout.Skin;
                }
                var tag = new TagBuilder(HtmlConst.LINK);
                tag.Attributes.Add(HtmlConst.REL, HtmlConst.STYLESHEET);
                tag.Attributes.Add(HtmlConst.HREF, skinCssFileName);
                return tag;
            }
            return new StringHtmlContent("");
        }
        /// <summary>
        /// viewstart中调用
        /// </summary>
        /// <param name="razor"></param>
        public static void ViewStartCallback(this RazorPage razor)
        {
            if (razor.ViewContext.ViewData.ContainsKey(CustomLayoutKey))
            {
                razor.Layout = razor.ViewContext.ViewData[CustomLayoutKey] as string;
                return;
            }
            foreach (var item in ApplicationBuilderEx.LayoutRouteList)
            {
                foreach (var route in item.Value)
                {
                    bool? r = null;
                    foreach (var k in route.RouteMap)
                    {
                        if (k.Value == null)
                        {
                            continue;
                        }
                        r = razor.ViewContext.RouteData.Values.ContainsKey(k.Key) && (razor.ViewContext.RouteData.Values[k.Key] as string) == k.Value;
                        if (r ?? false)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (r ?? false)
                    {
                        razor.Layout = item.Key;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="this"></param>
        /// <param name="src"></param>
        /// <param name="rsMap"></param>
        public static void AddResourcs(this List<Dictionary<string, string>> @this, string src, Dictionary<string, string> rsMap = null)
        {
            foreach (var item in @this)
            {
                if (item[""] == src)
                {
                    return;
                }
            }
            if (rsMap == null)
            {
                rsMap = new Dictionary<string, string>();
            }
            rsMap.Add("", src);
            @this.Add(rsMap);
        }

        /// <summary>
        /// 渲染附加JS
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="razorPage"></param>
        /// <returns></returns>
        public static IHtmlContent RenderAppendJS<TModel>(this RazorPage<TModel> razorPage)
        {
            return RenderAppend(razorPage, TemplateControllerEx.AppendJsList);
        }
        /// <summary>
        /// 渲染附加CSS
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="razorPage"></param>
        /// <returns></returns>
        public static IHtmlContent RenderAppendCSS<TModel>(this RazorPage<TModel> razorPage)
        {
            return RenderAppend(razorPage, TemplateControllerEx.AppendCssList);
        }
        private static IHtmlContent RenderAppend<TModel>(RazorPage<TModel> razorPage, string key)
        {
            if (razorPage.ViewData.ContainsKey(key))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in razorPage.ViewData[key] as List<string>)
                {
                    sb.Append(item);
                }
                return new HtmlString(sb.ToString());
            }
            else
            {
                return new HtmlString("");
            }
        }
    }
}
