using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.MenuGroups
{
    /// <summary>
    /// 菜单组数据库模型
    /// </summary>
    public interface IDbMenuGroupTable : IDbTableBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 页面数量
        /// </summary>
        int PageCount { get; set; }
        /// <summary>
        /// 授权数量
        /// </summary>
        int AuthCount { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
    /// <summary>
    /// 菜单项数据库模型
    /// </summary>
    public interface IDbMenuGroupItemTable : IDbTableBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        string Tooltip { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        string Icon { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        int Pid { get; set; }
        /// <summary>
        /// 菜单组Id
        /// </summary>
        int GroupId { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        int Url { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
}