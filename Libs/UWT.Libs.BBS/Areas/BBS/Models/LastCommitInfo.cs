using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class LastCommitInfo : NameIdModel
    {
        public DateTime Time { get; set; }
        public string Url { get; set; }
    }
}
