using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using UWT.Libs.Users.Roles;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Services.Extends;
using UWT.Libs.Users.Users;
using System.Text.Json;

namespace UWT.Libs.Users
{
    /// <summary>
    /// 授权对象
    /// </summary>
    public class AuthUserAttribute : AuthAttribute
    {
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <returns>是否有权限</returns>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public override async Task<bool> HasAuthorized()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return HandleAuth();
        }
        bool HandleAuth()
        {
            //  特殊处理 退出不判断权限
            if (Context.HttpContext.Request.Path == "/Accounts/Logout")
            {
                return true;
            }
            var roleId = Context.HttpContext.User.FindFirst("RoleId")?.Value;
            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }
            if (int.TryParse(roleId, out int roleIdInt))
            {
                //  处理特殊角色 可以使用任何权限
                //  如果有设置使用设置权限，如果没有设置0为特殊权限
                if ((AccountsController.Config.NoCheckAuthorizedRoleList == null && roleIdInt == 0)
                    || (AccountsController.Config.NoCheckAuthorizedRoleList != null && AccountsController.Config.NoCheckAuthorizedRoleList.Contains(roleIdInt)))
                {
                    return true;
                }
                string url = this.Context.HttpContext.Request.Path.ToString();
                if (url == "/" || url == "")
                {
                    url = "/home/index";
                }
                return Context.HttpContext.HasUrlAuth(url);
            }
            return false;
        }
    }
}
