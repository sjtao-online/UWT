using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.Areas
{
    [FormModel]
    public class AreaMgrAddModel
    {
        [FormItem("名称", IsRequire = true)]
        [FormItems.Text(MaxLength = 32, MinLength = 2)]
        public string Name { get; set; }
        [FormItem("摘要")]
        [FormItems.Text(MaxLength = 100)]
        public string Summary { get; set; }
        [FormItem("图标", FormItemType.File, Tooltip = "建议使用正方形图片")]
        [FormItems.File(FileType = FileTypeConst.Image)]
        public string Icon { get; set; }
        [FormItem("备注")]
        [FormItems.Text(MaxLength = 255)]
        public string Desc { get; set; }
        [FormItem("父级版块", FormItemType.ChooseId)]
        [FormItems.ChooseIdFromTable("uwt_bbs_areas", ParentIdColumnName = "p_id")]
        public int PId { get; set; }
        [FormItem("版主", FormItemType.ChooseId, IsRequire = true)]
        [FormItems.ChooseIdFromTable("uwt_bbs_users", NameColumnName = "nickname")]
        public int MgrId { get; set; }
    }
    [FormModel(".ModifyModel")]
    public class AreaMgrModifyModel : AreaMgrAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
}
