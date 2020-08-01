using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Templates.Lists
{
    /// <summary>
    /// 增强列表列
    /// </summary>
    public class ListColumnBasicModel
    {
        /// <summary>
        /// 标题文字
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 悬停提示文本
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 存储自定义数据<br/>
        /// 框架不会使用，扩展自用
        /// </summary>
        public object Tag { get; set; }
    }
    /// <summary>
    /// 列表超链接增强
    /// </summary>
    public class ListColumnLinkModel : ListColumnBasicModel
    {
        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否新窗口打开
        /// </summary>
        public bool IsNewTableOpen { get; set; }
    }
    /// <summary>
    /// 列表文本增强
    /// </summary>
    public class ListColumnTextModel : ListColumnBasicModel
    {
        /// <summary>
        /// 背景<br/>
        /// 建议直接使用“#AAA”这种颜色
        /// </summary>
        public string Background { get; set; }
        /// <summary>
        /// 圆角大小
        /// </summary>
        public string BorderRadius { get; set; }
        /// <summary>
        /// 文本颜色
        /// </summary>
        public string FontColor { get; set; }
        /// <summary>
        /// 内边距
        /// </summary>
        public string Padding { get; set; }
    }
}
