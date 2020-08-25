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
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件名，不要扩展名
        /// </summary>
        public string FilenameNoExt { get; set; }
        /// <summary>
        /// 是否服务器模式<br/>
        /// 不设置
        /// </summary>
        public bool? IsServerMode { get; set; }
        static HashSet<string> Complied = new HashSet<string>();
        static MethodInfo DotlessMethod = null;
        public LessLinkerTagHelper(IHtmlHelper html)
        {
            Html = html;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "link";
            if (IsServerMode.HasValue ? (bool)IsServerMode : (ServiceCollectionEx.LessServerMode??false))
            {
                if (!Complied.Contains(Path + FilenameNoExt))
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
                        if (filePath[filePath.Length-1] != '\\')
                        {
                            filePath += '\\';
                        }
                        filePath += Path;
                        if (filePath[filePath.Length - 1] != '\\')
                        {
                            filePath += '\\';
                        }
                        filePath += FilenameNoExt;
                        filePath = ReplaceFilePath(filePath);
                        using (var sr = new StreamReader(filePath + ".less"))
                        {
                            var lessContent = sr.ReadToEnd();
                            var cssText = DotlessMethod.Invoke(null, new object[] { lessContent });
                            using (var sw = new StreamWriter(filePath + ".css"))
                            {
                                sw.Write(cssText);
                            }
                        }
                        Complied.Add(Path + FilenameNoExt);
                    }
                }
                output.Attributes.Add("href", Path + FilenameNoExt + ".css");
                output.Attributes.Add("rel", "stylesheet");
                output.Attributes.Add("type", "text/css");
            }
            else
            {
                RenderClientMode(output);
                (Html as HtmlHelper).Contextualize(ViewContext);
                const string hasLess = "__has_less";
                if (!ViewContext.HttpContext.Items.ContainsKey(hasLess))
                {
                    ViewContext.HttpContext.Items.Add(hasLess, true);
                }
            }
        }

        private string ReplaceFilePath(string filePath)
        {
            filePath = filePath.Replace('/', '\\');
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
            output.Attributes.Add("href", Path + FilenameNoExt + ".less");
            output.Attributes.Add("rel", "stylesheet/less");
        }
    }
}
