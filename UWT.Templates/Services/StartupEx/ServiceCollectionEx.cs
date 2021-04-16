using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Database;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Layouts;
using UWT.Templates.Services.Auths;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.Converts.Json;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.Filters;

namespace UWT.Templates.Services.StartupEx
{
    /// <summary>
    /// ServiceCollection扩展方法
    /// </summary>
    public static class ServiceCollectionEx
    {
        internal static FileManagerOptional FileManagerOptional { get; private set; } = new FileManagerOptional()
        {
            FilterFileNameParamter = "FileName",
            FilterFileTypeParamter = "FileType",
            GetFileListApi = "/Files/List",
            UploadApi = "/Files/Upload"
        };
        internal static Delegates.ModelId2Name ChooseId2Name;
        internal static Delegates.ModelIds2Names ChooseIds2Names;
        internal static Func<string, int, object, IResultModelBasicT> ApiResultBuildFuncT = (msg, code, data)=> new ResultModelBasicT() { Code = code, Msg = msg, Data = data };
        internal static Func<string, int, IResultModelBasic> ApiResultBuildFunc = (msg, code) => new ResultModelBasic() { Code = code, Msg = msg };
        internal static bool? LessServerMode = null;
        internal static void AddJson(this IMvcBuilder mvc)
        {
            mvc.AddJsonOptions(m =>
            {
                m.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                m.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                m.JsonSerializerOptions.ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip;
                m.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
                m.JsonSerializerOptions.Converters.Add(new TimeSpanNullConverter());
                ApiCustomFilter.JsonOptional = m;
            });
        }
        /// <summary>
        /// 添加Less支持
        /// </summary>
        /// <param name="service">服务</param>
        /// <param name="isServerMode">是否为服务模式</param>
        /// <returns></returns>
        public static IServiceCollection AddLess(this IServiceCollection service, bool isServerMode = false)
        {
            if (!LessServerMode.HasValue)
            {
                LessServerMode = isServerMode;
            }
            return service;
        }
        /// <summary>
        /// 添加静态字典
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dic"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IServiceCollection AddConstDictionary(this IServiceCollection service, Dictionary<string, string> dic, string assemblyName = null)
        {
            if (dic != null)
            {
                Dictionary<string, string> handleMap = null;
                if (assemblyName != null)
                {
                    if (TextEx.LibConstRDictionary.ContainsKey(assemblyName))
                    {
                        handleMap = TextEx.LibConstRDictionary[assemblyName];
                    }
                    else
                    {
                        handleMap = TextEx.LibConstRDictionary[assemblyName] = new Dictionary<string, string>();
                    }
                }
                else
                {
                    handleMap = TextEx.CommonConstRDictionary;
                }
                foreach (var item in dic)
                {
                    handleMap.TryAdd(item.Key, item.Value);
                }
            }
            return service;
        }
        /// <summary>
        /// 添加模型缓存
        /// </summary>
        /// <param name="service"></param>
        /// <param name="assemblies">支持的程序集，默认自动选择</param>
        /// <returns></returns>
        public static IServiceCollection AddTemplateModelCache(this IServiceCollection service, List<Assembly> assemblies = null)
        {
            service.AddSingleton<DebugRequestBodyMiddleware>();
            ModelCache.Init(assemblies);
            return service;
        }

        /// <summary>
        /// 添加Id转名称方法
        /// </summary>
        /// <param name="services"></param>
        /// <param name="id2Name"></param>
        /// <param name="ids2Names"></param>
        /// <returns></returns>
        public static IServiceCollection AddId2Name(this IServiceCollection services, Delegates.ModelId2Name id2Name, Delegates.ModelIds2Names ids2Names)
        {
            ChooseId2Name = id2Name;
            ChooseIds2Names = ids2Names;
            return services;
        }

        /// <summary>
        /// 添加文件管理
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileManager(this IServiceCollection services, Action<FileManagerOptional> action)
        {
            action(FileManagerOptional);
            return services;
        }
        /// <summary>
        /// 定制api接口返回模型
        /// </summary>
        /// <typeparam name="TResultBasic"></typeparam>
        /// <typeparam name="TResultBasicT"></typeparam>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiResultTemplate<TResultBasic, TResultBasicT>(this IServiceCollection service)
            where TResultBasic : IResultModelBasic, new ()
            where TResultBasicT : IResultModelBasicT, new ()
        {
            var mvc = service.AddControllers();
            mvc.AddJson();
            mvc.AddMvcOptions(op =>
            {
                op.Filters.Add<ApiCustomFilter>();
            });
            ApiResultBuildFuncT = (msg, code, data) =>
            {
                return new TResultBasicT()
                {
                    Code = code,
                    Msg = msg,
                    Data = data
                };
            };
            ApiResultBuildFunc = (msg, code) =>
            {
                return new TResultBasic()
                {
                    Code = code,
                    Msg = msg
                };
            };
            return service;
        }
        /// <summary>
        /// 添加模板授权相关
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loginurl">登录URL</param>
        /// <param name="refParamName">登录时之前URL的参数名</param>
        /// <param name="expTimeSpan">认证过期时间默认30分钟，不可设置小于1分钟的值，每次活动都会刷新认证过期时间</param>
        /// <returns></returns>
        public static IServiceCollection AddTemplateAuth(this IServiceCollection services, string loginurl, string refParamName, TimeSpan? expTimeSpan = null)
        {
            AuthAttribute.LoginUrl = loginurl;
            AuthAttribute.RefText = refParamName;
            if (!isAddedCookieAuthHandler)
            {
                isAddedCookieAuthHandler = true;
                services.AddAuthenticationCore(op =>
                {
                    op.AddScheme<CookieAuthHandler>(CookieAuthHandler.CookieName, CookieAuthHandler.CookieName);
                });
            }
            if (expTimeSpan != null && expTimeSpan > TimeSpan.FromMinutes(1))
            {
                CookieAuthHandler.TicketExpiresTimeSpan = expTimeSpan.Value;
            }
            services.AddMemoryCache();
            return services;
        }
        static bool isAddedCookieAuthHandler = false;
    }
    /// <summary>
    /// 上传文件相关
    /// </summary>
    public class FileManagerOptional
    {
        /// <summary>
        /// 上传文件接口
        /// </summary>
        public string UploadApi { get; set; }
        /// <summary>
        /// 获得文件列表接口
        /// </summary>
        public string GetFileListApi { get; set; }
        /// <summary>
        /// 获得文件列表时
        /// 筛选文件类型的参数名
        /// </summary>
        public string FilterFileTypeParamter { get; set; }
        /// <summary>
        /// 获得文件列表时
        /// 筛选文件名称的参数名
        /// </summary>
        public string FilterFileNameParamter { get; set; }
    }
    /// <summary>
    /// 委托
    /// </summary>
    public class Delegates
    {
        /// <summary>
        /// Id转换名称委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public delegate string ModelId2Name(string key, int id);
        /// <summary>
        /// Ids转换名称委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public delegate string ModelIds2Names(string key, List<int> ids);
    }
}
