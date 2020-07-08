using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.Normals.Banners
{
    [FormModel("/${BannerController}/AddModel")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class BannerAddModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>
    {
        [FormItem("标题")]
        public string Title { get; set; }
        [FormItem("副标题")]
        public string SubTitle { get; set; }
        [FormItem("链接类型", FormItemType.SimpleSelect)]
        [FormItems.SimpleSelect(0, BuilderTemplateTypeIndex = 2)]
        public string Type { get; set; }
        [FormItem("链接")]
        public string TargetUrl { get; set; }
        [FormItem("图片", ItemType = FormItemType.File, Tooltip = "${BannerSizeTip}", IsInline = true)]
        [FormItems.File(FileType = FileTypeConst.Image)]
        public string ImageUrl { get; set; }
        [FormItem("次序", FormItemType.Integer, IsInline = true)]
        [FormItems.Integer(Min = -1)]
        public int Index { get; set; }
        [FormItem("Banner类型", FormItemType.SimpleSelect, IsInline = true)]
        [FormItems.SimpleSelect(0, BuilderTemplateTypeIndex = 1)]
        public string Cate { get; set; }
        [FormItem("备注")]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.AreaText)]
        public string Desc { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
