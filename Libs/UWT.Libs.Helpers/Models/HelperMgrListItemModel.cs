using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.Helpers.Models
{
    [ListViewModel("帮助列表")]
    class HelperMgrListItemModel : HelperListItemModel
    {
        [ListColumn("操作", Index = int.MaxValue, ColumnType = ColumnType.Handle)]
        public List<HandleModel> HandleList
        {
            get
            {
                var list = new List<HandleModel>();
                list.Add(HandleModel.BuildNavigate("预览", "/Helpers/Detail?id=" + Id));
                if (Publish == "-")
                {
                    list.Add(HandleModel.BuildNavigate("编辑", "/HelperMgr/Modify?id=" + Id));
                    list.Add(HandleModel.BuildPublish("/HelperMgr/Publish?id=" + Id));
                }
                else
                {
                    list.Add(HandleModel.BuildPublishRemove("/HelperMgr/PublishRemove?id=" + Id));
                }
                list.Add(HandleModel.BuildDel("/HelperMgr/Del?id=" + Id));
                return list;
            }
        }
    }
}
