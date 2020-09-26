using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.Models.Areas;
using UWT.Libs.BBS.Models;
using UWT.Templates.Services.Extends;
using System.Linq;
using UWT.Templates.Models.Basics;
using UWT.Libs.BBS.Models.Const;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    public class AreaService : BBSService
    {
        public List<UrlTitleIdModel> GetCrumbListFromAreaId(int id)
        {
            List<UrlTitleIdModel> list = new List<UrlTitleIdModel>();
            if (id == 0)
            {
                return list;
            }
            var q = from it in DataConnection.TableArea()
                    where it.Id == id
                    select new
                    {
                        it.Id,
                        it.Name,
                        it.PId
                    };
            if (q.Count() == 0)
            {
                return list;
            }
            var info = q.First();
            list.AddRange(GetCrumbListFromAreaId(info.PId));
            list.Add(new UrlTitleIdModel()
            {
                Url = "/bbs/area/" + info.Id,
                Title = info.Name,
                Id = info.Id
            });
            return list;
        }

        public List<UrlTitleIdModel> GetCrumbListFromTopicId(int id, int areaId)
        {
            List<UrlTitleIdModel> list = new List<UrlTitleIdModel>();
            var q = from it in DataConnection.TableTopic()
                         where it.Id == id && it.Status == TopicStatus.Publish
                         select it.Title;
            if (q.Count() != 0)
            {
                var info = q.First();
                list.AddRange(GetCrumbListFromAreaId(areaId));
                list.Add(new UrlTitleIdModel()
                {
                    Title = info,
                    Url = "/bbs/area/topic?id=" + id
                });
            }
            return list;
        }
        /// <summary>
        /// 获得主页面版块信息
        /// </summary>
        /// <returns></returns>
        public List<AreaModel> GetHomeAreaList()
        {
            var areas = (from it in DataConnection.TableArea()
                            where it.PId == 0 && it.Status == "show" && it.Apply == "publish"
                            select new AreaModel()
                            {
                                Id = it.Id,
                                Title = it.Name,
                                Summary = it.Summary,
                                Icon = it.Icon
                            }).ToList();
            foreach (var item in areas)
            {
                item.Children = FillChildren(item.Id);
            }
            return areas;
        }
        /// <summary>
        /// 获得本版块及子版块列表信息
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public AreaModel GetAresInfoSubAreaList(int areaId)
        {
            using (var db = TemplateControllerEx.GetDB(null))
            {
                var qinfo = from it in db.TableArea()
                           where it.Status == "show" && it.Apply == "publish" && it.Id == areaId
                           select new AreaModel()
                           {
                               Id = it.Id,
                               Title = it.Name,
                               Summary = it.Summary,
                               Icon = it.Icon
                           };
                if (qinfo.Count() != 0)
                {
                    var info = qinfo.First();
                    info.TopicCount = (from it in db.TableAreaTopicRef() where it.AId == areaId select 1).Count();
                    info.CommentCount = (from it in db.TableTopicBack()
                                         join a in db.TableAreaTopicRef() on it.TId equals a.TId
                                         where a.AId == areaId
                                         group it by it.TId into x
                                         select 1).Count();
                    info.Children = FillChildren(areaId);
                    return info;
                }
                return null;
            }
        }
        private List<SubAreaInfoModel> FillChildren(int areaId)
        {
            List<SubAreaInfoModel> children = (from it in DataConnection.TableArea()
                                               where it.PId == areaId && it.Apply == "publish"
                                               select new SubAreaInfoModel()
                                               {
                                                   Id = it.Id,
                                                   Title = it.Name,
                                                   Summary = it.Summary,
                                                   Icon = it.Icon,
                                               }).ToList();
            foreach (var area in children)
            {
                area.TopicCount = (from it in DataConnection.TableAreaTopicRef() where it.AId == area.Id select 1).Count();
                area.CommentCount = (from it in DataConnection.TableTopicBack()
                                     join a in DataConnection.TableAreaTopicRef() on it.TId equals a.TId
                                     where a.AId == area.Id
                                     group it by it.TId into x
                                     select 1).Count();
                area.LastComment = (from it in DataConnection.TableTopicBack()
                                    join a in DataConnection.TableAreaTopicRef() on it.TId equals a.TId
                                    join u in DataConnection.TableUser() on it.CreateUserId equals u.Id
                                    where a.AId == area.Id
                                    orderby it.Id descending
                                    select new CommentInfoModel()
                                    {
                                        Id = it.Id,
                                        UserName = u.Nickname,
                                        CommentTime = it.AddTime,
                                        UserId = it.CreateUserId
                                    }).FirstOrDefault();
            }
            return children;
        }
    }
}
