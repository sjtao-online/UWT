using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Commons
{
    /// <summary>
    /// 视图类基类
    /// </summary>
    public class ViewModelBasic : IViewModelBasic
    {
        /// <summary>
        /// 样式类
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Styles { get; set; }
    }
}
