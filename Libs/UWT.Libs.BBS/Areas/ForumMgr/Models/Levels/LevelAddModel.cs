using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.Levels
{
    [FormModel]
    public class LevelAddModel
    {
        [FormItem("名称", IsRequire = true)]
        [FormItems.Text(MaxLength = 20, MinLength = 1)]
        public string Name { get; set; }
        [FormItem("经验值", FormItemType.Integer, IsRequire = true)]
        [FormItems.Integer(Max = uint.MaxValue, Min = 0)]
        public uint Exp { get; set; }
        [FormItem("图标", FormItemType.File)]
        [FormItems.File(FileType = FileTypeConst.Image, MaxSize = FileSizeConst.KB * 200)]
        public string Icon { get; set; }
        [FormItem("类别", FormItemType.ChooseId, IsRequire = true)]
        [FormItems.ChooseIdFromTable("uwt_bbs_user_level_types", Where = "valid = 1")]
        public int TypeId { get; set; }
    }
    [FormModel(".ModifyModel")]
    public class LevelModifyModel : LevelAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
}
