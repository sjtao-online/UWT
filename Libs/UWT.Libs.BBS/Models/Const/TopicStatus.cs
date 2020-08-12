using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.BBS.Models.Const
{
    public class TopicStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        public const string Draft = "draft";
        /// <summary>
        /// 等待审核
        /// </summary>
        public const string WaitApply = "wait_apply";
        /// <summary>
        /// 发布
        /// </summary>
        public const string Publish = "publish";
        /// <summary>
        /// 禁止
        /// </summary>
        public const string Forbid = "forbid";
    }
}
