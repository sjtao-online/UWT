using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.BBS.Models.AreaMgr
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
        [FormItem("图标", FormItemType.File)]
        [FormItems.File(FileType = FileTypeConst.Image)]
        public string Icon { get; set; }
        [FormItem("备注")]
        [FormItems.Text(MaxLength = 255)]
        public string Desc { get; set; }
        [FormItem("父级版块", FormItemType.ChooseId)]
        [FormItems.ChooseIdFromTable("${BbsAreaTableName}", ParentIdColumnName = "p_id")]
        public int PId { get; set; }
        [FormItem("版主")]
        [FormItems.ChooseIdFromTable("${BbsUserTableName}", NameColumnName = "nick_name")]
        public int MgrId { get; set; }
    }
}
