using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Basics
{
    /// <summary>
    /// 路由模型
    /// </summary>
    public class RouteModel
    {
        internal const string RouteKeyArea = "area";
        internal const string RouteKeyController = "controller";
        internal const string RouteKeyAction = "action";
        internal Dictionary<string, string> RouteMap { get; private set; }
        /// <summary>
        /// 区
        /// </summary>
        public string Area
        {
            get
            {
                return GetRouteMap(RouteKeyArea);
            }
            set
            {
                SetRouteMap(value, RouteKeyArea);
            }
        }
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller
        {
            get
            {
                return GetRouteMap(RouteKeyController);
            }
            set
            {
                SetRouteMap(value, RouteKeyController);
            }
        }
        /// <summary>
        /// 动作
        /// </summary>
        public string Action
        {
            get
            {
                return GetRouteMap(RouteKeyAction);
            }
            set
            {
                SetRouteMap(value, RouteKeyAction);
            }
        }
        /// <summary>
        /// 路由模型
        /// </summary>
        public RouteModel()
        {
            RouteMap = new Dictionary<string, string>();
        }
        private void SetRouteMap(string value, string key)
        {
            RouteMap[key] = value;
        }
        private string GetRouteMap(string key)
        {
            if (RouteMap.ContainsKey(key))
            {
                return RouteMap[key];
            }
            return null;
        }
    }
}
