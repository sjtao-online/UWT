using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 页面返回器
    /// </summary>
    public interface IPageResult
    {
        /// <summary>
        /// 返回ViewResult
        /// </summary>
        /// <returns></returns>
        IActionResult View();
    }
}
