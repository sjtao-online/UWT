﻿using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Models.Users
{
    public class UserSimpleInfo : UserLightInfo
    {
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
