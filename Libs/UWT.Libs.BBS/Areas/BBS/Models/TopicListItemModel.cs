using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class TopicListItemModel : TitleIdModel
    {
        public IconNameIdModel UserInfo { get; set; }
        public LastCommitInfo LastCommitInfo { get; set; }
        public bool IsHot { get; set; }
        public int TopLevel { get; set; }
        public int VisitorCount { get; set; }
        public int CommitCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
