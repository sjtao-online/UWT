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

namespace UWT.Libs.Users.MenuGroups
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [ListViewModel("菜单组列表")]
    public class MenuGroupListItemModel
    {
        [ListColumn("编号", ColumnType = ColumnType.Text)]
        public int Id { get; set; }
        [ListColumn("组名", ColumnType = ColumnType.Text)]
        public string Name { get; set; }
        [ListColumn("备注", ColumnType = ColumnType.Text)]
        public string Desc { get; set; }
        [ListColumn("页面数", ColumnType = ColumnType.Text)]
        public int PageCount { get; set; }
        [ListColumn("权限数", ColumnType = ColumnType.Text)]
        public int AuthCount { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> Handles
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                handles.Add(new HandleModel()
                {
                    Title = "编辑",
                    Type = HandleModel.TypeTagNavigate,
                    Target = "/MenuGroups/Modify?id=" + Id
                });
                handles.Add(new HandleModel()
                {
                    Title = "编辑树",
                    Type = HandleModel.TypeTagNavigate,
                    Target = "/MenuGroups/ModifyTree?id=" + Id
                });
                handles.Add(new HandleModel()
                {
                    Title = "删除",
                    Class = HandleModel.ClassBtnDel,
                    Target = "/MenuGroups/Del?Id=" + Id,
                    AskTooltip = HandleModel.TipDel,
                    Type = HandleModel.TypeTagApiPost
                });
                return handles;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}