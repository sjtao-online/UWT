using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UWT.Libs.Users;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class AuthBBSAttribute : AuthUserAttribute
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public override async Task<bool> HasAuthorized()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return true;
        }
    }
}
