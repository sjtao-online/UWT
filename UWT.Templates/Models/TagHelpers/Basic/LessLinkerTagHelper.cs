using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;
using UWT.Templates.Models.Consts;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Models.TagHelpers.Basic
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("link-less")]
    public class LessLinkerTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        IHtmlHelper Html { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public string Href { get; set; }
        /// <summary>
        /// 是否服务器模式<br/>
        /// 不设置
        /// </summary>
        public bool? IsServerMode { get; set; }
        static HashSet<string> Complied = new HashSet<string>();
        static MethodInfo DotlessMethod = null;
        internal const string hasLess = "__has_less";
        const string ExtCss = ".css";
        const string ExtLess = ".less";
        const char PathBlank = '\\';
        static bool? isDev = null;
        static bool IsDev
        {
            get
            {
                if (isDev == null)
                {
                    isDev = isDev.GetCurrentWebHost().IsDevelopment();
                }
                return isDev.Value;
            }
        }
        public LessLinkerTagHelper(IHtmlHelper html)
        {
            Html = html;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = HtmlConst.LINK;
            if (IsServerMode.HasValue ? (bool)IsServerMode : (ServiceCollectionEx.LessServerMode??false))
            {
                if (IsDev || !Complied.Contains(Href))
                {
                    //  编译
                    if (DotlessMethod == null)
                    {
                        var ass = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "dotless.Core.dll");
                        var DotlessType = ass.GetType("dotless.Core.Less");
                        DotlessMethod = DotlessType.GetMethod("Parse", new Type[] { typeof(string) });
                    }
                    if (DotlessMethod == null)
                    {
                        RenderClientMode(output);
                        return;
                    }
                    else
                    {
                        string filePath = this.GetCurrentWebHost().WebRootPath;
                        if (filePath[filePath.Length-1] != PathBlank)
                        {
                            filePath += PathBlank;
                        }
                        filePath += Href;
                        filePath = ReplaceFilePath(filePath);
                        var dir = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        if (File.Exists(filePath))
                        {
                            using (var sr = new StreamReader(filePath))
                            {
                                WriteCss(sr, filePath);
                            }
                        }
                        else
                        {
                            using var stream = ModelCache.GetManifestResourceStream(Href);
                            using (var sr = new StreamReader(stream))
                            {
                                WriteCss(sr, filePath);
                            }
                        }
                        Complied.Add(Href);
                    }
                }
                output.Attributes.Add(HtmlConst.HREF, Href + ExtCss);
                output.Attributes.Add(HtmlConst.REL, HtmlConst.STYLESHEET);
                output.Attributes.Add(HtmlConst.TYPE, HtmlConst.TYPE_CSS);
            }
            else
            {
                RenderClientMode(output);
                (Html as HtmlHelper).Contextualize(ViewContext);
                if (!ViewContext.HttpContext.Items.ContainsKey(hasLess))
                {
                    ViewContext.HttpContext.Items.Add(hasLess, true);
                }
            }
        }

        private void WriteCss(StreamReader sr, string filePath)
        {
            var lessContent = sr.ReadToEnd();
            var cssText = DotlessMethod.Invoke(null, new object[] { lessContent });
            using (var sw = new StreamWriter(filePath + ExtCss))
            {
                sw.Write(cssText);
            }
        }

        private string ReplaceFilePath(string filePath)
        {
            filePath = filePath.Replace('/', PathBlank);
            while (true)
            {
                int len = filePath.Length;
                filePath = filePath.Replace("\\\\", "\\");
                if (len == filePath.Length)
                {
                    break;
                }
            }
            return filePath;
        }

        private void RenderClientMode(TagHelperOutput output)
        {
            output.Attributes.Add(HtmlConst.HREF, Href);
            output.Attributes.Add(HtmlConst.REL, HtmlConst.STYLESHEET_LESS);
        }
    }
}
