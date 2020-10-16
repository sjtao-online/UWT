using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Users.Roles;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Converts;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Users.Users
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [UwtNoRecordModule]
    public class AccountsController : Controller
        , ITemplateController
    {
        /// <summary>
        /// 账号配置
        /// </summary>
        public static AccountConfig Config => new AccountConfig
        {
            LoginAccountType = "mgr",
            ViewTheme = "default",
            NoCheckAuthorizedRoleList = new List<int>(),
            DisabledController = false
        };
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <param name="ref">登录后跳转页面</param>
        /// <returns></returns>
        public virtual async Task<IActionResult> Login(string @ref = null)
        {
            if (Config.DisabledController)
            {
                return NotFound();
            }
            this.ActionLog();
            if (await AuthAttribute.HasSignInUser(HttpContext))
            {
                return this.Redirect(string.IsNullOrEmpty(@ref) ? this.GetClaimValue(AuthConst.DefaultHomeUrl) : @ref);
            }
            ViewBag.Ref = @ref;
            ViewBag.Theme = Config.ViewTheme;
            return View();
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object Login([FromBody]UserLoginModel loginModel)
        {
            if (Config.DisabledController)
            {
                return NotFound();
            }
            this.ActionLog();
            return DoLoginToContext(HttpContext, loginModel.Username, loginModel.Password, Config.LoginAccountType);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [AuthUser]
        public IActionResult Logout()
        {
            this.ActionLog();
            ControllerEx.SignOuto(this);
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="acccountId">账号Id</param>
        /// <param name="pwd">密码</param>
        /// <param name="pwdEncoder">密码编码方式</param>
        [NonAction]
        public static void ChangePwd(int acccountId, string pwd, PwdEncoder pwdEncoder)
        {
            switch (pwdEncoder)
            {
                case PwdEncoder.None:
                    break;
                case PwdEncoder.MD5:
                    pwd = PwdConverter.BuildMD5(pwd);
                    break;
                case PwdEncoder.SHA1:
                    pwd = PwdConverter.BuildSHA1(pwd);
                    break;
                case PwdEncoder.SHA256:
                    pwd = PwdConverter.BuildSHA256(pwd);
                    break;
                default:
                    break;
            }
            using (var db = TemplateControllerEx.GetDB())
            {
                db.UwtGetTable<IDbAccountTable>().UwtUpdate(acccountId, new Dictionary<string, object>()
                {
                    [nameof(IDbAccountTable.Password)] = pwd
                });
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="type">账号类型</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pwdEncoder">密码编码方式</param>
        /// <param name="others">其它项</param>
        /// <returns></returns>
        [NonAction]
        public static int? AddUser(string account, string type, string pwd, int roleId, PwdEncoder pwdEncoder, Dictionary<string, object> others)
        {
            int? accountId = null;
            switch (pwdEncoder)
            {
                case PwdEncoder.None:
                    break;
                case PwdEncoder.MD5:
                    pwd = PwdConverter.BuildMD5(pwd);
                    break;
                case PwdEncoder.SHA1:
                    pwd = PwdConverter.BuildSHA1(pwd);
                    break;
                case PwdEncoder.SHA256:
                    pwd = PwdConverter.BuildSHA256(pwd);
                    break;
                default:
                    break;
            }
            using (var db = TemplateControllerEx.GetDB())
            {
                var insert = new Dictionary<string, object>()
                {
                    [nameof(IDbAccountTable.Account)] = account,
                    [nameof(IDbAccountTable.Password)] = pwd,
                    [nameof(IDbAccountTable.Status)] = "enabled",
                    [nameof(IDbAccountTable.RoleId)] = roleId
                };
                if (type != null)
                {
                    insert[nameof(IDbAccountTable.Type)] = type;
                }
                if (others != null)
                {
                    foreach (var item in others)
                    {
                        if (insert.ContainsKey(item.Key))
                        {
                            insert.Remove(item.Key);
                        }
                        insert.Add(item.Key, item.Value);
                    }
                }
                try
                {
                    accountId = db.UwtGetTable<IDbAccountTable>().UwtInsertWithInt32(insert);
                }
                catch (Exception ex)
                {
                    0.LogError(ex.ToString());
                }
            }
            return accountId;
        }

        /// <summary>
        /// uwt账号表登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="account">账号名</param>
        /// <param name="pwd">密码</param>
        /// <param name="type">账号类型</param>
        /// <param name="authType">授权类型</param>
        /// <returns>API标准回复</returns>
        public static object DoLoginToContext(HttpContext httpContext, string account, string pwd, string type, string authType = null)
        {
            object ret = null;
            using (var db = TemplateControllerEx.GetDB())
            {
                var accounttable = db.UwtGetTable<IDbAccountTable>();
                var q = (from it in accounttable
                         where it.Account == account && it.Type == type
                         select new
                         {
                             it.Id,
                             it.RoleId,
                             it.Account,
                             it.Password,
                             it.Status
                         }).Take(1);
                int aid = 0;
                if (q.Count() != 0)
                {
                    var a = q.First();
                    if (a.Status == "enabled")
                    {
                        bool pwdtrue = false;
                        if (a.Password != pwd)
                        {
                            if (a.Password.EndsWith(")"))
                            {
                                if ((a.Password.StartsWith("md5(") && a.Password == PwdConverter.BuildMD5(pwd))
                                    || (a.Password.StartsWith("sha1(") && a.Password == PwdConverter.BuildSHA1(pwd))
                                    || (a.Password.StartsWith("sha256(") && a.Password == PwdConverter.BuildSHA256(pwd)))
                                {
                                    pwdtrue = true;
                                }
                            }
                        }
                        else
                        {
                            pwdtrue = true;
                        }
                        if (pwdtrue)
                        {
                            string rolename = "内置超级管理员";
                            string homepage = "/";
                            aid = a.Id;
                            if (a.RoleId != 0)
                            {
                                var roletable = db.UwtGetTable<IDbRoleTable>();
                                var qrole = (from it in roletable
                                             where it.Id == a.RoleId
                                             select new
                                             {
                                                 it.Id,
                                                 it.Name,
                                                 it.HomePageUrl
                                             }).Take(1);
                                if (qrole.Count() != 0)
                                {
                                    var role = qrole.First();
                                    rolename = role.Name;
                                    homepage = role.HomePageUrl;
                                }
                            }
                            httpContext.SignInto(new Dictionary<string, string>()
                            {
                                [AuthConst.RoleIdKey] = a.RoleId.ToString(),
                                [AuthConst.AccountIdKey] = a.Id.ToString(),
                                [AuthConst.AccountNameKey] = a.Account,
                                [AuthConst.RoleNameKey] = rolename,
                                [AuthConst.DefaultHomeUrl] = homepage
                            }, authType);
                            accounttable.UwtUpdate(a.Id, new Dictionary<string, object>()
                            {
                                [nameof(IDbAccountTable.LastLoginTime)] = DateTime.Now
                            });
                            ret = ControllerEx.Success(null, homepage);
                        }
                        else
                        {
                            ret = ControllerEx.Error(null, Templates.Models.Basics.ErrorCode.Login_UserPwdError);
                        }
                    }
                    else
                    {
                        if (a.Status == "disabled")
                        {
                            ret = ControllerEx.Error(null, Templates.Models.Basics.ErrorCode.Login_UserDisabled);
                        }
                        else if (a.Status == "writeoff")
                        {
                            ret = ControllerEx.Error(null, Templates.Models.Basics.ErrorCode.Login_UserPwdError);
                        }
                    }
                }
                else
                {
                    ret = ControllerEx.Error(null, Templates.Models.Basics.ErrorCode.Login_UserPwdError);
                }
                db.UwtGetTable<IDbUserLoginHisTable>().UwtInsertWithInt32(new Dictionary<string, object>()
                {
                    [nameof(IDbUserLoginHisTable.Username)] = account,
                    [nameof(IDbUserLoginHisTable.Pwd)] = pwd,
                    [nameof(IDbUserLoginHisTable.Type)] = type,
                    [nameof(IDbUserLoginHisTable.Status)] = aid != 0,
                    [nameof(IDbUserLoginHisTable.AId)] = aid
                });
            }
            return ret;
        }
    }
}
