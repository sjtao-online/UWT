using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Services.Extends;
using LinqToDB;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Attributes.Routes;

namespace UWT.Libs.Users.Roles
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [AuthUser]
    [UwtControllerName("角色管理")]
    public class RolesController : Controller
        , IListToPage<IDbRoleTable, RoleListItemModel>
        , IFormToPage<RoleAddModel>
        , IFormToPage<RoleModifyModel>
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Index()
        {
            this.ActionLog();
            this.AddHandler("添加", ".Add");
            return this.ListResult(m=>new RoleListItemModel()
            {
                Id = m.Id,
                Name = m.Name,
            }).View();
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<RoleAddModel>().View();
        }
        /// <summary>
        /// 添加接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<object> AddModel([FromBody]RoleAddModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<RoleAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbRoleTable>();
                var homePage = (from it in db.UwtGetTable<IDbModuleTable>() where it.Id == model.HomePageUrl select it.Url).First();
                var roleId = table.UwtInsertWithInt32(new Dictionary<string, object>()
                {
                    [nameof(IDbRoleTable.Name)] = model.Name,
                    [nameof(IDbRoleTable.Desc)] = model.Desc,
                    [nameof(IDbRoleTable.MenuGroupId)] = model.MenuGroupId,
                    [nameof(IDbRoleTable.HomePageUrl)] = homePage
                });
                var rmRef = db.UwtGetTable<IDbRoleModuleRefTable>();
                foreach (var item in model.Urls)
                {
                    rmRef.UwtInsertWithInt32(new Dictionary<string, object>()
                    {
                        [nameof(IDbRoleModuleRefTable.RId)] = roleId,
                        [nameof(IDbRoleModuleRefTable.MId)] = item
                    });
                }
            });
            return this.Success();
        }

        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IActionResult Modify(int id)
        {
            this.ActionLog();
            RoleModifyModel current = null;
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbRoleTable>();
                var q = (from it in table
                         join url in db.UwtGetTable<IDbModuleTable>() on it.HomePageUrl equals url.Url
                         where it.Id == id && it.Valid
                         select new RoleModifyModel()
                         {
                             Id = it.Id,
                             Name = it.Name,
                             Desc = it.Desc,
                             MenuGroupId = it.MenuGroupId,
                             HomePageUrl = url.Id
                         }).Take(1);
                if (q.Count() != 0)
                {
                    current = q.First();
                    current.Urls = (from it in db.UwtGetTable<IDbRoleModuleRefTable>() where it.RId == id select it.MId).ToList();
                }
            });
            if (current == null)
            {
                return this.ItemNotFound();
            }
            return this.FormResult(current).View();
        }
        /// <summary>
        /// 修改接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody]RoleModifyModel model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<RoleModifyModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                System.Linq.Expressions.Expression<Func<IDbRoleTable, bool>> expression = m => m.Id == model.Id;
                var table = db.UwtGetTable<IDbRoleTable>();
                var homePage = (from it in db.UwtGetTable<IDbModuleTable>() where it.Id == model.HomePageUrl select it.Url).First();
                table.UwtUpdate(model.Id, new Dictionary<string, object>()
                {
                    [nameof(IDbRoleTable.Name)] = model.Name,
                    [nameof(IDbRoleTable.Desc)] = model.Desc,
                    [nameof(IDbRoleTable.MenuGroupId)] = model.MenuGroupId,
                    [nameof(IDbRoleTable.HomePageUrl)] = homePage
                });
                var rmRefTable = db.UwtGetTable<IDbRoleModuleRefTable>();
                rmRefTable.Delete(m => m.RId == model.Id);
                foreach (var item in model.Urls)
                {
                    rmRefTable.UwtInsertWithInt32(new Dictionary<string, object>()
                    {
                        [nameof(IDbRoleModuleRefTable.RId)] = model.Id,
                        [nameof(IDbRoleModuleRefTable.MId)] = item
                    });
                }
            });
            return this.Success();
        }
        
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object Del(int id)
        {
            this.ActionLog();
            this.UsingDb(db =>
            {
                var table = db.UwtGetTable<IDbRoleTable>();
                table.UwtUpdate(id, new Dictionary<string, object>()
                {
                    [nameof(IDbRoleTable.Valid)] = false
                });
            });
            return this.Success();
        }
    }
}