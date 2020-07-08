using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Consts;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.Users.Roles
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [FormModel("/Roles/AddModel")]
    public class RoleAddModel
    {
        [FormItem("名称", IsRequire = true)]
        public string Name { get; set; }
        [FormItem("说明")]
        public string Desc { get; set; }
        [FormItem("菜单组", FormItemType.ChooseId, IsRequire = true)]
        [FormItems.ChooseIdFromTable("${MenuGroupTableName}", Where = "valid = 1")]
        public int MenuGroupId { get; set; }
        [FormItem("主页", FormItemType.ChooseId, Tooltip = "登录后默认跳转的页面", IsRequire = true)]
        [FormItems.ChooseIdFromTable("${ModulesTableName}", NameColumnName = "url", Where = "type = 'page'")]
        public int HomePageUrl { get; set; }
        [FormItem("可用的API", FormItemType.ChooseId)]
        [FormItems.ChooseIdFromTable("${ModulesTableName}", NameColumnName = "url")]
        public List<int> Urls { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}