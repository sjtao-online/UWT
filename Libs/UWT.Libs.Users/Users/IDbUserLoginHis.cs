using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.Users
{
    /// <summary>
    /// 用户登录历史
    /// </summary>
    public interface IDbUserLoginHisTable : IDbTableBase
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Pwd { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// 账号Id
        /// </summary>
        int AId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        bool Status { get; set; }
    }
}
