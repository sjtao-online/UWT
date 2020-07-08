using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Routes
{
    /// <summary>
    /// 标准模板
    /// </summary>
    public class UwtRouteAttribute : RouteAttribute, IRouteValueProvider
    {
        /// <summary>
        /// 标准模板/area/controller/action
        /// </summary>
        /// <param name="area">区域名</param>
        /// <param name="routetemplate">路由前置部分,默认值[area]/[controller]/[action]</param>
        public UwtRouteAttribute(string area, string routetemplate = null)
            : base(routetemplate??"[area]/[controller]/[action]")
        {
            RouteKey = "area";
            RouteValue = area;
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public string RouteKey { get; private set; }

        public string RouteValue { get; private set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
