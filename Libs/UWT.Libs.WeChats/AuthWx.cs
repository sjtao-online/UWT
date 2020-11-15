using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.WeChats
{
    /// <summary>
    /// 微信授权特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class AuthWxAttribute : AuthAttribute
    {
        internal const string CurrentAuthType = "wechat";
        /// <summary>
        /// 微信授权特性
        /// </summary>
        public AuthWxAttribute()
        {
            AuthType = CurrentAuthType;
        }
    }
}
