using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.Users.Users
{
    /// <summary>
    /// 账号配置信息
    /// </summary>
    public class AccountConfig
    {
        /// <summary>
        /// 禁用本控制器<br/>
        /// 用于要使用Users内的功能但不用默认登录界面与接口<br/>
        /// 一般自定义界面或接口使用
        /// </summary>
        public bool DisabledController { get; set; }
        /// <summary>
        /// 不检测权限的列表
        /// </summary>
        public List<int> NoCheckAuthorizedRoleList { get; set; }
        /// <summary>
        /// 登录账号类型
        /// </summary>
        public string LoginAccountType { get; set; }
        /// <summary>
        /// 登录界面样式
        /// 默认支持<br/>
        /// default,s,star
        /// </summary>
        public string ViewTheme { get; set; }
    }
}
