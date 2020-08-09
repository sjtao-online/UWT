using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.Areas
{
    [ListViewModel]
    class AreaMgrListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("版块名")]
        public string Name { get; set; }
        [ListColumn("状态")]
        public string Status { get; set; }
        [ListColumn("版主")]
        public string MgrName { get; set; }
        [ListColumn("父版块")]
        public string PName { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<Templates.Models.Templates.Commons.HandleModel> HandleList
        {
            get
            {
                List<Templates.Models.Templates.Commons.HandleModel> handles = new List<Templates.Models.Templates.Commons.HandleModel>();
                handles.Add(HandleModel.BuildModify(".Modify?id=" + Id));
                handles.Add(HandleModel.BuildDel(".Del?id=" + Id));
                return handles;
            }
        }
    }
}
