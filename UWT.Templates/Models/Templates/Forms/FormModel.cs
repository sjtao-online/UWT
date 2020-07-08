using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.Forms
{
    class FormModel : IFormModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Type { get; set; }
        public bool HandleBtnsInTitleBar { get; set; }
        public List<IFormItemModel> FormItems { get; set; }
        public List<IFormHandlerModel> FormHandlers { get; set; }
        public string BackUrl { get; set; }
    }
    class CshtmlModel : IFormCshtmlEx, IDetailCshtmlEx, IListCshtmlEx
    {
        public string CshtmlPath { get; set; }
        public List<string> JsList { get; set; }
        public List<string> CssList { get; set; }
        public string CallBackFunc { get; set; }
    }
    class SliderModel : ISliderEx, ICanRangeMaxMinEx<uint>
    {
        public uint Block { get; set; }
        public uint Max { get; set; }
        public uint Min { get; set; }
        public string Class { get; set; }
        public bool IsRange { get; set; }
    }
    class FormListModel : IFormListModel
    {
        public FormItems.ListAttribute.FormListFlag Flags { get; set; }
    }
    class ShowModel : IShowModel
    {
        public string ShowDefault { get; set; }
    }
    class FormItemModel : IFormItemModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public string Tooltip { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFullWidth { get; set; }
        public bool IsInline { get; set; }
        public FormItemType ItemType { get; set; }
        public int Index { get; set; }
        public IFormItemExBasicModel ModelEx { get; set; }
        public string GroupName { get; set; }
        public string GroupItemName { get; set; }
    }
    class FormHandlerModel : ViewModelBasic, IFormHandlerModel
    {
        public string Title { get; set; }
        public string Handler { get; set; }
        public string JSCallback { get; set; }
    }
    class FormViewModel : IFormViewModel
    {
        public object Item { get; set; }
        public IFormModel FormModel { get; set; }
        public FormViewModel()
        {

        }
    }
    #region FormItemsEx
    class FormItemsFormText : IFormTextEx
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public FormItems.TextAttribute.Cate TextCate { get; set; }
        public string Regex { get; set; }
    }
    class FormItemsCanRangeMaxMinEx<TType> : ICanRangeMaxMinEx<TType>
    {
        public TType Max { get; set; }
        public TType Min { get; set; }
        public bool IsRange { get; set; }
    }
    class FormItemMoneyEx : FormItemsCanRangeMaxMinEx<int>, IMoneyEx
    {
        public int DigitCnt { get; set; }
    }
    class FormItemsCanRangeMaxMinExSlider : FormItemsCanRangeMaxMinEx<long>
    {
        public bool IsSlider { get; set; }
    }
    class FormItemsPwd : IPwdEx
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public bool HasConfirm { get; set; }
        public string Regex { get; set; }
    }
    class FormItemsFile : IFileEx
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 是否可以选择以前的
        /// </summary>
        public bool CanSelectReadyAll { get; set; }
        /// <summary>
        /// 是否可以搜索
        /// </summary>
        public bool CanFilter { get; set; }
        /// <summary>
        /// 是否可以引用其它地方的文件
        /// </summary>
        public bool CanLinkOther { get; set; }
        /// <summary>
        /// 最大尺寸
        /// </summary>
        public long MaxSize { get; set; }
        /// <summary>
        /// 自定义
        /// </summary>
        public string CustomIcon { get; set; }
    }
    class SimpleSelect : ISimpleSelectEx
    {
        public Type SimpleSelectItemsBuilder { get; set; }
        public int GenericTypePramterIndex { get; set; }
        public int? DefaultSelected { get; set; }
        public ISelectItemBuilder GetCurrentBuilder(RazorPage razor, Type modelType)
        {
            if (SimpleSelectItemsBuilder != null)
            {
                var builder = SimpleSelectItemsBuilder.Assembly.CreateInstance(SimpleSelectItemsBuilder.FullName) as dynamic;
                builder.RazorPage = razor;
                return builder;
            }
            if (GenericTypePramterIndex >= 0)
            {
                var type = modelType.GetTypeInfo().GenericTypeArguments[GenericTypePramterIndex - 1];
                var builder = type.Assembly.CreateInstance(type.FullName) as dynamic;
                builder.RazorPage = razor;
                return builder;
            }
            return null;
        }
    }

    class DislplayGroupEx : SimpleSelect, IDisplayGroupEx
    {
        public string GroupName { get; set; }
    }

    class MultiSelect : IMultiSelectEx
    {
        public HashSet<string> DefaultSelected { get; set; }
        public int MaxSelectCount { get; set; }
        public Type SimpleSelectItemsBuilder { get; set; }
        public int GenericTypePramterIndex { get; set; }
        public FormItems.MultiSelectAttribute.StyleTypeValues StyleType { get; set; }
        public ISelectItemBuilder GetCurrentBuilder(RazorPage razor, Type modelType)
        {
            if (SimpleSelectItemsBuilder != null)
            {
                var builder = SimpleSelectItemsBuilder.Assembly.CreateInstance(SimpleSelectItemsBuilder.FullName) as dynamic;
                builder.RazorPage = razor;
                return builder;
            }
            if (GenericTypePramterIndex >= 0)
            {
                var type = modelType.GetTypeInfo().GenericTypeArguments[GenericTypePramterIndex - 1];
                var builder = type.Assembly.CreateInstance(type.FullName) as dynamic;
                builder.RazorPage = razor;
                return builder;
            }
            return null;
        }
    }
    class Switch : ISwitchEx
    {
        public bool DefaultValue { get; set; }
        public string SwitchText { get; set; }
    }
    class FormItemsChooseId : IChooseIdEx
    {
        /// <summary>
        /// 获得接口
        /// </summary>
        public string ApiUrl { get; set; }
        public string ChooseKey { get; set; }
        /// <summary>
        /// 是否为多选
        /// </summary>
        public bool MultiSelect { get; set; }
    }
    class ChoosenIdFromTable : IChoosenIdFromTableEx
    {
        public string TableName { get; set; }
        public string IdColumnName { get; set; }
        public string NameColumnName { get; set; }
        public string ParentIdColumnName { get; set; }
        public string Where { get; set; }
        public string RKey { get; set; }
        public bool MultiSelect { get; set; }
    }
    #endregion
}
