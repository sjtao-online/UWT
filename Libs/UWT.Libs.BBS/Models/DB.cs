using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Models
{
    /// <summary>
    /// 版块表
    /// </summary>
    public interface IDbAreaTable : IDbTableBase
    {
        string Apply { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        string Icon { get; set; }
        /// <summary>
        /// 管理者用户ID
        /// </summary>
        int MgrUserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 父版块Id
        /// </summary>
        int PId { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        string Summary { get; set; }
    }
    /// <summary>
    /// 论坛管理关系表
    /// </summary>
    public interface IDbAreaMgrRefTable
    {
        /// <summary>
        /// 版块Id
        /// </summary>
        int AId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        string Auths { get; set; }
        /// <summary>
        /// ID 没什么用
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        int UId { get; set; }
    }
    /// <summary>
    /// 主题版块关系表
    /// </summary>
    public interface IDbAreaTopicRefTable
    {
        /// <summary>
        /// 版块Id
        /// </summary>
        int AId { get; set; }
        /// <summary>
        /// 扩展信息
        /// 置顶、加精、热贴等
        /// </summary>
        string Ex { get; set; }
        /// <summary>
        /// Id 
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// 主题Id
        /// </summary>
        int TId { get; set; }
    }
    /// <summary>
    /// 主题表
    /// </summary>
    public interface IDbTopicTable : IDbTableBase
    {
        /// <summary>
        /// 审核备注
        /// </summary>
        string ApplyNote { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        DateTime? ApplyTime { get; set; }
        /// <summary>
        /// 题主Id
        /// </summary>
        int CreateUserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        int TouchCnt { get; set; }
    }
    /// <summary>
    /// 回帖表
    /// </summary>
    public interface IDbTopicBackTable : IDbTableBase
    {
        /// <summary>
        /// 回帖人ID
        /// </summary>
        int CreateUserId { get; set; }
        /// <summary>
        /// 序号(楼层)
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// 回应的哪个回帖 0为回复主题
        /// </summary>
        int TBId { get; set; }
        /// <summary>
        /// 哪个主题
        /// </summary>
        int TId { get; set; }
    }
    /// <summary>
    /// 回帧更改历史
    /// </summary>
    public interface IDbTopicBackHisTable : IDbTableBase
    {
        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 回帖Id
        /// </summary>
        int TBId { get; set; }
    }
    /// <summary>
    /// 主题历史表
    /// </summary>
    public interface IDbTopicHisTable : IDbTableBase
    {
        /// <summary>
        /// 内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 主题Id
        /// </summary>
        int TId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbUserTable
    {
        string Avatar { get; set; }
        uint Exp { get; set; }
        int Id { get; set; }
        DateTime JoinTime { get; set; }
        int LevelTypeId { get; set; }
        string Nickname { get; set; }
        bool Valid { get; set; }
    }
    public interface IDbUserLevelTable
    {
        string Avatar { get; set; }
        uint Exp { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        int TypeId { get; set; }
        bool Valid { get; set; }
    }
    public interface IDbUserLevelTypeTable
    {
        int Id { get; set; }
        string Name { get; set; }
        bool Valid { get; set; }
    }
}
