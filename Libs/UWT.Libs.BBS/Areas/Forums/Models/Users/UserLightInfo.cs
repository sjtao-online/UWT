using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Models.Users
{
    public class UserLightInfo : IdModel
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
        /// 等级名
        /// </summary>
        public string LevelName { get; set; }
    }
}
