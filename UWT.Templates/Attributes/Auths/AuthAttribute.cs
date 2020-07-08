using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// View默认值
        /// </summary>
        public const string AuthDefaultTypeView = "https://sjtao.online/auth/managers/view";
        /// <summary>
        /// Api默认值
        /// </summary>
        public const string AuthDefaultTypeApi = "https://sjtao.online/auth/managers/api";
        internal static string LoginUrl = "/Accounts/Login";
        internal static string RefText = "Ref";
        /// <summary>
        /// 类型<br/>
        /// 已实现view、api
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 上下文
        /// </summary>
        protected AuthorizationFilterContext Context { get; private set; }
        /// <summary>
        /// 默认使用Cookies，视图
        /// </summary>
        public AuthAttribute()
        {
            Type = AuthDefaultTypeView;
        }

        /// <summary>
        /// 判断是否已经登录
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> HasSignIn()
        {
            return await HasSignInUser(Context.HttpContext);
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
                case AuthDefaultTypeView:
                    if (Context.HttpContext.Request.Headers.ContainsKey(ErrorsController.ClientVersionText))
                    {
                        Context.Result = new JsonResult(ServiceCollectionEx.ApiResultBuildFunc("登录已过期", (int)ErrorCode.Login_SessionExp));
                    }
                    else
                    {
                        Context.Result = new RedirectResult(LoginUrl + "?" + RefText + "=" + WebUtility.UrlEncode(Context.HttpContext.GetRelativeUri()));
                    }
                    break;
                case AuthDefaultTypeApi:
                    Context.Result = new JsonResult(ServiceCollectionEx.ApiResultBuildFunc("登录已过期", (int)ErrorCode.Login_SessionExp));
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
                case AuthDefaultTypeView:
                    Context.Result = new ViewResult() { ViewName = ControllerEx.NotAuthorizedPageName };
                    break;
                case AuthDefaultTypeApi:
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
        /// 是否有用户登录
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public static async Task<bool> HasSignInUser(HttpContext context)
        {
            return (await context.AuthenticateAsync(CookieAuthHandler.CookieName)).Succeeded;
        }
    }
}
