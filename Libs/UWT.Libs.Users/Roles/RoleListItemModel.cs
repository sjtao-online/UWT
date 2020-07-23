using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Consts;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.Users.Roles
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [ListViewModel("列表")]
    public class RoleListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("名称", ColumnType = ColumnType.Text)]
        public string Name { get; set; }
        [ListColumn("备注", ColumnType = ColumnType.Text)]
        public string Desc { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> Handles
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                handles.Add(HandleModel.BuildModify("/Roles/Modify?id=" + Id));
                handles.Add(HandleModel.BuildDel("/Roles/Del?id=" + Id));
                return handles;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}