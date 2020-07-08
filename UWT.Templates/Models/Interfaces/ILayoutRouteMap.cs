using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Templates.Layouts;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 布局导航模型
    /// </summary>
    public interface ILayoutRouteMap
    {
        /// <summary>
        /// 默认管理界面导航的路由列表
        /// </summary>
        List<RouteModel> RouteList { get; }
        /// <summary>
        /// 默认布局
        /// </summary>
        LayoutModel DefaultLayout { get; }
        /// <summary>
        /// 以上下文转布局的回调方法
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="layoutModel">布局</param>
        void HttpContext2LayoutModel(HttpContext context, ref LayoutModel layoutModel);
    }
}
