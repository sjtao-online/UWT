using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.Forums.ServiceModels.Topic;
using UWT.Libs.BBS.Areas.Forums.Services;

namespace UWT.Libs.BBS.Areas.Forums.Controllers
{
    public class TopicController : Controller
    {
        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object CreateTopic([FromBody] CreateTopicModel topic)
        {
            return TopicService.Create(topic);
        }

        public object ModifyTopic([FromBody] ModifyTopicModel topic)
        {
            return TopicService.Modify(topic);
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object CommentTopic([FromBody] CommentModel comment)
        {
            return TopicService.Comment(comment);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object TopicList(int areaId, int pageIndex, int pageSize)
        {
            return TopicService.List(areaId, false, pageIndex, pageSize);
        }
        TopicService TopicService = new TopicService();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            TopicService.Dispose();
        }
    }
}
