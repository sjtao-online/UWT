using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.Models.Users;
using UWT.Libs.BBS.Models;
using UWT.Templates.Services.Extends;
using System.Linq;
using UWT.Libs.BBS.Models.Const;
using LinqToDB.Data;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    class UserService : IBBSService
    {
        public UserSimpleInfo Find(int id)
        {
            return Find<UserSimpleInfo>(id, null, true);
        }
        public TUserInfo Find<TUserInfo>(int id, Action<DataConnection, TUserInfo> dbCall, bool fillCount = false)
            where TUserInfo : UserLightInfo, new()
        {
            using (var db = this.GetDB())
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
                            }).Take(1);
                if (qinfo.Count() == 0)
                {
                    return null;
                }
                var info = qinfo.First();
                if (info is UserSimpleInfo)
                {
                    FillUserSimpleCount(db, info as UserSimpleInfo);
                }
                if (dbCall != null)
                {
                    dbCall(db, info);
                }
                return info;
            }
        }

        public List<UserPropertiesModel> GetProperties(int uid)
        {
            using (var db = this.GetDB())
            {
                var qprop = from it in db.TableUserPropGroup()
                            select new UserPropertiesModel()
                            {
                                GroupName = it.Name,
                                TitleMap = (from c in db.TableUserPropConfig() where c.GId == it.Id select new { c.Id, c.Name }).ToDictionary(m => m.Id, m => m.Name),
                                Children = (from p in db.TableUserProperty() join c in db.TableUserPropConfig() on p.PId equals c.Id where c.GId == it.Id select new TitleIdModel() { Id = p.Id, Title = p.Value }).ToList()
                            };
                return qprop.ToList();
            }
        }

        public List<TUserInfo> GetFollows<TUserInfo>(int uid)
            where TUserInfo : UserLightInfo, new()
        {
            using (var db = this.GetDB())
            {
                var qList = from it in db.TableFollow()
                            join u in db.TableUser() on it.UId equals u.Id
                            join level in db.TableUserLevel() on u.LevelTypeId equals level.TypeId
                            where it.FId == uid && u.Valid
                            select new TUserInfo()
                            {
                                Id = u.Id,
                                NickName = u.Nickname,
                                Avatar = u.Avatar,
                                RoleName = level.Name,
                            };
                var list = qList.ToList();
                if (typeof(TUserInfo) == typeof(UserSimpleInfo))
                {
                    foreach (var item in list)
                    {
                        FillUserSimpleCount(db, item as UserSimpleInfo);
                    }
                }
                return list;
            }
        }
        public List<TUserInfo> GetFans<TUserInfo>(int uid)
            where TUserInfo : UserLightInfo, new()
        {
            using (var db = this.GetDB())
            {
                var qList = from it in db.TableFollow()
                            join u in db.TableUser() on it.FId equals u.Id
                            join level in db.TableUserLevel() on u.LevelTypeId equals level.TypeId
                            where it.UId == uid && u.Valid
                            select new TUserInfo()
                            {
                                Id = u.Id,
                                NickName = u.Nickname,
                                Avatar = u.Avatar,
                                RoleName = level.Name,
                            };
                var list = qList.ToList();
                if (typeof(TUserInfo) == typeof(UserSimpleInfo))
                {
                    foreach (var item in list)
                    {
                        FillUserSimpleCount(db, item as UserSimpleInfo);
                    }
                }
                return list;
            }
        }
        private void FillUserSimpleCount(DataConnection db, UserSimpleInfo info)
        {
            info.TopicCnt = (from t in db.TableTopic() where t.CreateUserId == info.Id && t.Status == TopicStatus.Publish select t.Id).Count();
            info.FansCnt = (from f in db.TableFollow() where f.UId == info.Id && f.Valid select 1).Count();
            info.FollowCnt = (from f in db.TableFollow() where f.FId == info.Id && f.Valid select 1).Count();
        }
    }
}
