using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// 表单项
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class FormItemAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public FormItemType ItemType { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 是否不可为空
        /// </summary>
        public bool IsRequire { get; set; }
        /// <summary>
        /// 是否全宽<br/>
        /// 标题占一行，内容独占一行
        /// </summary>
        public bool TitleIsFullScreen { get; set; }
        /// <summary>
        /// 是否行内显示
        /// </summary>
        public bool IsInline { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 表单项
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="type">类型</param>
        public FormItemAttribute(string title, FormItemType type = FormItemType.Text)
        {
            Title = title;
            ItemType = type;
        }
        /// <summary>
        /// 表单项
        /// </summary>
        /// <param name="type">类型</param>
        public FormItemAttribute(FormItemType type)
        {
            ItemType = type;
        }
    }
    /// <summary>
    /// 表单项类型
    /// </summary>
    public enum FormItemType
    {
        /// <summary>
        /// 隐藏类型<br/>
        /// 一般用来隐式来数据<br/>
        /// 比如编辑的Id之类,只支持int,long,string,double<br/>
        /// 限制编辑使用，新增包含会被忽略
        /// </summary>
        Hidden,
        /// <summary>
        /// 文本类型<br/>
        /// 应加FormItems.Text标记<br/>
        /// 限制string
        /// </summary>
        Text,
        /// <summary>
        /// 整数数值类型<br/>
        /// 应加FormItems.Integer标记<br/>
        /// 限制int,long,uint,ulong
        /// </summary>
        Integer,
        /// <summary>
        /// 浮点数值类型<br/>
        /// 应加FormItems.Float标记<br/>
        /// 限制double,float
        /// </summary>
        Float,
        /// <summary>
        /// 钱类型<br/>
        /// 应加FormItems.Money标识<br/>
        /// 限制int,int?
        /// </summary>
        Money,
        /// <summary>
        /// 日期类型<br/>
        /// 应加FormItems.DateTime标识<br/>
        /// 限制DateTime
        /// </summary>
        Date,
        /// <summary>
        /// 日期时间类型<br/>
        /// 应加FormItems.DateTime标识<br/>
        /// 限制DateTime
        /// </summary>
        DateTime,
        /// <summary>
        /// 时长类型<br/>
        /// 应加FormItems.TimeSpan<br/>
        /// 限制TimeSpan
        /// </summary>
        TimeSpan,
        /// <summary>
        /// 密码类型<br/>
        /// 应加FormItems.Pwd<br/>
        /// 限制string
        /// </summary>
        Password,
        /// <summary>
        /// 文件类型<br/>
        /// 应加应FormItems.File<br/>
        /// 限制string
        /// </summary>
        File,
        /// <summary>
        /// 选择其它项目Id<br/>
        /// 应加FormItems.ChooseId或FormItems.ChooseIdFromTable<br/>
        /// 限制int,List&lt;int&gt;
        /// </summary>
        ChooseId,
        /// <summary>
        /// 颜色类型<br/>
        /// 应加FormItems.Color<br/>
        /// 限制类型string
        /// </summary>
        Color,
        /// <summary>
        /// 时间类型<br/>
        /// 应加FormItems.Time<br/>
        /// 限制类型int
        /// </summary>
        Time,
        /// <summary>
        /// 单项选择<br/>
        /// 应加FormItems.SimpleSelect<br/>
        /// 限制string
        /// </summary>
        SimpleSelect,
        /// <summary>
        /// 多项选择<br/>
        /// 应加FormItems.MultiSelect<br/>
        /// 限制List&lt;string&gt;
        /// </summary>
        MultiSelect,
        /// <summary>
        /// 开关<br/>
        /// 应添加FormItems.Switch<br/>
        /// 限制bool
        /// </summary>
        Switch,
        /// <summary>
        /// 数值类型<br/>
        /// 滑杆控制<br/>
        /// 应添加FormItems.Slider<br/>
        /// 限制int,double,float
        /// </summary>
        Slider,
        /// <summary>
        /// 列表<br/>
        /// 列表控制<br/>
        /// 应添加FormItems.List<br/>
        /// List泛型内可使用int,string,class的类<br/>
        /// class类要有FormListItemModel  未完成
        /// </summary>
        List,
        /// <summary>
        /// 附加CSHTML页面<br/>
        /// 用于实现未定义的高级功能<br/>
        /// 应添加FormItems.PartCshtml<br/>
        /// 任意类型，编辑会做为Model传到子页面中
        /// </summary>
        PartCshtml,
        /// <summary>
        /// 用于显示图片<br/>
        /// 用于编辑页面<br/>
        /// 不参与传值只显示<br/>
        /// 可添加FormItems.Show
        /// </summary>
        ShowImage,
        /// <summary>
        /// 显示简单文本<br/>
        /// 用于编辑页面<br/>
        /// 不参与传值只显示<br/>
        /// 可添加FormItems.Show
        /// </summary>
        ShowText,
        /// <summary>
        /// 选择显示组<br/>
        /// 用于编辑页面互斥元素显示用<br/>
        /// 应添加FormItems.DisplayGroup<br/>
        /// 限制string
        /// </summary>
        DisplayGroup
    }
}
