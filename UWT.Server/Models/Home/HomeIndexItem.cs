using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Lists;

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
        public List<Templates.Models.Templates.Commons.HandleModel> HandleList
        {
            get
            {
                List<Templates.Models.Templates.Commons.HandleModel> handles = new List<Templates.Models.Templates.Commons.HandleModel>();
                handles.Add(new Templates.Models.Templates.Commons.HandleModel()
                {
                    Title = "编辑",
                    Target = "/Home/Modify?id=" + Id
                });
                return handles;
            }
        }
    }
}
