using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.Users
{
    /// <summary>
    /// 账号接口
    /// </summary>
    public interface IDbAccountTable : IDbTableBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        int RoleId { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        DateTime? LastLoginTime { get; set; }
    }
}
