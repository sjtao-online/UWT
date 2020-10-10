using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.WeChats.Models;
using UWT.Libs.WeChats.Services;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.WeChats.Controllers
{
    /// <summary>
    /// 微信登录相关控制器
    /// </summary>
    public abstract class WeChatBasicController : Controller
        , ITemplateController
    {
        /// <summary>
        /// 微信配置
        /// </summary>
        public abstract WxConfig WxConfig { get; }
        const string AccountIdKey = "AccountId";
        const string NickNameKey = "NickName";
        /// <summary>
        /// 创建签名字典
        /// </summary>
        /// <param name="accountId">账号Id</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        protected virtual Dictionary<string, string> BuildSignIntoDic(int accountId, string nickname)
        {
            return new Dictionary<string, string>()
            {
                [AccountIdKey] = accountId.ToString(),
                [NickNameKey] = nickname
            };
        }
        /// <summary>
        /// 新建微信用户时要插件数据库的字典
        /// </summary>
        /// <param name="src">字典可以修改</param>
        /// <param name="handle">当前操作insert,update</param>
        /// <returns></returns>
        protected virtual void BuildWeChatUserDic(Dictionary<string, object> src, string handle)
        {
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel">小程序获得的登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public virtual object Login([FromBody]WxLoginModel loginModel)
        {
            this.ActionLog();
            var openId = WxLogin.DoLogin(loginModel.Code, WxConfig);
            if (string.IsNullOrEmpty(openId))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.Login_Failed, "微信远程登录失败");
            }
            string refreshToken = Guid.NewGuid().ToString("N");
            int accountId = 0;
            DateTimeOffset exp = DateTimeOffset.Now + (WxConfig.TokenExpiry ?? TimeSpan.FromDays(30));
            using (var db = this.GetDB())
            {
                var dbDic = new Dictionary<string, object>()
                {
                    [nameof(IDbWxUserModel.NickName)] = loginModel.UserInfo.NickName,
                    [nameof(IDbWxUserModel.AvatarUrl)] = loginModel.UserInfo.AvatarUrl,
                    [nameof(IDbWxUserModel.City)] = loginModel.UserInfo.City,
                    [nameof(IDbWxUserModel.Country)] = loginModel.UserInfo.Country,
                    [nameof(IDbWxUserModel.Gender)] = (sbyte)loginModel.UserInfo.Gender,
                    [nameof(IDbWxUserModel.Language)] = loginModel.UserInfo.Language,
                    [nameof(IDbWxUserModel.Token)] = refreshToken,
                    [nameof(IDbWxUserModel.TokenExp)] = exp.LocalDateTime,
                    [nameof(IDbWxUserModel.Status)] = "enabled"
                };
                var wxUserTable = db.UwtGetTable<IDbWxUserModel>();
                if (wxUserTable == null)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.DatatableNotFound, "IDbWxUserModel");
                }
                var q = (from it in wxUserTable where it.OpenId == openId select it.Id).Take(1);
                if (q.Count() == 0)
                {
                    dbDic.Add(nameof(IDbWxUserModel.OpenId), openId);
                    BuildWeChatUserDic(dbDic, "insert");
                    accountId = wxUserTable.UwtInsertWithInt32(dbDic);
                }
                else
                {
                    accountId = q.First();
                    BuildWeChatUserDic(dbDic, "update");
                    wxUserTable.UwtUpdate(accountId, dbDic);
                }
                string accessToken = this.SignInto(BuildSignIntoDic(accountId, loginModel.UserInfo.NickName), AuthWxAttribute.CurrentAuthType);
                return this.Success(new SignIntoModel()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccountId = accountId,
                    TokenExpiry = exp.LocalDateTime,
                    UserInfo = loginModel.UserInfo
                });
            }
        }

        /// <summary>
        /// 刷新Token(也是登录，只是要登录过)
        /// </summary>
        /// <param name="refreshToken">RefreshToken</param>
        /// <returns></returns>
        [HttpPost]
        public virtual object RefreshToken(string refreshToken)
        {
            this.ActionLog();
            DateTimeOffset exp = DateTimeOffset.Now + (WxConfig.TokenExpiry ?? TimeSpan.FromDays(30));
            using (var db = this.GetDB())
            {
                var wxUserTable = db.UwtGetTable<IDbWxUserModel>();
                if (wxUserTable == null)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.DatatableNotFound, "IDbWxUserModel");
                }
                var q = (from it in wxUserTable
                         where it.Token == refreshToken
                         select new 
                         {
                             Exp = it.TokenExp,
                             Token = it.Token,
                             Account = new UserInfoModel()
                             {
                                 Id = it.Id,
                                 NickName = it.NickName,
                                 AvatarUrl = it.AvatarUrl,
                                 City = it.City,
                                 Country = it.Country,
                                 Gender = it.Gender,
                                 Language = it.Language,
                                 Province = it.Province
                             }
                         }).Take(1);
                if (q.Count() == 0)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.Login_Failed, "刷新Token失败");
                }
                else
                {
                    var newRefreshToken = Guid.NewGuid().ToString("N");
                    var m = q.First();
                    var account = m.Account;
                    if (m.Exp >= DateTime.Now - TimeSpan.FromHours(4))
                    {
                        return this.Success(new SignIntoModel()
                        {
                            AccessToken = m.Token,
                            AccountId = account.Id,
                            RefreshToken = refreshToken,
                            TokenExpiry = m.Exp,
                            UserInfo = account
                        });
                    }
                    else
                    {
                        wxUserTable.UwtUpdate(account.Id, new Dictionary<string, object>()
                        {
                            [nameof(IDbWxUserModel.Token)] = newRefreshToken,
                            [nameof(IDbWxUserModel.TokenExp)] = exp.LocalDateTime
                        });
                        return this.Success(new SignIntoModel()
                        {
                            AccessToken = this.SignInto(BuildSignIntoDic(account.Id, account.NickName)),
                            AccountId = account.Id,
                            RefreshToken = newRefreshToken,
                            TokenExpiry = exp.LocalDateTime,
                            UserInfo = account
                        });
                    }
                }
            }
        }
    }
}
