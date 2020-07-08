using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.Layouts
{
    /// <summary>
    /// 布局模型
    /// </summary>
    public class LayoutModel
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 标题格式化
        /// </summary>
        public string TitleFormat { get; set; }
        /// <summary>
        /// 网页图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 使用的皮肤
        /// </summary>
        public string Skin { get; set; }
        /// <summary>
        /// 子标题
        /// 网页内标题中的小字
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 主标题
        /// 网页内标题中的大字
        /// </summary>
        public string MainTitle { get; set; }
        /// <summary>
        /// 登录者的头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 登录者的显示名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 登录者的角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 用户弹出菜单
        /// </summary>
        public List<UserMenuItem> UserPopupMenuItems { get; set; }
        /// <summary>
        /// 快捷方式栏
        /// </summary>
        public List<MenuItemModel> QuickLinks { get; set; }
        /// <summary>
        /// 菜单组
        /// </summary>
        public List<MenuItemModel> MenuGroup { get; set; }
        /// <summary>
        /// 首页Url
        /// </summary>
        public string HomePageUrl { get; set; }
        /// <summary>
        /// 首页标题
        /// </summary>
        public string HomePageTitle { get; set; }
        /// <summary>
        /// 有效期<br/>
        /// 底部--[几年-几年]
        /// </summary>
        public string PeriodOfValidity { get; set; }
        /// <summary>
        /// 公司名<br/>
        /// 底部使用
        /// </summary>
        public string CompanyName { get; set; }
        internal static LayoutModel DefaultLayout = new LayoutModel();
        internal static Action<HttpContext, LayoutModel> LayoutCallback;
        /// <summary>
        /// 获得一个默认布局的副本
        /// </summary>
        /// <returns></returns>
        static LayoutModel GetCloneLayout()
        {
            var upmis = new List<UserMenuItem>();
            if (DefaultLayout.UserPopupMenuItems != null)
            {
                foreach (var item in DefaultLayout.UserPopupMenuItems)
                {
                    if (item == null)
                    {
                        upmis.Add(null);
                        continue;
                    }
                    upmis.Add(new UserMenuItem()
                    {
                        Icon = item.Icon,
                        Id = item.Id,
                        Title = item.Title,
                        Url = item.Url
                    });
                }
            }
            return new LayoutModel()
            {
                TitleFormat = DefaultLayout.TitleFormat,
                NickName = DefaultLayout.NickName,
                Avatar = DefaultLayout.Avatar,
                Icon = DefaultLayout.Icon,
                MainTitle = DefaultLayout.MainTitle,
                Skin = DefaultLayout.Skin,
                SubTitle = DefaultLayout.SubTitle,
                UserPopupMenuItems = upmis,
                CompanyName = DefaultLayout.CompanyName,
                HomePageTitle = DefaultLayout.HomePageTitle,
                HomePageUrl = DefaultLayout.HomePageUrl,
                PeriodOfValidity = DefaultLayout.PeriodOfValidity,
                MenuGroup = CloneMenuItemList(DefaultLayout.MenuGroup),
                QuickLinks = CloneMenuItemList(DefaultLayout.QuickLinks)
            };
        }
        /// <summary>
        /// 拷贝一个菜单项列表
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        static List<MenuItemModel> CloneMenuItemList(List<MenuItemModel> menus)
        {
            if (menus == null)
            {
                return null;
            }
            List<MenuItemModel> rMenus = new List<MenuItemModel>();
            foreach (var item in menus)
            {
                rMenus.Add(new MenuItemModel()
                {
                    Icon = item.Icon,
                    Title = item.Title,
                    Id = item.Id,
                    Url = item.Url,
                    Children = CloneMenuItemList(item.Children)
                });
            }
            return rMenus;
        }
        /// <summary>
        /// 获得当前布局
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static LayoutModel GetCurrentLayout(HttpContext httpContext)
        {
            const string LayoutModelHttpContextKey = "_layout_context_model";
            if (httpContext.Items.ContainsKey(LayoutModelHttpContextKey))
            {
                var layout = httpContext.Items[LayoutModelHttpContextKey];
                if (layout is LayoutModel)
                {
                    return layout as LayoutModel;
                }
            }
            var current = GetCloneLayout();
            httpContext.Items[LayoutModelHttpContextKey] = current;
            LayoutCallback(httpContext, current);
            return current;
        }
    }
}
