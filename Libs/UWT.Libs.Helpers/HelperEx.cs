using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.Helpers
{
    /// <summary>
    /// 帮助功能扩展
    /// </summary>
    public static class HelperEx
    {
        /// <summary>
        /// 添加帮助功能
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHelper(this IServiceCollection services)
        {
            services.AddTransient<Templates.Models.Interfaces.IUwtHelper, UwtHelperImpl>();
            return services;
        }
    }
}
