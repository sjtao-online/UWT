using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Layouts;

namespace UWT.Server.Models
{
    public class LayoutRouteMap : ILayoutRouteMap
    {
        public List<RouteModel> RouteList => new List<RouteModel>() 
        { 
            new RouteModel()
            {
                Controller = "Banners"
            }
        };

        public LayoutModel DefaultLayout => new LayoutModel()
        {
            UserPopupMenuItems = new List<Templates.Models.Templates.Layouts.UserMenuItem>()
            {
                new Templates.Models.Templates.Layouts.UserMenuItem()
                {
                    Url = "/Home/Profile",
                    Title = "个人信息"
                },
                new Templates.Models.Templates.Layouts.UserMenuItem()
                {
                    Url = "/Home/Change",
                    Title = "修改密码"
                },
                null,
                new Templates.Models.Templates.Layouts.UserMenuItem()
                {
                    Title = "退出",
                    Url = "/Home/Logout"
                }
            },
            NickName = "新管理员",
            MainTitle = "后台管理系统",
            SubTitle = "新版本",
            CompanyName = "呵呵",
            MenuGroup = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
            {
                new Templates.Models.Templates.Layouts.MenuItemModel()
                {
                    Title = "首页",
                    Children = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
                    {
                        new Templates.Models.Templates.Layouts.MenuItemModel()
                        {
                            Title = "Logs",
                            Url = "/Home/Logs",
                            Icon = "layui-icon-heart"
                        },
                        new Templates.Models.Templates.Layouts.MenuItemModel()
                        {
                            Title = "Banners",
                            Url = "/Banners/Index",
                            Icon = "layui-icon-log"
                        },
                        new Templates.Models.Templates.Layouts.MenuItemModel()
                        {
                            Title = "Home",
                            Url = "/Home/Index",
                            Icon = "auto"
                        }
                    }
                },
                new Templates.Models.Templates.Layouts.MenuItemModel()
                {
                    Title = "首页2",
                    Children = new List<Templates.Models.Templates.Layouts.MenuItemModel>()
                    {
                        new Templates.Models.Templates.Layouts.MenuItemModel()
                        {
                            Title = "Logs",
                            Url = "/Home/Logs2"
                        },
                        new Templates.Models.Templates.Layouts.MenuItemModel()
                        {
                            Title = "Home",
                            Url = "/Home/Index2"
                        }
                    }
                }
            },
            TitleFormat = ""
        };

        public void HttpContext2LayoutModel(HttpContext context, ref LayoutModel layoutModel)
        {
        }
    }
}
