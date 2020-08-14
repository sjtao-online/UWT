using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UWT.Templates.Services.Auths;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpContextEx
    {
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="pairs"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        public static string SignInto(this HttpContext httpContext, Dictionary<string, string> pairs, string authType = null)
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
            httpContext.SignInAsync(CookieAuthHandler.CookieName, claimsPrincipal).Wait();
            return httpContext.Items[CookieAuthHandler.CookieName] as string;
        }
        /// <summary>
        /// 获得绝对URI
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpContext context)
        {
            return new StringBuilder()
                .Append(context.Request.Scheme)
                .Append("://")
                .Append(context.Request.Host)
                .Append(context.Request.PathBase)
                .Append(context.Request.Path)
                .Append(context.Request.QueryString)
                .ToString();
        }
        /// <summary>
        /// 获得相对URI
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRelativeUri(this HttpContext context)
        {
            return new StringBuilder()
                .Append(context.Request.PathBase)
                .Append(context.Request.Path)
                .Append(context.Request.QueryString)
                .ToString();
        }
    }
}
