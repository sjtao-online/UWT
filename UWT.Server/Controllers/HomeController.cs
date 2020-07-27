﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Auths;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Consts;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Services.Extends;

namespace UWT.Server.Controllers
{
    [AuthUser]
    public class HomeController : Controller
        , IFormToPage<StudentAddModel>
        , IListToPage<QueryTable, HomeListItemModel>
    {
        public IActionResult Index()
        {
            this.AddFilter("123", m => m.Name, Templates.Models.Filters.FilterType.Equal, Templates.Models.Filters.FilterValueType.TagSSelector, new List<HasFilterTypeChildrenNameKeyModel>()
            {
                new HasFilterTypeChildrenNameKeyModel()
                {
                    Key = "",
                    Name = "未选择"
                },
                new HasFilterTypeChildrenNameKeyModel()
                {
                    Key = "123",
                    FilterType = Templates.Models.Filters.FilterType.NotEqual,
                    Name = "非123",
                },
                new HasFilterTypeChildrenNameKeyModel()
                {
                    Key = "123",
                    Name = "123"
                }
            });
            this.AddHandler("111", "aaa");
            this.AddFilterFromCshtml("/Views/Home/Index3.cshtml", "search", "reset", "init", "value");
            //this.AddFilter("用户名", m => m.Name, Templates.Models.Filters.FilterType.Like, Templates.Models.Filters.FilterValueType.Text);
            using (DataModels.UwtDB db = new DataModels.UwtDB())
            {
                return this.ListResult(m => new HomeListItemModel()
                {
                    Name = m.Name
                }, from it in db.UwtUsersAccounts
                   select new QueryTable()
                   {
                       Name = it.Account
                   }).View();
            }
        }

        public IActionResult Form()
        {
            return this.FormResult().View();
        }

        public IActionResult Logs()
        {
            ViewBag.Title = "天天天";
            return View("Index");
        }
    }
    class QueryTable
    {
        public string Name { get; set; }
    }
    [ListViewModel]
    class HomeListItemModel
    {
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index1 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index2 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index3 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index4 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index5 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index6 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index7 { get; set; }
        [ListColumn("序号", ColumnType = ColumnType.Index)]
        public int Index8 { get; set; }
        public string Name { get; set; }
        [ListColumn("操作", ColumnType =  ColumnType.Handle, Styles = "width: 200px;")]
        public List<HandleModel> HandleList
        {
            get
            {
                var list = new List<HandleModel>();
                list.Add(HandleModel.BuildEvalJS("复制", "clipboardCopy('123')", "确定要复制吗？", "复制"));
                list.Add(HandleModel.BuildDel(".Del"));
                list.Add(HandleModel.BuildDownload("下载", ".download"));
                list.Add(HandleModel.BuildNavigate("详情", ".detail?id=" + Index, "确定要看详情吗？"));
                list.Add(HandleModel.BuildPopupDlg("弹出", ".PopupDlg", "", ""));
                list.Add(HandleModel.BuildMultiButtons("更多", new List<HandleModel>()
                {
                    HandleModel.BuildDel(".Abc"),
                    HandleModel.BuildDel(".Abc"),
                    HandleModel.BuildDel(".Abc"),
                }));
                return list;
            }
        }
    }
    [FormModel(BackUrl = "/Home/Index")]
    [FormHandler("提交")]
    public class StudentAddModel
    {
        [FormItem("列名", FormItemType.File)]
        [FormItems.File(FileType = ".pdf")]
        public string File { get; set; }
    }
    class MBulder : SelectItemBuilderBasic
    {
        public override List<NameKeyModel> BuildItemList()
        {
            return new List<NameKeyModel>()
            {
                new NameKeyModel()
                {
                    Key = "ss1",
                    Name = "SS1"
                },
                new NameKeyModel()
                {
                    Key = "ss2",
                    Name = "SS2"
                },
                new NameKeyModel()
                {
                    Key = "ss3",
                    Name = "SS3"
                }
            };
        }
    }
}