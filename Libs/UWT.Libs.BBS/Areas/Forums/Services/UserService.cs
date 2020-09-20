using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.Models.Users;
using UWT.Libs.BBS.Models;
using UWT.Templates.Services.Extends;
using System.Linq;
using UWT.Libs.BBS.Models.Const;
using LinqToDB.Data;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    class UserService
    {
        public UserSimpleInfo Find(int id)
        {
            return Find<UserSimpleInfo>(id, null);
        }
        public TUserInfo Find<TUserInfo>(int id, Action<DataConnection, TUserInfo> dbCall)
            where TUserInfo : UserSimpleInfo, new()
        {
            using (var db = TemplateControllerEx.GetDB(null))
            {
                var qinfo = (from u in db.TableUser()
                            join level in db.TableUserLevel() on u.LevelTypeId equals level.TypeId
                            where u.Valid && u.Id == id
                            select new TUserInfo()
                            {
                                Id = u.Id,
                                NickName = u.Nickname,
                                Avatar = u.Avatar,
                                RoleName = level.Name,
                                TopicCnt = (from t in db.TableTopic() where t.CreateUserId == u.Id && t.Status == TopicStatus.Publish select t.Id).Count(),
                                FansCnt = 0,
                                FollowCnt = 0
                            }).Take(1);
                if (qinfo.Count() == 0)
                {
                    return null;
                }
                var info = qinfo.First();
                if (dbCall != null)
                {
                    dbCall(db, info);
                }
                return info;
            }
        }

        
    }
}
