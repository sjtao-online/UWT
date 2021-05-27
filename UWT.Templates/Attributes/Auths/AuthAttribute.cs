using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UWT.Templates.Controllers;
using UWT.Templates.Models.Basics;
using UWT.Templates.Services.Auths;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Attributes.Auths
{
    /// <summary>
    /// 登录授权特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        internal static string LoginUrl = "/Accounts/Login";
        internal static string RefText = "Ref";
        /// <summary>
        /// 类型
        /// </summary>
        public AuthRequestType Type 
        {
            get
            {
                if (saveType == null)
                {
                    saveType = Context.HttpContext.Request.Headers.ContainsKey(ErrorsController.ClientVersionText) ? AuthRequestType.Api : AuthRequestType.View;
                }
                return (AuthRequestType)saveType;
            }
        }
        private AuthRequestType? saveType;
        /// <summary>
        /// 授权类型
        /// </summary>
        public string AuthType { get; protected set; }
        /// <summary>
        /// 上下文
        /// </summary>
        protected AuthorizationFilterContext Context { get; private set; }
        /// <summary>
        /// 默认使用Cookies，视图
        /// </summary>
        public AuthAttribute()
        {
            AuthType = CookieAuthHandler.CookieName;
        }


        /// <summary>
        /// 判断是否已经登录
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> HasSignIn()
        {
            return await HasSignInUser(Context.HttpContext, AuthType);
        }
        /// <summary>
        /// 判断有无授权
        /// </summary>
        /// <returns></returns>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public virtual async Task<bool> HasAuthorized()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return true;
        }

        /// <summary>
        /// 处理未登录情况
        /// </summary>
        public virtual void HandleUnsignIn()
        {
            switch (Type)
            {
                case AuthRequestType.View:
                    HandleNoSignView(LoginUrl, RefText);
                    break;
                case AuthRequestType.Api:
                    HandleNoSignApi();
                    break;
                case AuthRequestType.Download:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理没权限的情况
        /// </summary>
        public virtual void HandleNotAuthorized()
        {
            switch (Type)
            {
                case AuthRequestType.View:
                    Context.Result = new ViewResult() { ViewName = ControllerEx.NotAuthorizedPageName };
                    break;
                case AuthRequestType.Api:
                    Context.Result = new JsonResult(ServiceCollectionEx.ApiResultBuildFunc("没权限", (int)ErrorCode.NotAuthorized));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理有权限的情况
        /// </summary>
        public virtual void HandleAuthorized()
        {
        }

        /// <summary>
        /// 权限过滤器
        /// </summary>
        /// <param name="context">上下文</param>
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            Context = context;
            //  是否已登录
            if (await HasSignIn())
            {
                //  是否有权限
                if (await HasAuthorized())
                {
                    //  处理有权限
                    HandleAuthorized();
                }
                else
                {
                    //  处理无权限
                    HandleNotAuthorized();
                }
            }
            else
            {
                //  处理未登录
                HandleUnsignIn();
            }
        }

        /// <summary>
        /// 处理未登录View
        /// </summary>
        /// <param name="url">登录界面URL</param>
        /// <param name="refParamName">重定向的参数名</param>
        /// <param name="otherParamters">其它参数</param>
        protected void HandleNoSignView(string url, string refParamName, List<KeyValuePair<string, string>> otherParamters = null)
        {
            StringBuilder urlBuilder = new StringBuilder(LoginUrl);
            urlBuilder.Append("?");
            urlBuilder.UwtAppend(refParamName, "{0}=" + WebUtility.UrlEncode(Context.HttpContext.GetRelativeUri()) + "&");
            if (otherParamters != null)
            {
                foreach (var item in otherParamters)
                {
                    urlBuilder.AppendFormat("{0}={1}", item.Key, WebUtility.HtmlEncode(item.Value));
                }
            }
            Context.Result = new RedirectResult(urlBuilder.ToString());
        }
        /// <summary>
        /// 处理未登录API
        /// </summary>
        protected void HandleNoSignApi()
        {
            Context.Result = new JsonResult(ServiceCollectionEx.ApiResultBuildFunc("登录已过期", (int)ErrorCode.Login_SessionExp));
        }

        /// <summary>
        /// 是否有用户登录
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="authType">授权类型</param>
        /// <returns></returns>
        public static async Task<bool> HasSignInUser(HttpContext context, string authType)
        {
            if (authType == null)
            {
                return false;
            }
            var auths = authType.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var ticket = await context.AuthenticateAsync(CookieAuthHandler.CookieName);
            if (ticket.Succeeded)
            {

                var claim = ticket.Principal.FindFirst(CookieAuthHandler.UwtAuthTypeKey);
                if (claim!= null && auths.Contains(claim.Value))
                {
                    return true;
                }
            }
            return false;
        }
    }
    /// <summary>
    /// 权限访问类型
    /// </summary>
    public enum AuthRequestType
    {
        /// <summary>
        /// View页面
        /// </summary>
        View,
        /// <summary>
        /// API接口
        /// </summary>
        Api,
        /// <summary>
        /// 下载文件<br/>
        /// 暂未使用
        /// </summary>
        Download
    }
}
