using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Server.Models.Home
{
    [ListViewModel(BatchKey = "Id")]
    public class HomeIndexItem
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("名称")]
        public string Name { get; set; }
        [ListColumn("备注")]
        public string Desc { get; set; }
        [ListColumn("操作", Index = int.MaxValue, ColumnType = ColumnType.Handle)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                handles.Add(HandleModel.BuildModify("/Home/Modify?id=" + Id));
                return handles;
            }
        }
    }
}
