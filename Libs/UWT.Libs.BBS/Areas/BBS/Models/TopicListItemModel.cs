using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class TopicListItemModel : TopicInfoModel
    {
        public IconNameIdModel UserInfo { get; set; }
        public LastCommitInfo LastCommitInfo { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
