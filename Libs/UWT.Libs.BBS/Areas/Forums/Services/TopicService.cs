using LinqToDB;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.ServiceModels.Topic;
using UWT.Libs.BBS.Models;
using UWT.Libs.BBS.Models.Const;
using UWT.Templates.Services.Extends;
using UWT.Libs.BBS.Areas.BBS.Models;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    public class TopicService : BBSService
    {
        public object Create(CreateTopicModel topic)
        {
            //  检测用户当前是否可以发

            var bt = DataConnection.BeginTransaction();
            try
            {
                //  创建主题主表
                int topicId = DataConnection.TableTopic().InsertWithInt32Identity(() => new UwtBbsTopic()
                {
                    Title = topic.Title,
                    Type = topic.Type
                });
                //  添加一个内容主体
                int topicHisId = DataConnection.TableTopicHis().InsertWithInt32Identity(() => new UwtBbsTopicHis()
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
                    var a = from it in DataConnection.TableArea() where it.Id == item && it.Status == AreaStatus.Show select it.Apply;
                    if (a.Count() == 1)
                    {
                        var status = "applying";
                        if (a.First() == "publish")
                        {
                            status = "publish";
                        }
                        DataConnection.TableAreaTopicRef().Insert(() => new UwtBbsAreaTopicRef()
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
                        return ControllerEx.Error(null, "");
                    }
                }
                bt.Commit();
            }
            catch (Exception)
            {
                bt.Rollback();
                return ControllerEx.Error(null, "发贴失败");
            }
            return ControllerEx.Success(null);
        }

        public object Comment(CommentModel comment)
        {
            //  检测用户当前是否可以回复

            using (var db = TemplateControllerEx.GetDB(null))
            {
                var topic = from it in db.TableTopic()
                            where it.Id == comment.TopicId && it.Status == TopicStatus.Publish
                            select 0;
                if (topic.Count() == 0)
                {
                    return ControllerEx.Error(null, "回帖失败");
                }

                db.TableTopicBack().Insert(() => new UwtBbsTopicBack()
                {
                    TId = comment.TopicId,
                    TBId = comment.TopicBackId,
                    Status = "normal"

                });
            }
            return ControllerEx.Success(null);

        }

        public object ListFromCreator(int uid, int pageIndex, int pageSize)
        {
            var topicList = from it in DataConnection.TableTopic()
                            select new TopicListItemModel()
                            {
                                Id = it.Id,
                                Category = Enum.Parse<TopicCate>(it.Type),
                                Title = it.Title,
                                IsHot = false,
                                VisitorCount = it.TouchCnt,
                                CreateTime = it.AddTime,
                            };
            
            return ControllerEx.Success(null, topicList.ToList());
        }

        public object List(int areaId, bool isPostdate, int pageIndex, int pageSize)
        {
            var topics = from it in DataConnection.TableAreaTopicRef()
                         where it.AId == areaId && it.Status == TopicStatus.Publish
                         group it by it.TId into g
                         select g;
            return ControllerEx.Success(null);
        }
    }
}
