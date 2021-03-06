﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Filters;
using UWT.Templates.Services.Converts;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 筛选项基础接口
    /// </summary>
    public interface IFilterBasicModel
    {
        
    }
    /// <summary>
    /// 筛选项模型
    /// </summary>
    public interface IFilterModel : IFilterBasicModel
    {
        /// <summary>
        /// 筛选器名称
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 筛选属性名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 筛选类型
        /// </summary>
        [JsonConverter(typeof(FilterTypeConverter))]
        FilterType FilterType { get; }
        /// <summary>
        /// 值类型
        /// </summary>
        FilterValueType ValueType { get; }
        /// <summary>
        /// 值
        /// </summary>
        string Value { get; }
        /// <summary>
        /// 单项选择项
        /// </summary>
        List<HasFilterTypeChildrenNameKeyModel> CanSelectList { get; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        object Tag { get; }
    }
    /// <summary>
    /// cshtml扩展筛选项
    /// </summary>
    public interface IFilterModelFromCshtml : IFilterBasicModel
    {
        /// <summary>
        /// Cshtml路径
        /// </summary>
        string ViewPath { get; set; }
        /// <summary>
        /// 搜索回调方法名
        /// </summary>
        string SearchCallback { get; set; }
        /// <summary>
        /// 重置回调方法名
        /// </summary>
        string ResetCallback { get; set; }
        /// <summary>
        /// 初始化回调方法名
        /// </summary>
        string InitCallback { get; set; }
        /// <summary>
        /// 上次的值
        /// </summary>
        string LastValue { get; set; }
        /// <summary>
        /// 扩展信息,作为Model出现
        /// </summary>
        object Tag { get; set; }
    }
    /// <summary>
    /// 筛选选择项
    /// </summary>
    public class HasFilterTypeChildrenNameKeyModel : HasChildrenNameKeyModel
    {
        /// <summary>
        /// 筛选类型<br/>
        /// 不赋值为主类型
        /// </summary>
        public FilterType? FilterType { get; set; }
    }
}
