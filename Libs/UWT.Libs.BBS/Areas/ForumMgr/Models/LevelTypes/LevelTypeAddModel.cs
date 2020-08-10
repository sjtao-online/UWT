using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.LevelTypes
{
    [FormModel]
    public class LevelTypeAddModel
    {
        [FormItem("名称")]
        [FormItems.Text(MaxLength = 100)]
        public string Name { get; set; }
    }

    [FormModel(".ModifyModel")]
    public class LevelTypeModifyModel : LevelTypeAddModel
    {
        [FormItem(FormItemType.Hidden)]
        public int Id { get; set; }
    }
}
