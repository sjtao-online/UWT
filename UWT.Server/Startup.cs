using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UWT.Libs.BBS;
using UWT.Libs.Helpers;
using UWT.Libs.Users;
using UWT.Templates.Models.Basics;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.StartupEx;

namespace UWT.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLess(false);
            services.AddConstDictionary(new Dictionary<string, string>()
            {
                ["BannerController"] = "Banners",
                ["BannerSizeTip"] = "100 * 100",
                ["NewsCatesController"] = "NewsCates",
                ["NewsController"] = "News",
                ["NewsModifyProperties"] = "ModifyProperties",
            }, "UWT.Libs.Normals");
            services.AddConstDictionary(new Dictionary<string, string>()
            {
                ["MenuGroupTableName"] = "uwt_users_menu_groups"
            }, "UWT.Libs.Users");
            services.AddConstDictionary(new Dictionary<string, string>()
            {
                ["ModulesTableName"] = "uwt_users_modules"
            });
            services.AddUWT("/Accounts/Login", "ref");
            services.AddHelper();
            services.AddBBS(new List<string>()
            {
                "red"
            });
            services.AddLogging();
            services.AddSingleton(HtmlEncoder.Create(System.Text.Unicode.UnicodeRanges.All));
#if DEBUG
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseUWT();
            app.UseDbSettings<DataModels.UwtDB>(System.IO.Path.Combine(env.ContentRootPath, "db.conf"));
            app.UseMgrRouteList(new List<RouteModel>()
            {
                new RouteModel()
                {
                    Controller = "Home"
                }
            });
            app.UseLibUser(m =>
            {
                m.TitleFormat = "{0} - 管理平台";
                m.SubTitle = "全新";
                m.NickName = "管理员";
                m.CompanyName = "我的公司";
                m.HomePageTitle = "首页标题";
                m.HomePageUrl = "/Home/Index";
                m.MainTitle = "新管理平台";
                m.PeriodOfValidity = "2020";
                m.MenuGroup = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
                {
                    new Templates.Models.Templates.Layouts.MenuItemModel()
                    {
                        Title = "首页",
                        Url = "/"
                    },
                    new Templates.Models.Templates.Layouts.MenuItemModel()
                    {
                        Title = "管理",
                        Icon = "layui-icon-light",
                        Children = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
                        {
                            new Templates.Models.Templates.Layouts.MenuItemModel()
                            {
                                Title = "管理1",
                                Url = "/",
                                Icon = "layui-icon-heart-fill"
                            },
                            new Templates.Models.Templates.Layouts.MenuItemModel()
                            {
                                Title = "管理1",
                                Url = "/",
                                Icon = "layui-icon-heart"
                            }
                        }
                    }
                };
                m.UserPopupMenuItems = new List<Templates.Models.Templates.Layouts.UserMenuItem>()
                {
                    new Templates.Models.Templates.Layouts.UserMenuItem()
                    {
                        Title = "我的信息",
                        Url = "/Accounts/Profile"
                    },
                    null,
                    new Templates.Models.Templates.Layouts.UserMenuItem()
                    {
                        Title = "退出",
                        Url = "/Accounts/Logout"
                    }
                };
                m.QuickLinks = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
                {
                    new Templates.Models.Templates.Layouts.MenuItemModel()
                    {
                        Title = "123",
                        Url = "/Account/Login"
                    },
                    new Templates.Models.Templates.Layouts.MenuItemModel()
                    {
                        Title = "123",
                        Url = "/Account/Login"
                    }
                };
            });
            app.UseBBS();
            using (StreamReader sr = new StreamReader(System.IO.Path.Combine(env.ContentRootPath, "..", "UWT.Templates", "LayuiIcons.json")))
            {
                Libs.Users.MenuGroups.IconSimpleSelectorBuilder.IconList = JsonSerializer.Deserialize<List<NameKeyModel>>(sr.ReadToEnd());
            }
            Libs.Users.Users.AccountsController.Config.NoCheckAuthorizedRoleList = new List<int>() { 2 };
            app.UseLess();
        }
    }
}
