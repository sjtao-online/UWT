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
    class UserService : BBSService
    {
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        public UserSimpleInfo Find(int uid)
        {
            return Find<UserSimpleInfo>(uid, null);
        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <typeparam name="TUserInfo"></typeparam>
        /// <param name="uid">用户Id</param>
        /// <param name="dbCall">回调</param>
        /// <returns></returns>
        public TUserInfo Find<TUserInfo>(int uid, Action<DataConnection, TUserInfo> dbCall)
            where TUserInfo : UserLightInfo, new()
        {
            var qinfo = (from u in DataConnection.TableUser()
                        join level in DataConnection.TableUserLevel() on u.LevelTypeId equals level.TypeId
                        where u.Valid && u.Id == uid
                        select new TUserInfo()
                        {
                            Id = u.Id,
                            NickName = u.Nickname,
                            Avatar = u.Avatar,
                            LevelName = level.Name,
                        }).Take(1);
            if (qinfo.Count() == 0)
            {
                return null;
            }
            var info = qinfo.First();
            if (info is UserSimpleInfo)
            {
                FillUserSimpleCount(info as UserSimpleInfo);
            }
            if (dbCall != null)
            {
                dbCall(DataConnection, info);
            }
            return info;
        }

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <returns></returns>
        public List<UserPropertiesModel> GetProperties(int uid)
        {
            var qprop = from it in DataConnection.TableUserPropGroup()
                        select new UserPropertiesModel()
                        {
                            GroupName = it.Name,
                            TitleMap = (from c in DataConnection.TableUserPropConfig() where c.GId == it.Id select new { c.Id, c.Name }).ToDictionary(m => m.Id, m => m.Name),
                            Children = (from p in DataConnection.TableUserProperty() join c in DataConnection.TableUserPropConfig() on p.PId equals c.Id where c.GId == it.Id select new TitleIdModel() { Id = p.Id, Title = p.Value }).ToList()
                        };
            return qprop.ToList();
        }

        public List<UserSimpleInfo> List(List<int> userList)
        {
            var list = new List<UserSimpleInfo>();
            foreach (var uid in userList)
            {
                list.Add(Find(uid));
            }
            return list;
        }

        public void GetPropertyConfig()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得关注列表(分页)
        /// </summary>
        /// <typeparam name="TUserInfo"></typeparam>
        /// <param name="uid">用户Id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<TUserInfo> GetFollows<TUserInfo>(int uid, int pageIndex = 0, int pageSize = 0)
            where TUserInfo : UserLightInfo, new()
        {
            FillPageSelector(ref pageIndex, ref pageSize);
            var qList = QueryFollow().Where(m => m.FId == uid).Select(m => m.CopyLightInfo<TUserInfo>()); ;
            lastPageCount = qList.Count();
            var list = qList.UwtQueryPageSelector(pageIndex, pageSize).ToList();
            if (typeof(TUserInfo) == typeof(UserSimpleInfo))
            {
                foreach (var item in list)
                {
                    FillUserSimpleCount(item as UserSimpleInfo);
                }
            }
            return list;
        }
        /// <summary>
        /// 获得粉丝列表(分页)
        /// </summary>
        /// <typeparam name="TUserInfo"></typeparam>
        /// <param name="uid">用户Id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<TUserInfo> GetFans<TUserInfo>(int uid, int pageIndex = 0, int pageSize = 0)
            where TUserInfo : UserLightInfo, new()
        {
            FillPageSelector(ref pageIndex, ref pageSize);
            var qList = QueryFollow().Where(m=>m.UId == uid).Select(m=>m.CopyLightInfo<TUserInfo>());
            lastPageCount = qList.Count();
            var list = qList.UwtQueryPageSelector(pageIndex, pageSize).ToList();
            if (typeof(TUserInfo) == typeof(UserSimpleInfo))
            {
                foreach (var item in list)
                {
                    FillUserSimpleCount(item as UserSimpleInfo);
                }
            }
            return list;
        }

        public IQueryable<NewUserLightInfo> QueryFollow()
            => from it in DataConnection.TableFollow()
               join u in DataConnection.TableUser() on it.FId equals u.Id
               join level in DataConnection.TableUserLevel() on u.LevelTypeId equals level.TypeId
               where u.Valid
               select new NewUserLightInfo()
               {
                   Id = u.Id,
                   NickName = u.Nickname,
                   Avatar = u.Avatar,
                   LevelName = level.Name,
                   UId = it.UId,
                   FId = it.FId
               };

        public int GetDefaultPageSize() => defaultPageSize;

        public int GetLastPageCount() => lastPageCount;

        static int defaultPageSize = 30;
        int lastPageCount = 0;

        private void FillPageSelector(ref int pageIndex, ref int pageSelector)
        {
            if (pageSelector == 0)
            {
                pageSelector = defaultPageSize;
                if (BBSEx.BbsConfigModel.PageConfig != null && BBSEx.BbsConfigModel.PageConfig.Default != null)
                {
                    pageSelector = defaultPageSize = BBSEx.BbsConfigModel.PageConfig.Default.PageSize;
                }
            }
        }
        private void FillUserSimpleCount(UserSimpleInfo info)
        {
            info.TopicCnt = (from t in DataConnection.TableTopic() where t.CreateUserId == info.Id && t.Status == TopicStatus.Publish select t.Id).Count();
            info.FansCnt = (from f in DataConnection.TableFollow() where f.UId == info.Id && f.Valid select 1).Count();
            info.FollowCnt = (from f in DataConnection.TableFollow() where f.FId == info.Id && f.Valid select 1).Count();
        }
    }
    class NewUserLightInfo : UserLightInfo
    {
        public int FId { get; set; }
        public int UId { get; set; }
        public TUserInfo CopyLightInfo<TUserInfo>()
            where TUserInfo : UserLightInfo, new()
        {
            return new TUserInfo()
            {
                Avatar = Avatar,
                Id = Id,
                NickName = NickName,
                LevelName = LevelName
            };
        }
    }
}
