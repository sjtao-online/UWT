using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UWT.Templates.Services.StartupEx;
using Microsoft.Extensions.DependencyInjection;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 任意扩展，方便使用
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 获得当前WebHost环境
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IWebHostEnvironment GetCurrentWebHost(this object @this)
        {
            return ApplicationBuilderEx.ApplicationBuilder.ApplicationServices.GetService<IWebHostEnvironment>();
        }
    }
}
