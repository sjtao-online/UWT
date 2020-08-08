using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Services.StartupEx;

namespace UWT.Libs.BBS
{
    /// <summary>
    /// 
    /// </summary>
    public static class BBSEx
    {
        /// <summary>
        /// 添加帮助功能
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBBS(this IServiceCollection services)
        {

            return services;
        }

        public static IApplicationBuilder UseBBS(this IApplicationBuilder app)
        {
            app.UseMgrRouteList(new List<Templates.Models.Basics.RouteModel>()
            {
                new Templates.Models.Basics.RouteModel()
                {
                    Area = "ForumMgr"
                }
            });
            return app;
        }
    }
}
