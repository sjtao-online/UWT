using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.BBS.Areas.Forums.ServiceModels.Topic
{
    public class CreateTopicModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型<br/>
        /// 'discuss','question','vote'
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 内容主体
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 版块
        /// </summary>
        public List<int> AreaList { get; set; }
        /// <summary>
        /// false为保存
        /// </summary>
        public bool IsPublish { get; set; }
    }
    public class ModifyTopicModel : CreateTopicModel
    {
        public int Id { get; set; }
    }
}
