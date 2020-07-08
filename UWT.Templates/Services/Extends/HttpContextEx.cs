using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpContextEx
    {
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
