using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Consts;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.Config
{
    [FormModel("/ForumMgr/Config/Commit", HandleBtnsInTitleBar = true)]
    [FormCshtml(FormCshtmlPosition.Header, "/Areas/ForumMgr/Views/Config/Index.cshtml")]
    public class ConfigModel
    {
        [FormItem("论坛名称", IsRequire = true)]
        [FormItems.Text(MaxLength = 10)]
        public string Name { get; set; }
        [FormItem("Logo", FormItemType.File, IsRequire = true, Tooltip = "建议200 * 40，大小")]
        [FormItems.File(FileType = FileTypeConst.Image)]
        public string Logo { get; set; }
    }
}
