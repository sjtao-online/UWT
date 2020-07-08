using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UWT.Templates.Services.Converts.Json;

namespace UWT.Libs.WeChats.Models
{
    /// <summary>
    /// 微信登录返回信息模型
    /// </summary>
    public class SignIntoModel
    {
        /// <summary>
        /// 登录Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新用Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Token过期时间
        /// </summary>
        [JsonConverter(typeof(DateTimeSecondsConverter))]
        public DateTime TokenExpiry { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoModel UserInfo { get; set; }
    }
}
