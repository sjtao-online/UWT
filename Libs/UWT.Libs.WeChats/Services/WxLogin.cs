using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace UWT.Libs.WeChats.Services
{
    /// <summary>
    /// 微信登录服务
    /// </summary>
    public class WxLogin
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="code">微信Code</param>
        /// <param name="config">登录配置</param>
        /// <returns></returns>
        public static string DoLogin(string code, WxConfig config)
        {
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={config.AppId}&secret={config.Secret}&js_code={code}&grant_type=authorization_code";
        RECHECK:
            HttpClient httpClient = new HttpClient();
            var r = httpClient.GetFromJsonAsync<JsCode2SessionModel>(url);
            r.Wait();
            var result = r.Result;
            if (result.errcode == 0)
            {
                return result.openid;
            }
            else if (result.errcode == -1)
            {
                Thread.Sleep(200);
                goto RECHECK;
            }
            return null;
        }
    }
    class JsCode2SessionModel
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public int errcode { get; set; }
        public string errMsg { get; set; }

    }
    /// <summary>
    /// 登录配置
    /// </summary>
    public class WxConfig
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 到期时长<br/>
        /// null为30天
        /// </summary>
        public TimeSpan? TokenExpiry { get; set; }
    }
}
