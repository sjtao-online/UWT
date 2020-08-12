using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.BBS.Models;
using UWT.Libs.BBS.Areas.BBS.Models.ForumBasic;
using UWT.Libs.BBS.Models;
using UWT.Libs.BBS.Models.Const;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.BBS.Controllers
{
    [BBSRoute, AuthBBS]
    public class ForumBasicController : Controller
        , ITemplateController
    {
        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual object CreateTopic([FromBody]CreateTopicModel topic)
        {
            //  检测用户当前是否可以发
            
            using (var db = this.GetDB())
            {
                var bt = db.BeginTransaction();
                try
                {
                    //  创建主题主表
                    int topicId = db.TableTopic().InsertWithInt32Identity(() => new UwtBbsTopic()
                    {
                        Title = topic.Title,
                        Type = topic.Type
                    });
                    //  添加一个内容主体
                    int topicHisId = db.TableTopicHis().InsertWithInt32Identity(() => new UwtBbsTopicHis()
                    {
                        Content = topic.Content,
                        TId = topicId,
                        Title = topic.Title,
                        ApplyTime = null,
                        Status = topic.IsPublish ? TopicStatus.WaitApply : TopicStatus.Draft
                    });
                    //  给版块添加
                    foreach (var item in topic.AreaList)
                    {
                        var a = from it in db.TableArea() where it.Id == item && it.Status == AreaStatus.Show select it.Apply;
                        if (a.Count() == 1)
                        {
                            var status = "applying";
                            if (a.First() == "publish")
                            {
                                status = "publish";
                            }
                            db.TableAreaTopicRef().Insert(() => new UwtBbsAreaTopicRef()
                            {
                                TId = topicId,
                                AId = item,
                                HId = topicHisId,
                                Ex = "",
                                Status = status
                            });
                        }
                        else
                        {
                            bt.Rollback();
                            return this.Error("");
                        }
                    }
                    bt.Commit();
                }
                catch (Exception)
                {
                    bt.Rollback();
                    return this.Error("发贴失败");
                }
            }
            return this.Success();
        }

        [HttpPost]
        public virtual object CommentTopic([FromBody]CommentModel comment)
        {
            using (var db = this.GetDB())
            {
                var topic = from it in db.TableTopic()
                            where it.Id == comment.TopicId && it.Status == TopicStatus.Publish
                            select 0;
                if (topic.Count() == 0)
                {
                    return this.Error("回帖失败");
                }

                db.TableTopicBack().Insert(() => new UwtBbsTopicBack()
                {
                    TId = comment.TopicId,
                    TBId = comment.TopicBackId,
                    Status = "normal"

                });
            }
            return this.Success();
        }
    }
}
