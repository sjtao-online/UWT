using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UWT.Libs.WeChats.Models
{
    /// <summary>
    /// 微信登录模型
    /// </summary>
    public class WxLoginModel
    {
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoModel UserInfo { get; set; }
    }
    /// <summary>
    /// 用户信息模型
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// Id 内部使用
        /// </summary>
        [JsonIgnore]
        internal int Id { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 城镇
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 不应该
        /// </summary>
        public string Language { get; set; }
    }
}
