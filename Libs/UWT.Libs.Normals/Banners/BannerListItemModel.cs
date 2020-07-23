using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.Normals.Banners
{
    [ListViewModel("Banner列表")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class BannerListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("标题")]
        public string Title { get; set; }
        [ListColumn("说明")]
        public string Desc { get; set; }
        [ListColumn("图片", ColumnType = ColumnType.Image)]
        public string ImageUrl { get; set; }
        [ListColumn("操作", Index = int.MaxValue, ColumnType = ColumnType.Handle)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                handles.Add(HandleModel.BuildModify("/${BannerController}/Modify?Id=" + Id));
                handles.Add(HandleModel.BuildDel("/${BannerController}/Del?Id=" + Id));
                handles.Add(HandleModel.BuildNavigate("详情", "/${BannerController}/Detail?Id=" + Id));
                return handles;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
