using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 选择控件模模型
    /// </summary>
    public interface ISelectItemBuilder
    {
        /// <summary>
        /// 页面
        /// </summary>
        RazorPage RazorPage { get; }
        /// <summary>
        /// 生成选项列表
        /// </summary>
        /// <returns></returns>
        List<NameKeyModel> BuildItemList();
    }
    /// <summary>
    /// 选择控件默认实现
    /// </summary>
    public abstract class SelectItemBuilderBasic : ISelectItemBuilder
    {
        /// <summary>
        /// 页面
        /// </summary>
        public RazorPage RazorPage { get; set; }
        /// <summary>
        /// 生成选项列表
        /// </summary>
        /// <returns></returns>
        public abstract List<NameKeyModel> BuildItemList();
    }
}