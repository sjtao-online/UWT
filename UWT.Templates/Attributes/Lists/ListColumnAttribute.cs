using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Lists
{
    /// <summary>
    /// 列表列项
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ListColumnAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// 样式组
        /// </summary>
        public string Styles { get; set; }
        /// <summary>
        /// 类名，多个以空格格开
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 是否不显示此列
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public ColumnType ColumnType { get; set; }
        /// <summary>
        /// 宽度使用WPF类似方法GridLength方法<br/>
        /// 详见readme.md中说明（注1）
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// 最小宽度<br/>
        /// 默认值80
        /// </summary>
        public double MinWidth { get; set; } = 80;
        /// <summary>
        /// 默认不限制
        /// </summary>
        public double? MaxWidth { get; set; }
        /// <summary>
        /// 列值转换委托<br/>
        /// 必须为UWT.Templates.Models.Interfaces.IListColumnConverter实现类型
        /// 对ColumnType为Index、MIndex无效，这两值是自动计算的
        /// </summary>
        public Type CallbackType { get; set; }
        /// <summary>
        /// 列表列项
        /// </summary>
        /// <param name="title">列标头</param>
        public ListColumnAttribute(string title = null)
        {
            this.Title = title;
        }
    }
    /// <summary>
    /// 列类型
    /// </summary>
    public enum ColumnType
    {
        /// <summary>
        /// 文本列<br/>
        /// 支持基本类型与增强型ListColumnTextModel
        /// </summary>
        Text,
        /// <summary>
        /// 文本列
        /// 会显示缩略
        /// </summary>
        Summary,
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 操作列
        /// </summary>
        Handle,
        /// <summary>
        /// 链接文本<br/>
        /// 文本的特殊类型<br/>
        /// 支持string与增强型ListColumnLinkModel
        /// </summary>
        Link,
        /// <summary>
        /// 序号
        /// 单页自增长
        /// 占位使用，不用赋值
        /// </summary>
        Index,
        /// <summary>
        /// 序号
        /// 多页自增长
        /// 占位使用，不用赋值
        /// </summary>
        MIndex,
        /// <summary>
        /// 增强cshtml片段<br/>
        /// 需要添加ListItems.PartCshtml
        /// </summary>
        Cshtml
    }
}
