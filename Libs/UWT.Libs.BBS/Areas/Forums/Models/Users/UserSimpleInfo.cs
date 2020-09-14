using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Models.Users
{
    public class UserSimpleInfo : IdModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
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
