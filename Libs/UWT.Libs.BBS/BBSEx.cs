using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.StartupEx;

namespace UWT.Libs.BBS
{
    /// <summary>
    /// 
    /// </summary>
    public static class BBSEx
    {
        internal static List<string> ThemeCssList = new List<string>();
        static HashSet<string> UwtThemeList = new HashSet<string>()
        {
            "red"
        };
        /// <summary>
        /// 添加论坛功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appendCss">附加的css</param>
        /// <returns></returns>
        public static IServiceCollection AddBBS(this IServiceCollection services, string appendCss)
        {
            string css = "";
            if (UwtThemeList.Contains(appendCss))
            {
                //  内置主题
#if DEBUG
                css = "/_content/uwt.libs.bbs" +
#else
                css = 
#endif
                      "/bbs/themes/" + appendCss + ".css";
            }
            else if (appendCss.StartsWith("//") || appendCss.ToLower().StartsWith("http://") || appendCss.ToLower().StartsWith("https://") || appendCss.StartsWith("/"))
            {
                //  直接的CSS文件名
                css = appendCss;
            }
            else
            {

            }
            if (!ThemeCssList.Contains(css))
            {
                ThemeCssList.Add(css);
            }
            return services;
        }
        /// <summary>
        /// 添加论坛功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appendCssList">附加CSS列表，用于更改论坛样式</param>
        /// <returns></returns>
        public static IServiceCollection AddBBS(this IServiceCollection services, List<string> appendCssList = null)
        {
            if (appendCssList != null && appendCssList.Count != 0)
            {
                foreach (var item in appendCssList)
                {
                    services.AddBBS(item);
                }
            }
            return services;
        }

        public static IApplicationBuilder UseBBS(this IApplicationBuilder app)
        {
            app.UseMgrRouteList(new List<Templates.Models.Basics.RouteModel>()
            {
                new Templates.Models.Basics.RouteModel()
                {
                    Area = "ForumMgr"
                }
            });
            using (var config = new StreamReader(Path.Combine(app.GetCurrentWebHost().ContentRootPath, "bbsconfig.json")))
            {

            }
            return app;
        }
    }
    class BbsConfigModel
    {
        public string BeianCode { get; set; }
        public string TitleFormat { get; set; }
    }
}
