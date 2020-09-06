using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Models.Areas
{
    public class AreaModel : IconTitleIdModel
    {
        /// <summary>
        /// 版块摘要
        /// </summary>
        public string Summary { get; set; }
        public List<SubAreaInfoModel> Children { get; set; }
        /// <summary>
        /// 主题数
        /// </summary>
        public int TopicCount { get; set; }
        /// <summary>
        /// 总帧数
        /// </summary>
        public int CommentCount { get; set; }
    }
    public class SubAreaInfoModel : AreaModel
    {
        /// <summary>
        /// 最后回复
        /// </summary>
        public CommentInfoModel LastComment { get; set; }
    }
    public class CommentInfoModel : IdModel
    {
        /// <summary>
        /// 该回复的UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 该回复的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CommentTime { get; set; }
    }
}
