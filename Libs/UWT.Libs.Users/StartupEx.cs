using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using UWT.Libs.Users.Roles;
using UWT.Libs.Users.Users;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Models.Templates.Layouts;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.StartupEx;
using UWT.Libs.Users.MenuGroups;
using UWT.Templates.Services.Converts;
using LinqToDB;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.Users
{
    /// <summary>
    /// 启动相关扩展
    /// </summary>
    public static class StartupEx
    {
        /// <summary>
        /// 使用User，应放置于AddDbSetting之后
        /// </summary>
        /// <param name="app"></param>
        /// <param name="defaultLayoutCallback">构造布局对象</param>
        /// <returns></returns>
        public static IApplicationBuilder UseLibUser(this IApplicationBuilder app, Action<LayoutModel> defaultLayoutCallback)
        {
            MenuGroupEx.BuildRoleCacheFunc = (roleId, menuGroup, canurls) =>
            {
                using (var db = TemplateControllerEx.GetDB(null))
                {
                    var roleTable = db.UwtGetTable<IDbRoleTable>();
                    var roles = (from it in roleTable
                                where it.Id == roleId
                                select new
                                {
                                    it.Id,
                                    it.HomePageUrl,
                                    it.MenuGroupId
                                }).Take(1);
                    if (roles.Count() == 0)
                    {
                        return;
                    }
                    var role = roles.First();
                    List<MenuItemModel> menuItems = new List<MenuItemModel>();
                    menuItems.Add(new MenuItemModel()
                    {
                        Title = "首页",
                        Url = role.HomePageUrl
                    });
                    if ((AccountsController.NoCheckAuthorizedRoleList == null && roleId == 0)
                        || (AccountsController.NoCheckAuthorizedRoleList != null && AccountsController.NoCheckAuthorizedRoleList.Contains(roleId)))
                    {
                        var ms = from it in db.UwtGetTable<IDbModuleTable>() where it.Type == "page" select it.Url;
                        Dictionary<string, List<string>> maps = new Dictionary<string, List<string>>();
                        foreach (var item in ms)
                        {
                            var urlkeys = item.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (urlkeys.Count == 3)
                            {
                                if (!maps.ContainsKey(urlkeys[0]))
                                {
                                    maps[urlkeys[0]] = new List<string>();
                                }
                                maps[urlkeys[0]].Add(item);
                            }
                            else
                            {
                                menuItems.Add(new MenuItemModel()
                                {
                                    Title = item,
                                    Url = item
                                });
                            }
                        }
                        foreach (var item in maps)
                        {
                            var mim = new MenuItemModel()
                            {
                                Title = item.Key,
                                Children = new List<MenuItemModel>(),
                            };
                            foreach (var it in item.Value)
                            {
                                mim.Children.Add(new MenuItemModel()
                                {
                                    Url = it,
                                    Title = it
                                });
                            }
                        }
                        menuGroup.AddRange(menuItems);
                        canurls.AddRange(db.UwtGetTable<IDbModuleTable>().Select(m => m.Url.ToLower()));
                        return;
                    }
                    FillMenuItems(db, ref menuItems, 0, role.MenuGroupId);
                    menuGroup.AddRange(menuItems);
                    var urls = from it in db.UwtGetTable<IDbRoleModuleRefTable>()
                               join u in db.UwtGetTable<IDbModuleTable>() on it.MId equals u.Id
                               where it.RId == roleId
                               select u.Url.ToLower();
                    canurls.AddRange(urls.ToList());
                }
            };
            app.UseMgrLayout(list => 
            {
                list.Add(new Templates.Models.Basics.RouteModel()
                {
                    Controller = "MenuGroups"
                });
                list.Add(new Templates.Models.Basics.RouteModel()
                {
                    Controller = "Role"
                });
            }, defaultLayoutCallback, LibUserHasAuthHandleAction);
            TemplateControllerEx.UsingDb(null, db =>
            {
                //  重新组织Modules
                var m = db.UwtGetTable<IDbModuleTable>();
                var modules = ControllerToModulesConverter.CalcAllModules();
                if (m.Count() == 0)
                {
                    var bt = db.BeginTransaction();
                    try
                    {
                        foreach (var item in modules)
                        {
                            m.UwtInsertWithInt32(BuildModuleInsertDic(item));
                        }
                        bt.Commit();
                    }
                    catch (Exception ex)
                    {
                        db.LogError(ex.ToString());
                        bt.Rollback();
                    }
                }
                else
                {
                    List<IDbModuleTable> marr = m.ToList();
                    List<KeyValuePair<int, string>> namechanges = new List<KeyValuePair<int, string>>();
                    for (int i = marr.Count - 1; i >= 0; i--)
                    {
                        var current = marr[i];
                        foreach (var item in modules)
                        {
                            if (item.Url == current.Url)
                            {
                                if (item.ShowName != current.Name)
                                {
                                    namechanges.Add(new KeyValuePair<int, string>(current.Id, item.ShowName));
                                }
                                modules.Remove(item);
                                marr.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (marr.Count > 0 || modules.Count > 0 || namechanges.Count > 0)
                    {
                        var bt = db.BeginTransaction();
                        try
                        {
                            List<int> vs = new List<int>();
                            marr.ForEach(m => vs.Add(m.Id));
                            m.Delete(m => vs.Contains(m.Id));
                            foreach (var item in modules)
                            {
                                m.UwtInsertWithInt32(BuildModuleInsertDic(item));
                            }
                            foreach (var item in namechanges)
                            {
                                m.UwtUpdate(item.Key, new Dictionary<string, object>()
                                {
                                    [nameof(IDbModuleTable.Name)] = item.Value
                                });
                            }
                            bt.Commit();
                        }
                        catch (Exception ex)
                        {
                            db.LogError(ex.ToString());
                            bt.Rollback();
                        }
                    }
                }
            });
            return app;
        }

        static Dictionary<string, object> BuildModuleInsertDic(ModuleModel item)
        {
            return new Dictionary<string, object>()
            {
                [nameof(IDbModuleTable.Url)] = item.Url,
                [nameof(IDbModuleTable.Type)] = item.Category == ModuleCategory.API ? "api" : "page",
                [nameof(IDbModuleTable.Name)] = item.ShowName
            };
        }

        private static void FillMenuItems(LinqToDB.Data.DataConnection db, ref List<MenuItemModel> menuItems, int pid, int groupId)
        {
            var qitems = from it in db.UwtGetTable<IDbMenuGroupItemTable>()
                         join m in db.UwtGetTable<IDbModuleTable>() on it.Url equals m.Id
                         where it.GroupId == groupId && it.Pid == pid && it.Valid
                         orderby it.Index
                         select new
                         {
                             it.Id,
                             it.Icon,
                             it.Title,
                             m.Url,
                             it.Tooltip
                         };
            foreach (var item in qitems.ToArray())
            {
                var children = new List<MenuItemModel>();
                FillMenuItems(db, ref children, item.Id, groupId);
                menuItems.Add(new MenuItemModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Url = item.Url,
                    Icon = item.Icon,
                    Tooltip = item.Tooltip,
                    Children = children
                });
            }
        }

        private static void LibUserHasAuthHandleAction(HttpContext context, LayoutModel layout)
        {
            if (context.User != null && context.User.HasClaim(m=>m.Type == AuthConst.RoleIdKey))
            {
                var roleIdClaim = context.User.FindFirst(AuthConst.RoleIdKey);
                var nickname = context.User.FindFirst(AuthConst.AccountNameKey).Value;
                if (!string.IsNullOrEmpty(nickname))
                {
                    layout.NickName = nickname;
                }
                var accountIdClaim = context.User.FindFirst(AuthConst.AccountIdKey).Value;
                if (!string.IsNullOrEmpty(accountIdClaim))
                {
                    if (int.TryParse(accountIdClaim, out int accountId))
                    {
                        layout.AccountId = accountId;
                    }
                }
                var rolename = context.User.FindFirst(AuthConst.RoleNameKey).Value;
                if (!string.IsNullOrEmpty(rolename))
                {
                    layout.RoleName = rolename;
                }
                if (int.TryParse(roleIdClaim.Value, out int roleId))
                {
                    layout.RoleId = roleId;
                    var menuItems = layout.GetMenuGroupFromRoleId();
                    layout.MenuGroup.InsertRange(0, menuItems);
                }
            }
        }
    }
}
