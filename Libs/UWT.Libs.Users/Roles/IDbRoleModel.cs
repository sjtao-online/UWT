using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.Roles
{
    /// <summary>
    /// ��ɫ���ݿ�ģ��
    /// </summary>
    public interface IDbRoleTable : IDbTableBase
    {
        /// <summary>
        /// ��ɫ��
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// ��ҳ
        /// </summary>
        string HomePageUrl { get; set; }
        /// <summary>
        /// �˵���Id
        /// </summary>
        int MenuGroupId { get; set; }
        /// <summary>
        /// ��Ч
        /// </summary>
        bool Valid { get; set; }
    }
    /// <summary>
    /// ��ɫ��ģ�������
    /// </summary>
    public interface IDbRoleModuleRefTable
    {
        /// <summary>
        /// ��ɫId
        /// </summary>
        int RId { get; }
        /// <summary>
        /// ģ��Id
        /// </summary>
        int MId { get; }
    }
    /// <summary>
    /// ģ���
    /// </summary>
    public interface IDbModuleTable
    {
        /// <summary>
        /// ���
        /// </summary>
        int Id { get; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        string Name { get; }
        /// <summary>
        /// URL
        /// </summary>
        string Url { get; }
        /// <summary>
        /// ����
        /// </summary>
        string Type { get; }
    }
}