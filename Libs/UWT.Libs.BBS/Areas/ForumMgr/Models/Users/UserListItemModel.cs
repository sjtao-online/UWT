using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models.Users
{
    [ListViewModel]
    class UserListItemModel
    {
        [ListColumn("编号")]
        public int Id { get; set; }
        [ListColumn("账号名")]
        public string Account { get; set; }
        [ListColumn("昵称")]
        public string Nickname { get; set; }
        [ListColumn("加入时间")]
        public string JoinTime { get; set; }
        [ListColumn("状态")]
        public string Status { get; set; }
        [ListColumn("操作", ColumnType = ColumnType.Handle, Index = int.MaxValue)]
        public List<HandleModel> HandleList
        {
            get
            {
                List<HandleModel> handles = new List<HandleModel>();
                List<HandleModel> breaks = new List<HandleModel>();
                breaks.Add(HandleModel.BuildApiPost("昵称", ".NicknameBreak?id=" + Id, "确定修改此论坛用户昵称为“违规昵称*****”吗？", "设置为违规昵称"));
                breaks.Add(HandleModel.BuildApiPost("禁言", ".BanWords?id=" + Id, "确定禁止此用户发言吗？"));
                handles.Add(HandleModel.BuildMultiButtons("违规", breaks, "处理违规"));
                return handles;
            }
        }
    }
}
