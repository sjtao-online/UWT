﻿using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Auths;

namespace UWT.Libs.WeChats
{
    /// <summary>
    /// 微信授权特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class AuthWxAttribute : AuthAttribute
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
