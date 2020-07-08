using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Details;

namespace UWT.Libs.Normals.Banners
{
    [DetailModel]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class BannerDetailModel
    {
        [DetailItem("标题")]
        public string Title { get; set; }
        [DetailItem("副标题")]
        public string SubTitle { get; set; }
        [DetailItem("显示图片", DetailItemCategory.Image)]
        public string Image { get; set; }
        [DetailItem("编号")]
        public int Index { get; set; }
        [DetailItem("说明")]
        public string Desc { get; set; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
