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


        public List<TopicItemModel> ItemList(int id, int areaId, int pageIndex, int pageSize, ref int count)
        {
            List<TopicItemModel> list = new List<TopicItemModel>();
            if (pageIndex == 0)
            {
                var q = from it in DataConnection.TableTopic()
                        join c in DataConnection.TableTopicHis() on it.Id equals c.TId into ls
                        from content in ls orderby content.AddTime descending where content.Status == TopicStatus.Publish
                        where it.Id == id && it.Status == TopicStatus.Publish
                        select new TopicItemModel()
                        {
                            Id = content.Id,
                            Content = content.Content,
                            FlowIndex = 0,
                            PostTime = it.AddTime,
                            UserId = it.CreateUserId
                        };
                if (q.Take(1).Count() == 0)
                {
                    return null;
                }
                var info = q.First();
                list.Add(info);
                pageSize -= 1;
            }
            var backs = from it in DataConnection.TableTopicBack()
                        join c in DataConnection.TableTopicBackHis() on it.Id equals c.TBId into ls
                        from content in ls orderby content.AddTime descending
                        where it.TId == id && it.Status == TopicStatus.Publish
                        select new TopicItemModel()
                        {
                            Id = it.Id,
                            FlowIndex = it.Index,
                            UserId = it.CreateUserId,
                            PostTime = it.AddTime
                        };
            count = backs.Count() + 1;
            if (count == 1)
            {
                return null;
            }
            list.AddRange(backs.UwtQueryPageSelector(pageIndex, pageSize));
            return list;
        }

        public void FillToCount(int tid, ref int vcount, ref int ccount)
        {
            var q = from it in DataConnection.TableTopic()
                    where it.Id == tid
                    select new
                    {
                        it.TouchCnt,
                        CCount = (from b in DataConnection.TableTopicBack() where it.Id == b.TId select 1).Count()
                    };
            if (q.Count() != 0)
            {
                var info = q.First();
                vcount = info.TouchCnt;
                ccount = info.CCount;
            }
        }

        public object Modify(ModifyTopicModel topic)
        {
            throw new NotImplementedException();
        }

        public object Comment(CommentModel comment)
        {
            //  检测用户当前是否可以回复

            using (var db = TemplateControllerEx.GetDB())
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
