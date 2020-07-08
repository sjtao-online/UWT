using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 帮助文档接口
    /// </summary>
    public interface IUwtHelper
    {
        /// <summary>
        /// 是否支持帮助
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>返回是否支持</returns>
        bool HasHelper(string url);
    }
}
