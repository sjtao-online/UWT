using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.WeChats.Models
{
    /// <summary>
    /// 微信用户表
    /// </summary>
    public interface IDbWxUserModel : IDbTableBase
    {
        /// <summary>
        /// 昵称
        /// </summary>
        string NickName { get; set; }
        /// <summary>
        /// 刷新登录
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// Token有效期
        /// </summary>
        DateTime TokenExp { get; set; }
        /// <summary>
        /// WxId
        /// </summary>
        string OpenId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        sbyte Gender { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        string Language { get; set; }
        /// <summary>
        /// 国
        /// </summary>
        string Country { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        string City { get; set; }
        /// <summary>
        /// 城镇
        /// </summary>
        string Province { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        string AvatarUrl { get; set; }
        /// <summary>
        /// 状态 默认 'enabled'
        /// </summary>
        string Status { get; set; }
    }
}
