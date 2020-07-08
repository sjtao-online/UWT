using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Controllers
{
    /// <summary>
    /// 数据表转换器
    /// </summary>
    public class ErrorsController : Controller
        , IListToPage<ErrorCodeListModel>
    {
        /// <summary>
        /// API使用标记
        /// </summary>
        public const string ClientVersionText = "client-version";
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns></returns>
        [Route("Errors/Error/{errorCode:int}")]
        public object Error(int errorCode)
        {
            //  接口API返回接口形式
            if (HttpContext.Request.Headers.ContainsKey(ClientVersionText))
            {
                HttpContext.Response.StatusCode = 200;
                switch (errorCode)
                {
                    case 404:
                        return this.Error(ErrorCode.API_NotFound);
                    case 405:
                        return this.Error(ErrorCode.API_MethodNotAllowed);
                    case 500:
                        var errId = Guid.NewGuid().ToString("N");
                        var tt = this.HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                        this.LogError(errId + ":\n" + tt.Error.ToString());
                        return this.Error(ErrorCode.API_Carsh, data: errId);
                    default:
                        break;
                }
            }
            switch (errorCode)
            {
                case 404:
                case 405:
                    return new NotFoundResult();
                case 500:
                default:
                    break;
            }
            return null;
        }
        /// <summary>
        /// 通用错误码表
        /// </summary>
        /// <returns></returns>
        public object ErrorCodeMap()
        {
            List<ErrorCodeListModel> list = new List<ErrorCodeListModel>();
            foreach (ErrorCode item in Enum.GetValues(typeof(ErrorCode)))
            {
                list.Add(new ErrorCodeListModel()
                {
                    Code = (int)item,
                    Name = item.ToString(),
                    Desc = ErrorCodeEx.GetErrorCodeMsg(item)
                });
            }
            if (HttpContext.Request.Headers.ContainsKey(ErrorsController.ClientVersionText))
                return this.Success(list);
            this.ChangeLayout(RazorPageEx.LayoutFileName_Help);
            return this.ListResult(list).View();
        }
    }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [ListViewModel]
    public class ErrorCodeListModel
    {
        [ListColumn("错误码")]
        public int Code { get; set; }
        [ListColumn("名称")]
        public string Name { get; set; }
        [ListColumn("描述")]
        public string Desc { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}