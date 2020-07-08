using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWT.Templates.Models.Templates.Layouts;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 构建菜单组与URL权限
    /// </summary>
    /// <param name="roleId">角色Id</param>
    /// <param name="menugroup">菜单组</param>
    /// <param name="canurls">可行URL权限</param>
    public delegate void BuildRoleMapDelegate(int roleId, List<MenuItemModel> menugroup, List<string> canurls);
    /// <summary>
    /// 菜单组扩展
    /// </summary>
    public static class MenuGroupEx
    {
        class RoleCacheModel
        {
            public HashSet<string> CanUsedUrls { get; set; }
            public List<MenuItemModel> MenuGroup { get; set; }
        }
        static Dictionary<int, RoleCacheModel> Role2RoleCacheMap = new Dictionary<int, RoleCacheModel>();
        /// <summary>
        /// 构建菜单组与URL权限方法
        /// </summary>
        public static BuildRoleMapDelegate BuildRoleCacheFunc { get; set; }
        /// <summary>
        /// 获得菜单组
        /// </summary>
        /// <param name="layout">布局</param>
        /// <returns></returns>
        public static List<MenuItemModel> GetMenuGroupFromRoleId(this LayoutModel layout)
        {
            if (!Role2RoleCacheMap.ContainsKey(layout.RoleId))
            {
                RebuildCache(layout.RoleId);
                if (!Role2RoleCacheMap.ContainsKey(layout.RoleId))
                {
                    return new List<MenuItemModel>();
                }
            }
            return Role2RoleCacheMap[layout.RoleId]?.MenuGroup;
        }
        /// <summary>
        /// 计算URL是否有权限
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool HasUrlAuth(this HttpContext context, string url)
        {
            int roleId = 0;
            if (int.TryParse(context.User?.FindFirst(Models.Consts.AuthConst.RoleIdKey).Value, out roleId))
            {
                HashSet<string> canurls = null;
                if (!Role2RoleCacheMap.ContainsKey(roleId))
                {
                    RebuildCache(roleId);
                    if (!Role2RoleCacheMap.ContainsKey(roleId))
                    {
                        canurls = new HashSet<string>();
                    }
                }
                return canurls.Contains(url.ToLower());
            }
            return false;
        }
        private static void RebuildCache(int roleId)
        {
            var menuGroup = new List<MenuItemModel>();
            var canurls = new List<string>();
            if (BuildRoleCacheFunc == null)
            {
                0.LogError("BuildRoleCacheFunc 未初始化");
            }
            else
            {
                BuildRoleCacheFunc(roleId, menuGroup, canurls);
                lock (Role2RoleCacheMap)
                {
                    Role2RoleCacheMap.Add(roleId, new RoleCacheModel()
                    {
                        MenuGroup = menuGroup,
                        CanUsedUrls = canurls.ToHashSet()
                    });
                }
            }
        }
        /// <summary>
        /// 重建菜单组缓存
        /// </summary>
        public static void RebuildMenuGroup()
        {
            lock (Role2RoleCacheMap)
            {
                Role2RoleCacheMap.Clear();
            }
        }
    }
}
