using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Interfaces;

namespace UWT.Server.Models.Home
{
    [FormModel(".AddModel", Method = "POST")]
    public class AddHomeModel
    {
        [FormItem("名称", ItemType = FormItemType.Text, TitleIsFullScreen = true)]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.RichText)]
        public string Name { get; set; }
        [FormItem("列名", ItemType = FormItemType.Integer, IsRequire = true, IsInline = true)]
        [FormItems.Integer(Max = 100, Min = 20)]
        public Range<int> Ins { get; set; }
        [FormItem("日期时间", ItemType = FormItemType.DateTime, IsRequire = true, IsInline = true)]
        [FormItems.DateTime(Max = "2030-10-1", Min = "2020-10-1")]
        public DateTime Dt { get; set; }
        [FormItem("日期时间", ItemType = FormItemType.DateTime, IsRequire = true, IsInline = true)]
        [FormItems.DateTime(Max = "2030-10-1", Min = "2020-1-1")]
        public Range<DateTime> DtX { get; set; }
        [FormItem("Slider", ItemType = FormItemType.Slider, IsInline = true)]
        [FormItems.Slider(Min = 4, Max = 100)]
        public Range<int> Dt2 { get; set; }
        [FormItem("Slider", ItemType = FormItemType.Slider, TitleIsFullScreen = true)]
        [FormItems.Slider(Min = 4, Max = 100)]
        public int Dt3 { get; set; }
        [FormItem("文件", FormItemType.File)]
        [FormItems.File(FileType = "file", MaxSize = Templates.Models.Consts.FileSizeConst.MB, CanFilter = true, CanSelectReadyAll = true, CanLinkOther = true)]
        public string UploadFile { get; set; }
        [FormItem("颜色选择器", FormItemType.Color)]

        public string ColorPicker { get; set; }
    }
    [FormModel(".ModifyModel", Method = "POST")]
    public class ModifyHomeModel
    {
        [FormItem("", ItemType = FormItemType.Hidden)]
        public int Id { get; set; }
        [FormItem("名称", ItemType = FormItemType.Text)]
        [FormItems.Text(TextCate = FormItems.TextAttribute.Cate.RichText)]
        public string Name { get; set; }
        [FormItem("列名", ItemType = FormItemType.Integer)]
        [FormItems.Integer(Max = 100, Min = 20)]
        public Range<int> Ins { get; set; }
        [FormItem("日期时间", ItemType = FormItemType.DateTime)]
        [FormItems.DateTime(Max = "2030-10-1", Min = "2020-10-1")]
        public DateTime Dt { get; set; }
        [FormItem("Slider", ItemType = FormItemType.Slider)]
        [FormItems.Slider(Min = 4, Max = 100)]
        public Range<int> Dt2 { get; set; }
        [FormItem("Slider", ItemType = FormItemType.Slider)]
        [FormItems.Slider(Min = 4, Max = 100)]
        public int Dt3 { get; set; }
        [FormItem("文件", FormItemType.File)]
        [FormItems.File(FileType = "file", MaxSize = Templates.Models.Consts.FileSizeConst.MB, CanFilter = true, CanSelectReadyAll = true, CanLinkOther = true)]
        public string UploadFile { get; set; }
    }
}
