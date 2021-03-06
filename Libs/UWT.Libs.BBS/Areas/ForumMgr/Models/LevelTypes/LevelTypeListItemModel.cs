﻿using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.LevelTypes
{
    [ListViewModel("等级类型")]
    public class LevelTypeListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("名称")]
        public string Name { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<Templates.Models.Templates.Commons.HandleModel> HandleList
        {
            get
            {
                List<Templates.Models.Templates.Commons.HandleModel> handles = new List<Templates.Models.Templates.Commons.HandleModel>();
                handles.Add(HandleModel.BuildModify(Id));
                handles.Add(HandleModel.BuildDel(Id));
                return handles;
            }
        }
    }
}
