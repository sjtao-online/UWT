using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class TopicInfoModel : TitleIdModel
    {
        public bool IsHot { get; set; }
        public int TopLevel { get; set; }
        public int VisitorCount { get; set; }
        public int CommitCount { get; set; }
    }
}
