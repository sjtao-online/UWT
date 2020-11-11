using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// 将类当命名空间用
    /// </summary>
    public sealed class FormItems
    {
        /// <summary>
        /// 文本类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class TextAttribute : Attribute
        {
            /// <summary>
            /// 文本类型
            /// </summary>
            public enum Cate
            {
                /// <summary>
                /// 简单文本
                /// </summary>
                SimpleText,
                /// <summary>
                /// 多行文本
                /// </summary>
                AreaText,
                /// <summary>
                /// 富文本(Layedit)<br/>
                /// 优点：简单，易控制<br/>
                /// 缺点：功能少
                /// </summary>
                RichText,
                /// <summary>
                /// 富文本(wangEditor)<br/>
                /// 优点：功能多些
                /// </summary>
                RichText_Wang,
                /// <summary>
                /// 富文本(UEditor)<br/>
                /// 优点：功能特多<br/>
				///	暂未支持
                /// </summary>
                RichText_UEditor,
            }
            /// <summary>
            /// 最大长度
            /// </summary>
            public int MaxLength { get; set; } = -1;
            /// <summary>
            /// 最小长度
            /// </summary>
            public int MinLength { get; set; }
            /// <summary>
            /// 正则表达式
            /// </summary>
            public string Regex { get; set; }
            /// <summary>
            /// 文本类型
            /// </summary>
            public Cate TextCate { get; set; }
            /// <summary>
            /// 文本类型
            /// </summary>
            public TextAttribute()
            {

            }
        }
        /// <summary>
        /// 整数类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class IntegerAttribute : Attribute, ICanMaxMinEx<long>
        {
            /// <summary>
            /// 最大值
            /// </summary>
            public long Max { get; set; } = long.MaxValue;
            /// <summary>
            /// 大小值
            /// </summary>
            public long Min { get; set; } = long.MinValue;
            /// <summary>
            /// 整数类型
            /// </summary>
            public IntegerAttribute()
            {
            }
        }
        /// <summary>
        /// 浮点数类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class FloatAttribute : Attribute, ICanMaxMinEx<double>
        {
            /// <summary>
            /// 最大值
            /// </summary>
            public double Max { get; set; }
            /// <summary>
            /// 大小值
            /// </summary>
            public double Min { get; set; }
            /// <summary>
            /// 浮点数类型
            /// </summary>
            public FloatAttribute()
            {
            }
        }
        /// <summary>
        /// 日期时间类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class DateTimeAttribute : Attribute, ICanMaxMinEx<string>
        {
            /// <summary>
            /// 最大值
            /// </summary>
            public string Max { get; set; }
            /// <summary>
            /// 大小值
            /// </summary>
            public string Min { get; set; }
            /// <summary>
            /// 日期时间类型
            /// </summary>
            public DateTimeAttribute()
            {
            }
        }
        /// <summary>
        /// 时长类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class TimeSpanAttribute : Attribute, ICanMaxMinEx<int>
        {
            /// <summary>
            /// 最大值(秒)<br/>
            /// 默认值一天
            /// </summary>
            public int Max { get; set; } = 60 * 60 * 24;
            /// <summary>
            /// 大小值(秒)<br/>
            /// 默认值0
            /// </summary>
            public int Min { get; set; } = 0;
            /// <summary>
            /// 最小粒度，最大粒度由最小、大值确定
            /// </summary>
            public TimeSpanMinSize MinSize { get; set; } = TimeSpanMinSize.Minute;
            /// <summary>
            /// 时长类型
            /// </summary>
            public TimeSpanAttribute()
            {
            }
            /// <summary>
            /// 时长粒度
            /// </summary>
            public enum TimeSpanMinSize
            {
                /// <summary>
                /// 天
                /// </summary>
                Day,
                /// <summary>
                /// 小时
                /// </summary>
                Hour,
                /// <summary>
                /// 分钟
                /// </summary>
                Minute,
                /// <summary>
                /// 秒
                /// </summary>
                Second,
                /// <summary>
                /// 毫秒(内核代码示支持)
                /// </summary>
                MSecond,
            }
        }
        /// <summary>
        /// 钱类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class MoneyAttribute : Attribute, ICanMaxMinEx<int>
        {
            /// <summary>
            /// 最大值
            /// </summary>
            public int Max { get; set; } = int.MaxValue;
            /// <summary>
            /// 大小值
            /// </summary>
            public int Min { get; set; } = int.MinValue;
            /// <summary>
            /// 小数位数
            /// </summary>
            public int DigitCnt { get; set; } = 2;
            /// <summary>
            /// 钱类型
            /// </summary>
            public MoneyAttribute()
            {
            }
        }
        /// <summary>
        /// 密码类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class PasswordAttribute : Attribute
        {
            /// <summary>
            /// 最大长度
            /// </summary>
            public int MaxLength { get; set; } = 20;
            /// <summary>
            /// 最小长度
            /// </summary>
            public int MinLength { get; set; } = 6;
            /// <summary>
            /// 正则表达式
            /// </summary>
            public string Regex { get; set; }
            /// <summary>
            /// 是否包含确认
            /// </summary>
            public bool HasCofirm { get; set; }
            /// <summary>
            /// 密码类型
            /// </summary>
            public PasswordAttribute()
            {

            }
        }
        /// <summary>
        /// 时间类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class TimeAttribute : Attribute
        {
            /// <summary>
            /// 最大值
            /// </summary>
            public string Max { get; set; } = "23:59:59";
            /// <summary>
            /// 最小值
            /// </summary>
            public string Min { get; set; } = "0:0:0";
            /// <summary>
            /// 时间类型
            /// </summary>
            public TimeAttribute()
            {
            }
        }
        /// <summary>
        /// Id选择类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class ChooseIdAttribute : Attribute
        {
            /// <summary>
            /// 获得可选项
            /// </summary>
            public string ApiUrl { get; set; }
            /// <summary>
            /// 用于确定Id换名字的类型
            /// </summary>
            public string ChooseKey { get; set; }
            /// <summary>
            /// Id选择类型
            /// </summary>
            public ChooseIdAttribute()
            {
            }
        }
        /// <summary>
        /// 数据库Id选择类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class ChooseIdFromTableAttribute : Attribute
        {
            /// <summary>
            /// 表名
            /// </summary>
            public string TableName { get; set; }
            /// <summary>
            /// id字段名,默认id
            /// </summary>
            public string IdColumnName { get; set; } = "id";
            /// <summary>
            /// name字段名,默认name
            /// </summary>
            public string NameColumnName { get; set; } = "name";
            /// <summary>
            /// 有此字段为树
            /// </summary>
            public string ParentIdColumnName { get; set; }
            /// <summary>
            /// 筛选条件(SQL)
            /// </summary>
            public string Where { get; set; }
            /// <summary>
            /// 数据库Id选择类型
            /// </summary>
            public ChooseIdFromTableAttribute(string tableName)
            {
                TableName = tableName;
            }
        }
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        sealed class ChooseIdFromSqlAttribute : Attribute
        {
            /// <summary>
            /// SQL语句
            /// 必须有id,name
            /// </summary>
            public string SqlText { get; set; }
            public ChooseIdFromSqlAttribute()
            {
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class FileAttribute : Attribute
        {
            /// <summary>
            /// 文件类型
            /// image/png,image/*
            /// FileTypeConst
            /// </summary>
            public string FileType { get; set; }
            /// <summary>
            /// 最大尺寸
            /// 字节
            /// 0为自动限制
            /// -1为不限制
            /// </summary>
            public long MaxSize { get; set; }
            /// <summary>
            /// 是否可以选择以前的
            /// </summary>
            public bool CanSelectReadyAll { get; set; }
            /// <summary>
            /// 是否可以搜索
            /// </summary>
            public bool CanFilter { get; set; }
            /// <summary>
            /// 是否可以引用其它资源的链接
            /// </summary>
            public bool CanLinkOther { get; set; }
            /// <summary>
            /// 自定义图标<br/>
            /// 以'/'开头为图片<br/>
            /// 不写的话以FileType自行判断，判断不了的按普通文件算
            /// </summary>
            public string CustomIcon { get; set; }
            /// <summary>
            /// 上传文件
            /// </summary>
            public FileAttribute()
            {
            }
        }
        /// <summary>
        /// 单选框类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class SimpleSelectAttribute : Attribute
        {
            /// <summary>
            /// ISelectItemBuilder的实现类<br/>
            /// 一般直接继承SelectItemBuilderBasic就可以
            /// </summary>
            public Type Builder { get; set; }
            /// <summary>
            /// ISelectItemBuilder的实现类型在模板类中的序号
            /// </summary>
            /// <value></value>
            public int BuilderTemplateTypeIndex { get; set; }
            /// <summary>
            /// 默认选择项
            /// </summary>
            public int DefaultSelected { get; set; }
            /// <summary>
            /// 单选框类型
            /// </summary>
            public SimpleSelectAttribute(int defaultSelect)
            {
                DefaultSelected = defaultSelect;
            }
        }
        /// <summary>
        /// 多项选择
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class MultiSelectAttribute : Attribute
        {
            /// <summary>
            /// ISelectItemBuilder的实现类<br/>
            /// 一般直接继承SelectItemBuilderBasic就可以
            /// </summary>
            public Type Builder { get; set; }
            /// <summary>
            /// ISelectItemBuilder的实现类型在模板类中的序号
            /// </summary>
            /// <value></value>
            public int BuilderTemplateTypeIndex { get; set; }
            /// <summary>
            /// 样式类型
            /// </summary>
            public enum StyleTypeValues
            {
                /// <summary>
                /// 方块选择框
                /// </summary>
                BlackCheckBox,
                /// <summary>
                /// 标准选择框
                /// </summary>
                SimpleCheckBox,
                /// <summary>
                /// 下拉框筛选(暂未实现)
                /// </summary>
                DropdownSelector,
            }
            /// <summary>
            /// 最多选多少项
            /// </summary>
            public int MaxSelectCount { get; set; } = int.MaxValue;
            /// <summary>
            /// 显示样式类型
            /// </summary>
            public StyleTypeValues StyleType { get; set; }
            /// <summary>
            /// 默认选项性，以分号逗号或号中任一符号分隔<br/>
            /// 如：a;b;c
            /// </summary>
            public string DefaultSelected { get; set; }
            /// <summary>
            /// 多项选择
            /// </summary>
            public MultiSelectAttribute()
            {
            }
        }
        /// <summary>
        /// 开关
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class SwitchAttribute : Attribute
        {
            /// <summary>
            /// 开：文本
            /// </summary>
            public string OnText { get; set; } = "ON";
            /// <summary>
            /// 关：文本
            /// </summary>
            public string OffText { get; set; } = "OFF";
            /// <summary>
            /// 默认值
            /// </summary>
            public bool DefaultValue { get; set; }
            /// <summary>
            /// 开关
            /// </summary>
            public SwitchAttribute()
            {
            }
        }
        /// <summary>
        /// cshtml片段类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class PartCshtmlAttribute : Attribute
            , Models.Interfaces.IPartCshtmlAttribute
        {
            /// <summary>
            /// 部分布局路径
            /// </summary>
            public string PartPath { get; set; }
            /// <summary>
            /// JS提交回调
            /// </summary>
            public string CommitCallback { get; set; } = "";
            /// <summary>
            /// 附加的JS文件<br/>
            /// 应以,分隔多个文件
            /// </summary>
            public string AppendJS { get; set; } = "";
            /// <summary>
            /// 附加的CSS文件<br/>
            /// 应以,分隔多个文件
            /// </summary>
            public string AppendCSS { get; set; } = "";
            /// <summary>
            /// cshtml片段类型
            /// </summary>
            public PartCshtmlAttribute(string PartPath)
            {
                this.PartPath = PartPath;
            }
        }
        /// <summary>
        /// 显示类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class ShowAttribute : Attribute
        {
            /// <summary>
            /// 默认值
            /// </summary>
            public string Default { get; set; }
            /// <summary>
            /// 显示类型
            /// </summary>
            public ShowAttribute(string def)
            {
                this.Default = def;
            }
        }
        /// <summary>
        /// 滑杆类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class SliderAttribute : Attribute, ICanMaxMinEx<uint>
        {
            /// <summary>
            /// 最小值
            /// </summary>
            public uint Min { get; set; } = 0;
            /// <summary>
            /// 最大值
            /// </summary>
            public uint Max { get; set; } = 100;
            /// <summary>
            /// 步长
            /// </summary>
            public uint Block { get; set; } = 1;
            /// <summary>
            /// 样式
            /// </summary>
            public string Class { get; set; } = "ui-slider-green ui-slider-small";
            /// <summary>
            /// 是否显示当前数值
            /// </summary>
            public bool ShowNumber { get; set; }
            /// <summary>
            /// 滑杆类型
            /// </summary>
            public SliderAttribute()
            {
            }
        }
        /// <summary>
        /// 列表项类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class ListAttribute : Attribute
        {
            /// <summary>
            /// 列表标识
            /// </summary>
            [Flags]
            public enum FormListFlag : int
            {
                #region 编辑选项
                /// <summary>
                /// 可以调整顺序
                /// </summary>
                CanChangeOrder = 0b0000001,
                /// <summary>
                /// 可以添加
                /// </summary>
                CanAddNew = 0b0000010,
                /// <summary>
                /// 可以编辑旧项
                /// </summary>
                CanEditOld = 0b0000100,
                /// <summary>
                /// 可以删除旧项
                /// </summary>
                CanDelOld = 0b0001000,
                #endregion
                #region 显示选项
                /// <summary>
                /// 以小标签形式显示
                /// </summary>
                ShowAsTag = 0b1000001,
                /// <summary>
                /// 以列表形式显示
                /// </summary>
                ShowAsList = 0b0000000,
                #endregion
            }
            /// <summary>
            /// 选项
            /// </summary>
            public FormListFlag Flags { get; set; }
            /// <summary>
            /// 列表项类型
            /// </summary>
            public ListAttribute()
            {
            }
        }
        /// <summary>
        /// 选择显示组
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class DisplayGroupAttribute : Attribute
        {
            /// <summary>
            /// 组名称
            /// </summary>
            public string GroupName { get; set; }
            /// <summary>
            /// ISelectItemBuilder的实现类<br/>
            /// 一般直接继承SelectItemBuilderBasic就可以
            /// </summary>
            public Type Builder { get; set; }
            /// <summary>
            /// 默认选择项
            /// </summary>
            public int DefaultSelected { get; set; }
            /// <summary>
            /// ISelectItemBuilder的实现类型在模板类中的序号
            /// </summary>
            public int BuilderTemplateTypeIndex { get; set; }
            /// <summary>
            /// 选择显示组
            /// </summary>
            /// <param name="groupName">组名称，若只有一个组可以不写名称</param>
            public DisplayGroupAttribute(string groupName = null)
            {
                GroupName = groupName;
            }
        }
    }
}
