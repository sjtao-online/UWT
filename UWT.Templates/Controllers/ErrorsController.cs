using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Caches;
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
        [UwtNoRecordModule]
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
        [UwtNoRecordModule]
        public object ErrorCodeMap()
        {
            List<ErrorCodeListModel> list = ModelCache.ErrorCodeMap.Values.ToList();
            list.Insert(0, new ErrorCodeListModel()
            {
                Code = (int)ErrorCode.UnkownError_SeeMsg,
                Name = ErrorCode.UnkownError_SeeMsg.ToString(),
                Desc = "未知错误，一般会定制错误信息",
                AssemblyName = "UWT.Templates"
            });
            list.Sort(new ErrorCodeComparer());
            if (HttpContext.Request.Headers.ContainsKey(ErrorsController.ClientVersionText))
                return this.Success(list);
            this.SetTitle("固定错误码对应表");
            this.ChangeLayout(RazorPageEx.LayoutFileName_Help);
            return this.ListResult(list).View();
        }
    }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    class ErrorCodeComparer : Comparer<ErrorCodeListModel>
    {
        public override int Compare([AllowNull] ErrorCodeListModel x, [AllowNull] ErrorCodeListModel y)
        {
            return x.Code - y.Code;
        }
    }
    [ListViewModel]
    public class ErrorCodeListModel
    {
        [ListColumn("错误码", Width = "80px")]
        public int Code { get; set; }
        [ListColumn("名称")]
        public string Name { get; set; }
        [ListColumn("描述")]
        public string Desc { get; set; }
        [ListColumn("程序集")]
        public string AssemblyName { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}