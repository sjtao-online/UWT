using LinqToDB.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
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
        internal static DataConnection GetDB(this IBBSService service)
        {
            return TemplateControllerEx.GetDB(null);
        }
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

        public static BbsConfigModel BbsConfigModel;

        public static IApplicationBuilder UseBBS(this IApplicationBuilder app)
        {
            app.UseMgrRouteList(new List<Templates.Models.Basics.RouteModel>()
            {
                new Templates.Models.Basics.RouteModel()
                {
                    Area = "ForumMgr"
                }
            });
            var configJson = Path.Combine(
#if DEBUG
                @"E:\Work\UWT\UWT.Server\bin\Debug\netcoreapp3.1"
#else
                app.GetCurrentWebHost().ContentRootPath
#endif
                , "bbsconfig.json");
            if (File.Exists(configJson))
            {
                using (var config = new StreamReader(configJson))
                {
                    BbsConfigModel = System.Text.Json.JsonSerializer.Deserialize<BbsConfigModel>(config.ReadToEnd());
                }
            }
            else
            {
                BbsConfigModel = new BbsConfigModel()
                {
                    Titles = new BbsTitleFormat()
                    {
                        MainFormat = "{0} - UWT论坛",
                        UserSpace = "{1} - {0}"
                    },
                    ForumName = "UWT论坛",
                    BeianCode = "",
                    Logo = ""
                };
            }
            return app;
        }
    }
    public interface IBBSService
    {

    }
    public class BbsConfigModel
    {
        [JsonPropertyName("beian-code")]
        public string BeianCode { get; set; }
        [JsonPropertyName("titles")]
        public BbsTitleFormat Titles { get; set; }
        [JsonPropertyName("forum-name")]
        public string ForumName { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
    public class BbsTitleFormat
    {
        [JsonPropertyName("main-format")]
        public string MainFormat { get; set; }
        [JsonPropertyName("user-space")]
        public string UserSpace { get; set; }
    }
}
