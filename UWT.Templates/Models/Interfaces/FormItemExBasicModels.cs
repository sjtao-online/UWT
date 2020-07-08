using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Basics;
using static UWT.Templates.Attributes.Forms.FormItems;

namespace UWT.Templates.Models.Interfaces
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    /// <summary>
    /// 扩展基类
    /// </summary>
    public interface IFormItemExBasicModel
    {

    }
    /// <summary>
    /// 文本
    /// </summary>
    public interface IFormTextEx : IFormItemExBasicModel
    {
        int MaxLength { get; }
        int MinLength { get; }
        FormItems.TextAttribute.Cate TextCate { get; }
        string Regex { get; }
    }
    public interface ICanMaxMinEx<TType> : IFormItemExBasicModel
    {
        TType Max { get; }
        TType Min { get; }
    }
    public interface ICanRangeMaxMinEx<TType> : ICanMaxMinEx<TType>
    {
        bool IsRange { get; }
    }
    public interface IMoneyEx : ICanRangeMaxMinEx<int>
    {
        int DigitCnt { get; }
    }
    public interface ISliderEx : IFormItemExBasicModel
    {
        string Class { get; }
        uint Max { get; }
        uint Min { get; }
        uint Block { get; }
        bool IsRange { get; }
    }
    public interface IFormListModel : IFormItemExBasicModel
    {
        FormItems.ListAttribute.FormListFlag Flags { get; }
    }
    public interface IShowModel : IFormItemExBasicModel
    {
        string ShowDefault { get; }
    }
    public interface IPwdEx : IFormItemExBasicModel
    {
        int MaxLength { get; }
        int MinLength { get; }
        bool HasConfirm { get; }
        string Regex { get; }
    }
    public interface IFileEx : IFormItemExBasicModel
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        string FileType { get; }
        /// <summary>
        /// 是否可以选择以前的
        /// </summary>
        bool CanSelectReadyAll { get; }
        /// <summary>
        /// 是否可以搜索
        /// </summary>
        bool CanFilter { get; }
        /// <summary>
        /// 是否可以引用其它
        /// </summary>
        bool CanLinkOther { get; }
        /// <summary>
        /// 最大尺寸
        /// </summary>
        long MaxSize { get; }
        /// <summary>
        /// 自定义图标
        /// </summary>
        string CustomIcon { get; }
    }
    public interface ISimpleSelectEx : IFormItemExBasicModel
    {
        int? DefaultSelected { get; }
        ISelectItemBuilder GetCurrentBuilder(RazorPage razor, Type modelType);
    }
    public interface IDisplayGroupEx : ISimpleSelectEx
    {
        string GroupName { get; }
    }
    public interface IMultiSelectEx : IFormItemExBasicModel
    {
        HashSet<string> DefaultSelected { get; }
        int MaxSelectCount { get; }
        MultiSelectAttribute.StyleTypeValues StyleType { get; }
        ISelectItemBuilder GetCurrentBuilder(RazorPage razor, Type modelType);
    }
    public interface IChooseIdEx : IFormItemExBasicModel
    {
        /// <summary>
        /// 获得接口
        /// </summary>
        string ApiUrl { get; }
        /// <summary>
        /// 用于确定Id换名称的Key
        /// </summary>
        string ChooseKey { get; }
        /// <summary>
        /// 是否多选
        /// </summary>
        bool MultiSelect { get; }
    }
    public interface IChoosenIdFromTableEx : IFormItemExBasicModel
    {
        string TableName { get; }
        string IdColumnName { get; }
        string NameColumnName { get; }
        string ParentIdColumnName { get; }
        string Where { get; }
        string RKey { get; }
        bool MultiSelect { get; }
    }
    public interface ISwitchEx : IFormItemExBasicModel
    {
        bool DefaultValue { get; }
        string SwitchText { get; }
    }
    /// <summary>
    /// 表单支持的操作
    /// </summary>
    public interface IFormHandlerModel : IViewModelBasic
    {
        /// <summary>
        /// 按钮标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 附加操作标识
        /// </summary>
        string Handler { get; }
        /// <summary>
        /// JS回调代码
        /// </summary>
        string JSCallback { get; }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
