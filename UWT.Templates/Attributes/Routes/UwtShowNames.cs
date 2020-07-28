using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Routes
{
    /// <summary>
    /// URL显示名<br/>
    /// 一般用于菜单组或权限选择
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class UwtMethodAttribute : Attribute
    {
        /// <summary>
        /// 显示名字符串
        /// </summary>
        public string ShowName { get; internal set; }
        /// <summary>
        /// 模板字符串
        /// </summary>
        public string TemplateText { get; set; } = TemplateText_AreaControllerMethod;
        /// <summary>
        /// 模板常量<br/>
        /// areaShowName - controllerShowName - methodShowName
        /// </summary>
        public const string TemplateText_AreaControllerMethod = "[${AreaShowName} - ][${ControllerShowName} - ]${MethodShowName}";
        /// <summary>
        /// URL显示名
        /// </summary>
        /// <param name="showname">一般用于菜单组或权限选择</param>
        public UwtMethodAttribute(string showname)
        {
            ShowName = showname;
        }
    }
    /// <summary>
    /// 与UwtModule共同使用,表示控制器名<br/>
    /// 默认生成"{UwtControllerName} - {UwtModule}"
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class UwtControllerNameAttribute : Attribute
    {
        /// <summary>
        /// 显示名字符串
        /// </summary>
        public string ShowName { get; internal set; }
        /// <summary>
        /// 控制器显示名
        /// </summary>
        /// <param name="showname">显示名字符串</param>
        public UwtControllerNameAttribute(string showname)
        {
            ShowName = showname;
        }
    }
}
