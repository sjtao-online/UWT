using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Database;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Layouts;
using UWT.Templates.Services.Extends;
using Microsoft.AspNetCore.StaticFiles;

namespace UWT.Templates.Services.StartupEx
{
    /// <summary>
    /// ApplicationBuilder扩展
    /// </summary>
    public static class ApplicationBuilderEx
    {
        internal static IApplicationBuilder ApplicationBuilder = null;
        internal static Dictionary<string, List<RouteModel>> LayoutRouteList = new Dictionary<string, List<RouteModel>>();
        static int AdminLayoutIndex = 0;
        static string[] LayoutMgrKeys = new string[]
        {
            RazorPageEx.LayoutFileName_XAdmin,
            RazorPageEx.LayoutFileName_HPlus
        };

        static bool UsedLess = false;

        public static IApplicationBuilder UseLess(this IApplicationBuilder app)
        {
            UseCurrentAppBuilder(app);
            if (!UsedLess)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>()
                    {
                        [".less"] = "text/less"
                    })
                });
                UsedLess = true;
            }
            return app;
        }

        /// <summary>
        /// 设置管理布局路由
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeList"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMgrRouteList(this IApplicationBuilder app, List<RouteModel> routeList)
        {
            UseCurrentAppBuilder(app);
            AddRouteList(LayoutMgrKeys[AdminLayoutIndex], routeList);
            return app;
        }
        /// <summary>
        /// 设置帮助布局路由
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeList"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHelpRouteList(this IApplicationBuilder app, List<RouteModel> routeList)
        {
            UseCurrentAppBuilder(app);
            AddRouteList(RazorPageEx.LayoutFileName_Help, routeList);
            return app;
        }
        /// <summary>
        /// 设置其它布局文件路由模板
        /// </summary>
        /// <param name="app"></param>
        /// <param name="layoutFilename">布局文件</param>
        /// <param name="routeList">路由</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRouteList(this IApplicationBuilder app, string layoutFilename, List<RouteModel> routeList)
        {
            UseCurrentAppBuilder(app);
            AddRouteList(layoutFilename, routeList);
            return app;
        }
        /// <summary>
        /// 设置布局设置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeListCallback">适用默认的路由</param>
        /// <param name="defaultLayout">设置Layout的默认值</param>
        /// <param name="layoutCallback">设置实际调用中的回调</param>
        /// <returns></returns>
        public static IApplicationBuilder UseMgrLayout(this IApplicationBuilder app, Action<List<RouteModel>> routeListCallback, Action<LayoutModel> defaultLayout, Action<HttpContext, LayoutModel> layoutCallback)
        {
            UseCurrentAppBuilder(app);
            var routeList = new List<RouteModel>();
            routeListCallback(routeList);
            AddRouteList(LayoutMgrKeys[AdminLayoutIndex], routeList);
            defaultLayout(LayoutModel.DefaultLayout);
            LayoutModel.LayoutCallback = layoutCallback;
            return app;
        }
        /// <summary>
        /// 设置布局设置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="layoutRouteMap">通用性布局</param>
        /// <returns></returns>
        public static IApplicationBuilder UseMgrLayout(this IApplicationBuilder app, ILayoutRouteMap layoutRouteMap)
        {
            UseCurrentAppBuilder(app);
            AddRouteList(LayoutMgrKeys[AdminLayoutIndex], layoutRouteMap.RouteList);
            LayoutModel.DefaultLayout = layoutRouteMap.DefaultLayout;
            LayoutModel.LayoutCallback = (c, l) =>
            {
                layoutRouteMap.HttpContext2LayoutModel(c, ref l);
            };
            return app;
        }
        /// <summary>
        /// 添加数据库设置
        /// </summary>
        /// <typeparam name="TDb">数据类</typeparam>
        /// <param name="app"></param>
        /// <param name="setting">数据库配置</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDbSettings<TDb>(this IApplicationBuilder app, DbSettingModel setting)
            where TDb : LinqToDB.Data.DataConnection, new()
        {
            UseCurrentAppBuilder(app);
            if (setting.Format == null)
            {
                setting.Format = "Database={0};Data Source={1};Port={2};User={3};Password={4};Charset={5}";
            }
            UseDbSettings<TDb>(app, setting.DbType, string.Format(setting.Format, setting.DbName, setting.Server, setting.Port, setting.User, setting.Pwd, setting.Charset));
            return app;
        }
        /// <summary>
        /// 添加数据库设置
        /// </summary>
        /// <typeparam name="TDb">数据库类</typeparam>
        /// <param name="app"></param>
        /// <param name="dbProvider">数据库类型</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDbSettings<TDb>(this IApplicationBuilder app, string dbProvider, string connectionString)
            where TDb : LinqToDB.Data.DataConnection, new()
        {
            UseCurrentAppBuilder(app);
            LinqToDB.Data.DataConnection.DefaultSettings = new LinqDbSettings(dbProvider, connectionString);
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            DbCreator.CreateDb = () => new TDb();
            return app;
        }
        /// <summary>
        /// 添加数据库设置
        /// </summary>
        /// <typeparam name="TDb">数据类</typeparam>
        /// <param name="app"></param>
        /// <param name="configFilename">配置文件(json文件：UWT.Templates.Models.Database.DbConfig)</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDbSettings<TDb>(this IApplicationBuilder app, string configFilename)
            where TDb : LinqToDB.Data.DataConnection, new()
        {
            UseCurrentAppBuilder(app);
            using (StreamReader sr = new StreamReader(configFilename))
            {
                var config = System.Text.Json.JsonSerializer.Deserialize<DbConfig>(sr.ReadToEnd(), new System.Text.Json.JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip
                });
                if (config != null && config.DbSettings != null && config.DbSettings.ContainsKey(config.Current))
                {
                    UseDbSettings<TDb>(app, config.DbSettings[config.Current]);
                }
            }
            return app;
        }
        /// <summary>
        /// 使用HPlus后台样式
        /// </summary>
        /// <param name="app"></param>
        public static void UseLayoutHPlus(this IApplicationBuilder app)
        {
            UseCurrentAppBuilder(app);
            AdminLayoutIndex = 1;
        }
        /// <summary>
        /// 使用ACE后台样式
        /// </summary>
        /// <param name="app"></param>
        public static void UseLayoutAce(this IApplicationBuilder app)
        {
            UseCurrentAppBuilder(app);
            AdminLayoutIndex = 2;
        }
        static void AddRouteList(string layoutKey, List<RouteModel> routeList)
        {
            if (LayoutRouteList.ContainsKey(layoutKey))
            {
                LayoutRouteList[layoutKey].AddRange(routeList);
            }
            else
            {
                LayoutRouteList[layoutKey] = routeList;
            }
        }

        internal static void UseCurrentAppBuilder(IApplicationBuilder app)
        {
            if (ApplicationBuilder == null)
            {
                ApplicationBuilder = app;
            }
        }
    }
}
