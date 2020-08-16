using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.Models.Areas;
using UWT.Libs.BBS.Models;
using UWT.Templates.Services.Extends;
using System.Linq;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    public class AreaService
    {
        /// <summary>
        /// 获得
        /// </summary>
        /// <returns></returns>
        public List<AreaModel> GetHomeAreaList()
        {
            using (var db = TemplateControllerEx.GetDB(null))
            {
                var areas = (from it in db.TableArea()
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
                    item.Children = FillChildren(db, item.Id);
                }
                return areas;
            }
        }
        private List<SubAreaInfoModel> FillChildren(LinqToDB.Data.DataConnection db, int areaId)
        {
            List<SubAreaInfoModel> children = (from it in db.TableArea()
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
                area.TopicCount = (from it in db.TableAreaTopicRef() where it.AId == area.Id select 1).Count();
                area.CommentCount = (from it in db.TableTopicBack()
                                     join a in db.TableAreaTopicRef() on it.TId equals a.TId
                                     where a.AId == area.Id
                                     group it by it.TId into x
                                     select 1).Count();
                area.LastComment = (from it in db.TableTopicBack()
                                    join a in db.TableAreaTopicRef() on it.TId equals a.TId
                                    join u in db.TableUser() on it.CreateUserId equals u.Id
                                    where a.AId == area.Id
                                    orderby it.Id descending
                                    select new CommentInfoModel()
                                    {
                                        Id = it.Id,
                                        UserName = u.Nickname,
                                        CommentTime = it.AddTime,
                                        UserId = it.CreateUserId
                                    }).First();
            }
            return children;
        }
    }
}
