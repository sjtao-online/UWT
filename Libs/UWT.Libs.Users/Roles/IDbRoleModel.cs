using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.Roles
{
    /// <summary>
    /// 角色数据库模型
    /// </summary>
    public interface IDbRoleTable : IDbTableBase
    {
        /// <summary>
        /// 角色名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 主页
        /// </summary>
        string HomePageUrl { get; set; }
        /// <summary>
        /// 菜单组Id
        /// </summary>
        int MenuGroupId { get; set; }
        /// <summary>
        /// 生效
        /// </summary>
        bool Valid { get; set; }
    }
    /// <summary>
    /// 角色与模块关联表
    /// </summary>
    public interface IDbRoleModuleRefTable
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        int RId { get; }
        /// <summary>
        /// 模块Id
        /// </summary>
        int MId { get; }
    }
    /// <summary>
    /// 模块表
    /// </summary>
    public interface IDbModuleTable
    {
        /// <summary>
        /// 编号
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 显示名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// URL
        /// </summary>
        string Url { get; }
        /// <summary>
        /// 类型
        /// </summary>
        string Type { get; }
    }
}