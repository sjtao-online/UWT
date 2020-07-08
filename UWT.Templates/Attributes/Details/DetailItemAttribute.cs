using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Details
{
    /// <summary>
    /// 详情条目
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DetailItemAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public DetailItemCategory Cate { get; set; }
        /// <summary>
        /// 详情条目特性
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="cate">类型</param>
        public DetailItemAttribute(string title, DetailItemCategory cate = DetailItemCategory.Default)
        {
            this.Title = title;
            this.Cate = cate;
        }
    }

    /// <summary>
    /// 详情条目分类
    /// </summary>
    public enum DetailItemCategory
    {
        /// <summary>
        /// 默认值<br/>
        /// 根据类型自动识别<br/>
        /// string, 数值, enum: Text<br/>
        /// List&lt;T&gt; : List
        /// </summary>
        Default,
        /// <summary>
        /// 简单文本
        /// </summary>
        Text,
        /// <summary>
        /// 域文本<br/>
        /// 多行
        /// </summary>
        AreaText,
        /// <summary>
        /// 富文本
        /// </summary>
        RichText,
        /// <summary>
        /// 金钱显示
        /// </summary>
        Money,
        /// <summary>
        /// 图标
        /// </summary>
        Icon,
        /// <summary>
        /// 图片<br/>
        /// 支持预览
        /// </summary>
        Image,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 音频
        /// </summary>
        Audio,
        /// <summary>
        /// 多媒体类型<br/>
        /// 根据文件类似使用最最佳显示方式
        /// </summary>
        Media,
        /// <summary>
        /// 文件<br/>
        /// 支持下载
        /// </summary>
        File,
        /// <summary>
        /// 列表
        /// </summary>
        List,
        /// <summary>
        /// 多小文本标记
        /// </summary>
        Tags,
        /// <summary>
        /// 图列表
        /// </summary>
        ImageList,
        /// <summary>
        /// 专业扩展类型<br/>
        /// 定位cshtml页面<br/>
        /// 应添加DetailItems.Cshtml
        /// </summary>
        Cshtml,
    }
}
