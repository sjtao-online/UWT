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
                list.Add(new HandleModel()
                {
                    Title = "预览",
                    Target = "/Helpers/Detail?id=" + Id,
                    Type = HandleModel.TypeTagNavigate
                });
                if (Publish == "-")
                {
                    list.Add(new HandleModel()
                    {
                        Title = "编辑",
                        Target = "/HelperMgr/Modify?id=" + Id
                    });
                    list.Add(new HandleModel()
                    {
                        Title = "发布",
                        Target = "/HelperMgr/Publish?id=" + Id,
                        Type = HandleModel.TypeTagApiPost,
                        AskTooltip = HandleModel.TipPublishRemove
                    });
                }
                else
                {
                    list.Add(new HandleModel()
                    {
                        Title = "撤下",
                        Target = "/HelperMgr/PublishRemove?id=" + Id,
                        Type = HandleModel.TypeTagApiPost,
                        AskTooltip = HandleModel.TipPublishRemove
                    });
                }
                list.Add(new HandleModel()
                {
                    Title = "删除",
                    Target = "/HelperMgr/Del?id=" + Id,
                    Type = HandleModel.TypeTagApiPost,
                    AskTooltip = HandleModel.TipDel
                });
                return list;
            }
        }
    }
}
