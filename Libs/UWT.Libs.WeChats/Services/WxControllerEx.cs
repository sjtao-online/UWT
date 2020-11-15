using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWT.Libs.WeChats.Controllers;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.WeChats.Services
{
    /// <summary>
    /// 微信控制器的扩展功能
    /// </summary>
    public static class WxControllerEx
    {
        /// <summary>
        /// 获得微信用户表Id
        /// </summary>
        /// <typeparam name="TWxController"></typeparam>
        /// <param name="wxController"></param>
        /// <returns></returns>
        public static int GetWxUserId<TWxController>(this TWxController wxController)
            where TWxController: Controller, IWxController
        {
            return wxController.GetClaimValue(WeChatBasicController.AccountIdKey, 0);
        }

        /// <summary>
        /// 获得微信用户昵称
        /// </summary>
        /// <typeparam name="TWxController"></typeparam>
        /// <param name="wxController"></param>
        /// <returns></returns>
        public static string GetWxNickName<TWxController>(this TWxController wxController)
            where TWxController : Controller, IWxController
        {
            return wxController.GetClaimValue(WeChatBasicController.NickNameKey);
        }
    }

    /// <summary>
    /// 使用微信功能的控制器
    /// </summary>
    public interface IWxController
    {

    }
}
