using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 视图模型接口
    /// 提供显示用的类名与样式
    /// </summary>
    public interface IViewModelBasic
    {
        /// <summary>
        /// 类
        /// </summary>
        string Class { get; }
        /// <summary>
        /// 样式
        /// </summary>
        string Styles { get; }
    }
}
