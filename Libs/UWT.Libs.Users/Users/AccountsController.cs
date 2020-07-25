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
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Converts;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.Users.Users
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    public class AccountsController : Controller
        , ITemplateController
    {
        /// <summary>
        /// 不检测检测的列表
        /// </summary>
        public static List<int> NoCheckAuthorizedRoleList { get; set; }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <param name="ref"></param>
        /// <returns></returns>
        public virtual async Task<IActionResult> Login(string @ref = null)
        {
            this.ActionLog();
            if (await AuthAttribute.HasSignInUser(HttpContext))
            {
                return this.Redirect(string.IsNullOrEmpty(@ref) ? this.GetClaimValue(AuthConst.DefaultHomeUrl) : @ref);
            }
            ViewBag.Ref = @ref;
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
            this.ActionLog();
            object ret = null;
            this.UsingDb(db =>
            {
                var accounttable = db.UwtGetTable<IDbAccountTable>();
                var q = (from it in accounttable
                         where it.Account == loginModel.Username && it.Status == "enabled"
                         select new
                         {
                             it.Id,
                             it.RoleId,
                             it.Account,
                             it.Password
                         }).Take(1);
                int aid = 0;
                if (q.Count() != 0)
                {
                    var a = q.First();
                    bool pwdtrue = false;
                    if (a.Password != loginModel.Password)
                    {
                        if (a.Password.EndsWith(")"))
                        {
                            if ((a.Password.StartsWith("md5(") && a.Password == PwdConverter.BuildMD5(loginModel.Password))
                                || (a.Password.StartsWith("sha1(") && a.Password == PwdConverter.BuildSHA1(loginModel.Password))
                                || (a.Password.StartsWith("sha256(") && a.Password == PwdConverter.BuildSHA256(loginModel.Password)))
                            {
                                pwdtrue = true;
                            }
                        }
                    }
                    else
                    {
                        pwdtrue = true;
                    }
                    if (!pwdtrue)
                    {
                        ret = this.Error(Templates.Models.Basics.ErrorCode.Login_UserPwdError);
                        return;
                    }
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
                    this.SignInto(new Dictionary<string, string>()
                    {
                        [AuthConst.RoleIdKey] = a.RoleId.ToString(),
                        [AuthConst.AccountIdKey] = a.Id.ToString(),
                        [AuthConst.AccountNameKey] = a.Account,
                        [AuthConst.RoleNameKey] = rolename,
                        [AuthConst.DefaultHomeUrl] = homepage
                    });
                    accounttable.UwtUpdate(a.Id, new Dictionary<string, object>()
                    {
                        [nameof(IDbAccountTable.LastLoginTime)] = DateTime.Now
                    });
                    ret = this.Success(homepage);
                }
                else
                {
                    ret = this.Error(Templates.Models.Basics.ErrorCode.Login_UserPwdError);
                }
                db.UwtGetTable<IDbUserLoginHisTable>().UwtInsertWithInt32(new Dictionary<string, object>()
                {
                    [nameof(IDbUserLoginHisTable.Username)] = loginModel.Username,
                    [nameof(IDbUserLoginHisTable.Pwd)] = loginModel.Password,
                    [nameof(IDbUserLoginHisTable.Status)] = aid != 0,
                    [nameof(IDbUserLoginHisTable.AId)] = aid
                });
            });
            return ret;
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
        public void ChangePwd(int acccountId, string pwd, PwdEncoder pwdEncoder)
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
            this.UsingDb(db =>
            {
                db.UwtGetTable<IDbAccountTable>().UwtUpdate(acccountId, new Dictionary<string, object>()
                {
                    [nameof(IDbAccountTable.Password)] = pwd
                });
            });
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="pwdEncoder">密码编码方式</param>
        /// <param name="others">其它项</param>
        /// <returns></returns>
        [NonAction]
        public int? AddUser(string account, string pwd, int roleId, PwdEncoder pwdEncoder, Dictionary<string, object> others)
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
            this.UsingDb(db =>
            {
                var insert = new Dictionary<string, object>()
                {
                    [nameof(IDbAccountTable.Account)] = account,
                    [nameof(IDbAccountTable.Password)] = pwd,
                    [nameof(IDbAccountTable.Status)] = "enabled",
                    [nameof(IDbAccountTable.RoleId)] = roleId,
                };
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
                    this.LogError(ex.ToString());
                }
            });
            return accountId;
        }
    }
}
