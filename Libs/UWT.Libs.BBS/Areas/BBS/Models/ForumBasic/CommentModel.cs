using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.BBS.Areas.BBS.Models.ForumBasic
{
    public class CommentModel
    {
        /// <summary>
        /// 哪个话题
        /// </summary>
        public int TopicId { get; set; }
        /// <summary>
        /// 哪一楼 0为主楼 话题本身
        /// </summary>
        public int TopicBackId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }
    }
}
