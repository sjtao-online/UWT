using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;

namespace UWT.Libs.BBS.Models.AreaMgr
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

                return handles;
            }
        }
    }
}
