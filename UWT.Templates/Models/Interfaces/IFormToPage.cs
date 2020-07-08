using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Forms;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 表单控制器
    /// </summary>
    /// <typeparam name="TFormModel"></typeparam>
    public interface IFormToPage<TFormModel> : ITemplateController
        where TFormModel : class
    {

    }
    /// <summary>
    /// 表单主体
    /// </summary>
    public interface IFormModel
    {
        /// <summary>
        /// 显示标头<br/>
        /// 留null使用添加或编辑
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 接口
        /// </summary>
        string Url { get; }
        /// <summary>
        /// 方法
        /// 一般使用POST
        /// </summary>
        string Method { get; }
        /// <summary>
        /// JSON或Form
        /// </summary>
        string Type { get; }
        /// <summary>
        /// 是否标题栏显示按钮<br/>
        /// 用于字段特别多，不愿在最下方显示操作按钮的页面
        /// </summary>
        bool HandleBtnsInTitleBar { get; }
        /// <summary>
        /// 条目列表
        /// </summary>
        List<IFormItemModel> FormItems { get; }
        /// <summary>
        /// 支持的操作
        /// </summary>
        List<IFormHandlerModel> FormHandlers { get; }
        /// <summary>
        /// 返回URL
        /// </summary>
        string BackUrl { get; }
    }
    /// <summary>
    /// 生成表单
    /// </summary>
    public interface IFormViewModel
    {
        /// <summary>
        /// 编辑专用 添加一般为null
        /// </summary>
        object Item { get; }
        /// <summary>
        /// 表单主体
        /// </summary>
        IFormModel FormModel { get; }
    }
    /// <summary>
    /// 区间类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T> : ICanMaxMinEx<T>
    {
        /// <summary>
        /// 最大值
        /// </summary>
        public T Max { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public T Min { get; set; }
        /// <summary>
        /// 生成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Min.ToString() + "," + Max.ToString();
        }
    }
    /// <summary>
    /// 表单项
    /// </summary>
    public interface IFormItemModel
    {
        /// <summary>
        /// 提交属性名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }
        /// <summary>
        /// 属性
        /// </summary>
        PropertyInfo PropertyInfo { get; }
        /// <summary>
        /// 提示
        /// </summary>
        string Tooltip { get; }
        /// <summary>
        /// 是否必填
        /// </summary>
        bool IsRequired { get; }
        /// <summary>
        /// 是否行内显示
        /// </summary>
        bool IsInline { get; }
        /// <summary>
        /// 是否全宽<br/>
        /// 标题占一行，内容独占一行
        /// </summary>
        bool IsFullWidth { get; }
        /// <summary>
        /// 类型
        /// </summary>
        FormItemType ItemType { get; }
        /// <summary>
        /// 排序号
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        IFormItemExBasicModel ModelEx { get; }
        /// <summary>
        /// 组名
        /// </summary>
        string GroupName { get; }
        /// <summary>
        /// 组项名
        /// </summary>
        string GroupItemName { get; }
    }
}
