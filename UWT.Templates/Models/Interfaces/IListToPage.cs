using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Lists;
using UWT.Templates.Models.Templates.TagHelpers;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 列表页面控制器<br/>
    /// 请勿直接使用
    /// </summary>
    public interface IListToPage
    {

    }
    /// <summary>
    /// 列表页面控制器
    /// </summary>
    /// <typeparam name="TTable">数据表类型</typeparam>
    /// <typeparam name="TListItem">列表项类型</typeparam>
    public interface IListToPage<TTable, TListItem> : ITemplateController, IListToPage
        where TTable : class
        where TListItem : class
    {

    }
    /// <summary>
    /// 列表页面控制器
    /// </summary>
    /// <typeparam name="TListItem">列表项类型</typeparam>
    public interface IListToPage<TListItem> : ITemplateController
        where TListItem : class
    {

    }

    /// <summary>
    /// 列表配置
    /// </summary>
    public interface IListToPageConfig
    {
        /// <summary>
        /// 默认单页条目数
        /// 应大于1
        /// </summary>
        int DefaultPageSize { get; }
    }
    /// <summary>
    /// 分页模型接口
    /// </summary>
    public interface IToPageModel
    {
        /// <summary>
        /// 页面条目数量
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 当前页码
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 条目总数
        /// </summary>
        int ItemTotal { get; }
        /// <summary>
        /// 条目
        /// </summary>
        IList<object> Items { get; }
    }
    /// <summary>
    /// 分页模型接口
    /// </summary>
    public interface IToPageViewModel : IToPageModel
    {
        /// <summary>
        /// 列表
        /// </summary>
        IListViewModel ListViewModel { get; }
        /// <summary>
        /// 分页
        /// </summary>
        IPageSelectorModel PageSelector { get; }
        /// <summary>
        /// 自定义标题
        /// </summary>
        string CustomTitle { get; }
    }
    /// <summary>
    /// 分页模型
    /// </summary>
    public interface IPageSelectorModel
    {
        /// <summary>
        /// 当前页条目数
        /// </summary>
        int CurrentPageCount { get; }
        /// <summary>
        /// 页码基础
        /// </summary>
        string UrlBase { get; }
        /// <summary>
        /// 页面条目数量
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 当前页码
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 条目总数
        /// </summary>
        int ItemTotal { get; }
    }
    /// <summary>
    /// 列表模型接口
    /// </summary>
    public interface IListViewModel : IViewModelBasic
    {
        /// <summary>
        /// 列属性
        /// </summary>
        List<IListColumnModel> Columns { get; }
        /// <summary>
        /// 是否支持多项选择
        /// </summary>
        bool HasMutilCheck { get; }
        /// <summary>
        /// 批量传值Key
        /// </summary>
        string BatchKey { get; }
        /// <summary>
        /// 批量传值值获得方式
        /// </summary>
        PropertyInfo BatchProperty { get; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 颜色
        /// </summary>
        string Color { get; }
    }
    /// <summary>
    /// 列表项模型接口
    /// </summary>
    public interface IListColumnModel : IViewModelBasic
    {
        /// <summary>
        /// 列类型
        /// </summary>
        ColumnType ColumnType { get; }
        /// <summary>
        /// 排序号
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 是否忽略不显示
        /// </summary>
        bool IsIgnore { get; }
        /// <summary>
        /// 属性
        /// </summary>
        PropertyInfo Property { get; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        IListItemExBasicModel ModelEx { get; }
        /// <summary>
        /// 表格宽度
        /// </summary>
        ICellWidth Width { get; }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="item"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        object Convert(object item, object columnValue);
        /// <summary>
        /// 转换为显示内容
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="tagHelperTemplateModel">可以回写一般东西</param>
        /// <param name="html">Cshtml中的Html成员</param>
        /// <returns></returns>
        IHtmlContent GetRawValue(object obj, ref TagHelperTemplateModel tagHelperTemplateModel, IHtmlHelper html);
    }
    /// <summary>
    /// 表格宽度结构
    /// </summary>
    public interface ICellWidth
    {
        /// <summary>
        /// 是否为*值
        /// </summary>
        bool IsStar { get; }
        /// <summary>
        /// 是否为自动值
        /// </summary>
        bool IsAuto { get; }
        /// <summary>
        /// 是否为绝对值
        /// </summary>
        bool IsAbsolute { get; }
        /// <summary>
        /// 表示当前值
        /// </summary>
        double Value { get; }
        /// <summary>
        /// 最小值
        /// </summary>
        double MinWidth { get; }
        /// <summary>
        /// 最大值
        /// </summary>
        double? MaxWidth { get; }
    }
}
