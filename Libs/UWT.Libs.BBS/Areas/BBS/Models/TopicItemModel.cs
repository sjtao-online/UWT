using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class TopicItemModel : IdModel
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public int FlowIndex { get; set; }
    }
}
