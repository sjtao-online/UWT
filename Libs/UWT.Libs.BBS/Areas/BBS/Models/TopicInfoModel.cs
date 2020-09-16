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
        public TopicCate Category { get; set; }
    }
    public enum TopicCate
    {
        discuss,
        question,
        vote
    }
}
