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
        public List<Templates.Models.Templates.Commons.HandleModel> HandleList
        {
            get
            {
                List<Templates.Models.Templates.Commons.HandleModel> handles = new List<Templates.Models.Templates.Commons.HandleModel>();
                handles.Add(new Templates.Models.Templates.Commons.HandleModel()
                {
                    Title = "编辑",
                    Target = "/${BannerController}/Modify?Id=" + Id,
                    Type = Templates.Models.Templates.Commons.HandleModel.TypeTagNavigate
                });
                handles.Add(new Templates.Models.Templates.Commons.HandleModel()
                {
                    Title = "删除",
                    Target = "/${BannerController}/Del?Id=" + Id,
                    Type = Templates.Models.Templates.Commons.HandleModel.TypeTagApiPost,
                    Class = HandleModel.ClassBtnDel,
                    AskTooltip = HandleModel.TipDel
                });
                handles.Add(new Templates.Models.Templates.Commons.HandleModel()
                {
                    Title = "详情",
                    Target = "/${BannerController}/Detail?Id=" + Id,
                    Type = Templates.Models.Templates.Commons.HandleModel.TypeTagNavigate
                });
                return handles;
            }
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
