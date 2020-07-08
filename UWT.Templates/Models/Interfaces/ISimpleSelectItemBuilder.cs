using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// ѡ��ؼ�ģģ��
    /// </summary>
    public interface ISelectItemBuilder
    {
        /// <summary>
        /// ҳ��
        /// </summary>
        RazorPage RazorPage { get; }
        /// <summary>
        /// ����ѡ���б�
        /// </summary>
        /// <returns></returns>
        List<NameKeyModel> BuildItemList();
    }
    /// <summary>
    /// ѡ��ؼ�Ĭ��ʵ��
    /// </summary>
    public abstract class SelectItemBuilderBasic : ISelectItemBuilder
    {
        /// <summary>
        /// ҳ��
        /// </summary>
        public RazorPage RazorPage { get; set; }
        /// <summary>
        /// ����ѡ���б�
        /// </summary>
        /// <returns></returns>
        public abstract List<NameKeyModel> BuildItemList();
    }
}