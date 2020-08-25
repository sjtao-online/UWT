using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Filters;

namespace UWT.Templates.Services.StartupEx
{
    /// <summary>
    /// 快捷UWT扩展
    /// </summary>
    public static class UWTEx
    {
        /// <summary>
        /// 添加默认UWT信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loginUrl">登录Url</param>
        /// <param name="refParamName">登录时重定向到的URL的参数名</param>
        /// <returns></returns>
        public static IServiceCollection AddUWT(this IServiceCollection services, string loginUrl, string refParamName)
        {
            services.AddTransient<IUwtHelper, DefaultUwtHelper>();
            services.AddUWTWwwroot();
            if (ServiceCollectionEx.LessServerMode.HasValue)
            {
                services.AddControllersWithViews(op=>
                {
                    op.Filters.Add<LessViewFilter>();
                });
            }
            else
            {
                services.AddControllersWithViews();
            }
            services.AddControllers();
            services.AddTemplateModelCache();
            services.AddTemplateAuth(loginUrl, refParamName);
            return services;
        }

        class DefaultUwtHelper : IUwtHelper
        {
            public bool HasHelper(string url)
            {
                return false;
            }
        }

        /// <summary>
        /// 使用UWT
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUWT(this IApplicationBuilder app)
        {
            const string rUrl = "/Errors/Error/";
            app.UseExceptionHandler(rUrl + "500");
            app.UseStatusCodePagesWithReExecute(rUrl + "{0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(ep =>
            {
                ep.MapAreaControllerRoute("areas", "areas", "/{area:exists}/{controller=Home}/{action=Index}/{id?}");
                ep.MapControllerRoute("default", "/{controller=Home}/{action=Index}/{id?}");
                ep.MapControllers();
            });
            return app;
        }
    }
}
