using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Details;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 支持详情的控制器需要添加
    /// </summary>
    /// <typeparam name="TDetailModel">详情模型</typeparam>
    public interface IDetailToPage<TDetailModel> : ITemplateController
    {

    }
    /// <summary>
    /// 详情条目
    /// </summary>
    public interface IDetailItemModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        DetailItemCategory Cate { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        PropertyInfo PropertyInfo { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        IDetailItemExBasicModel ModelEx { get; set; }
    }
    /// <summary>
    /// 视图模型
    /// </summary>
    public interface IDetailViewModel
    {
        /// <summary>
        /// 详情对象
        /// </summary>
        object Detail { get; }
        /// <summary>
        /// 显示模型
        /// </summary>
        IDetailModel DetailModel { get; }
    }
    /// <summary>
    /// 显示模型定义
    /// </summary>
    public interface IDetailModel
    {
        /// <summary>
        /// 显示条目
        /// </summary>
        List<IDetailItemModel> ItemModels { get; }
    }
}
