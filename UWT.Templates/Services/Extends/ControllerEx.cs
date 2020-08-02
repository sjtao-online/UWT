using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Services.Auths;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 控制器扩展方法
    /// </summary>
    public static class ControllerEx
    {
        #region 日志输出
        /// <summary>
        /// Controller的Action输出被调用过
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="controller"></param>
        public static void ActionLog<TController>(this TController controller)
            where TController : ControllerBase
        {
            if (controller.Request.Query.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in controller.Request.Query)
                {
                    sb.Append(item.Key);
                    sb.Append("=");
                    sb.Append(item.Value);
                    sb.Append("&");
                }
                sb = sb.Remove(sb.Length - 1, 1);
                controller.GetLogger().Log(Microsoft.Extensions.Logging.LogLevel.Information, $"call {controller.Request.Method}: {controller.Request.Path} ? {sb}");
            }
            else
            {
                controller.GetLogger().Log(Microsoft.Extensions.Logging.LogLevel.Information, $"call {controller.Request.Method}: {controller.Request.Path} ? []");
            }
        }
        /// <summary>
        /// 切换布局文件
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="layout"></param>
        public static void ChangeLayout(this Controller controller, string layout)
        {
            if (controller.ViewData.ContainsKey(RazorPageEx.CustomLayoutKey))
            {
                controller.ViewData.Remove(RazorPageEx.CustomLayoutKey);
            }
            controller.ViewData.Add(RazorPageEx.CustomLayoutKey, layout);
        }
        #endregion

        #region 状态返回
        /// <summary>
        /// 只成功状态返回
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static object Success(this ControllerBase controller)
        {
            return ServiceCollectionEx.ApiResultBuildFunc("成功", 0);
        }
        /// <summary>
        /// 有数据的成功返回
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Success<TData>(this ControllerBase controller, TData data)
        {
            return ServiceCollectionEx.ApiResultBuildFuncT("成功", 0, data);
        }
        /// <summary>
        /// 未定义异常
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static object Error(this ControllerBase controller, string ErrorMsg)
        {
            return ServiceCollectionEx.ApiResultBuildFunc(ErrorMsg, -1);
        }
        /// <summary>
        /// 常规错误
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static object Error(this ControllerBase controller, UWT.Templates.Models.Basics.ErrorCode code, string msg = null)
        {
            int c = (int)code;
            string m = UWT.Templates.Models.Basics.ErrorCodeEx.GetErrorCodeMsg(code);
            if (!string.IsNullOrEmpty(msg))
            {
                m += ":" + msg;
            }
            return ServiceCollectionEx.ApiResultBuildFunc(m, c);
        }
        /// <summary>
        /// 常规错误 有实体
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="controller"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Error<TData>(this ControllerBase controller, UWT.Templates.Models.Basics.ErrorCode code, TData data)
        {
            int c = (int)code;
            string msg = UWT.Templates.Models.Basics.ErrorCodeEx.GetErrorCodeMsg(code);
            return ServiceCollectionEx.ApiResultBuildFuncT(msg, c, data);
        }
        /// <summary>
        /// 其它错误
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static object Error(this ControllerBase controller, string msg, int code)
        {
            return ServiceCollectionEx.ApiResultBuildFunc(msg, code);
        }
        /// <summary>
        /// 其它错误有实体
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Error<TData>(this ControllerBase controller, string msg, int code, TData data)
        {
            return ServiceCollectionEx.ApiResultBuildFuncT(msg, code, data);
        }
        #endregion

        #region 共享页面
        internal const string ItemNotFoundPageName = "/Views/Statics/ItemNotFound.cshtml";
        internal const string NotAuthorizedPageName = "/Views/Statics/NotAuthorized.cshtml";
        internal const string NotYourItemPageName = "/Views/Statics/NotYourItem.cshtml";
        /// <summary>
        /// 条目未找到
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IActionResult ItemNotFound(this Controller controller)
        {
            return controller.View(ItemNotFoundPageName);
        }
        /// <summary>
        /// 无权限
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IActionResult NotAuthorized(this Controller controller)
        {
            return controller.View(NotAuthorizedPageName);
        }
        /// <summary>
        /// 不是你的条目不可操作
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IActionResult NotYourItem(this Controller controller)
        {
            return controller.View(NotYourItemPageName);
        }
        /// <summary>
        /// 扩展View方法
        /// </summary>
        /// <typeparam name="TController">控制器</typeparam>
        /// <param name="controller">控制器</param>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        public static IActionResult ViewPlus<TController>(this TController controller, [CallerMemberName]string methodName = null)
            where TController : Controller
        {
            StringBuilder sb = new StringBuilder();
            var type = typeof(TController);
            if (type.Namespace.Contains("Areas"))
            {
                var nsarr = type.Namespace.Split('.');
                sb.Append("/Areas/");
                sb.Append(nsarr[nsarr.Length - 2]);
            }
            sb.Append("/Views/");
            var index = type.Name.IndexOf("Controller");
            if (index != -1)
            {
                sb.Append(type.Name.Substring(0, index));
            }
            sb.Append("/");
            sb.Append(methodName);
            sb.Append(".cshtml");
            return controller.View(sb.ToString());
        }
        #endregion

        #region 授权、登录、退出
        /// <summary>
        /// 获得指定项的int值
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key">项名称</param>
        /// <param name="defaultValue">无法转换或无项目的值</param>
        /// <returns></returns>
        public static int GetClaimValue(this ControllerBase controller, string key, int defaultValue)
        {
            return controller.mGetClaimVale(new List<string> { key }, defaultValue, int.TryParse);
        }
        /// <summary>
        /// 获得指定项的int值
        /// 由前到后依次查询
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="keys">项名称</param>
        /// <param name="defaultValue">无法转换或无项目的值</param>
        /// <returns></returns>
        public static int GetClaimValue(this ControllerBase controller, List<string> keys, int defaultValue)
        {
            return controller.mGetClaimVale(keys, defaultValue, int.TryParse);
        }
        /// <summary>
        /// 获得指定项的long值
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key">项名称</param>
        /// <param name="defaultValue">无法转换或无项目的值</param>
        /// <returns></returns>
        public static long GetClaimValue(this ControllerBase controller, string key, long defaultValue)
        {
            return controller.mGetClaimVale(new List<string> { key }, defaultValue, long.TryParse);
        }
        /// <summary>
        /// 获得指定项的int值
        /// 由前到后依次查询
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="keys">项名称</param>
        /// <param name="defaultValue">无法转换或无项目的值</param>
        /// <returns></returns>
        public static long GetClaimValue(this ControllerBase controller, List<string> keys, long defaultValue)
        {
            return controller.mGetClaimVale(keys, defaultValue, long.TryParse);
        }
        /// <summary>
        /// 获得指定项的字符串值
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="key">项名称</param>
        /// <returns></returns>
        public static string GetClaimValue(this ControllerBase controller, string key)
        {
            if (controller.HttpContext.User != null)
            {
                return controller.User.FindFirst(key)?.Value;
            }
            return null;
        }
        private static T mGetClaimVale<T>(this ControllerBase controller, List<string> keys, T defaultValue, ChangeTTryParse<T> func)
        {
            if (keys == null || keys.Count == 0)
            {
                return defaultValue;
            }
            if (controller.HttpContext.User != null)
            {
                foreach (var key in keys)
                {
                    string value = controller.User.FindFirst(key)?.Value;
                    if (value != null)
                    {
                        if (func(value, out T v))
                        {
                            return v;
                        }
                    }
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="pairs">登录缓存字典</param>
        /// <param name="authType">授权类型，对应Auth中的AuthType</param>
        /// <returns>返回Token</returns>
        public static string SignInto(this ControllerBase controller, Dictionary<string, string> pairs, string authType = null)
        {
            List<Claim> claims = new List<Claim>();
            foreach (var item in pairs)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }
            if (authType == null)
            {
                authType = CookieAuthHandler.CookieName;
            }
            claims.Add(new Claim(CookieAuthHandler.UwtAuthTypeKey, authType));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthHandler.CookieName));
            controller.HttpContext.SignInAsync(CookieAuthHandler.CookieName, claimsPrincipal).Wait();
            return controller.HttpContext.Items[CookieAuthHandler.CookieName] as string;
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="controller"></param>
        public static void SignOuto(this ControllerBase controller)
        {
            controller.HttpContext.SignOutAsync(CookieAuthHandler.CookieName);
        }
        delegate bool ChangeTTryParse<T>(string s, out T v);
        #endregion
    }
}
