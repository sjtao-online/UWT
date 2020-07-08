using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Users.MenuGroups
{
    /// <summary>
    /// �˵������ݿ�ģ��
    /// </summary>
    public interface IDbMenuGroupTable : IDbTableBase
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// ҳ������
        /// </summary>
        int PageCount { get; set; }
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        int AuthCount { get; set; }
        /// <summary>
        /// ��Ч��
        /// </summary>
        bool Valid { get; set; }
    }
    /// <summary>
    /// �˵������ݿ�ģ��
    /// </summary>
    public interface IDbMenuGroupItemTable : IDbTableBase
    {
        /// <summary>
        /// ����
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// ��ʾ
        /// </summary>
        string Tooltip { get; set; }
        /// <summary>
        /// ˵��
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// ͼ��
        /// </summary>
        string Icon { get; set; }
        /// <summary>
        /// ��Id
        /// </summary>
        int Pid { get; set; }
        /// <summary>
        /// �˵���Id
        /// </summary>
        int GroupId { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        int Url { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// ��Ч��
        /// </summary>
        bool Valid { get; set; }
    }
}