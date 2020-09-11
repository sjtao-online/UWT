using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class UserInfoModel : IconNameIdModel
    {
        public string RoleName { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCnt { get; set; }
        /// <summary>
        /// 发贴数
        /// </summary>
        public int TopicCnt { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int FollowCnt { get; set; }
    }
}
