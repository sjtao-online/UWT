using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Helpers.Models
{
    /// <summary>
    /// 帮助文档表模型
    /// </summary>
    public interface IDbHelperTable : IDbTableBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 富文本内容
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        string Summary { get; set; }
        /// <summary>
        /// 作者(显示用，随便写)
        /// </summary>
        string Author { get; set; }
        /// <summary>
        /// 创建者(真实)
        /// </summary>
        int CreatorId { get; set; }
        /// <summary>
        /// 更新者(真实)
        /// </summary>
        int ModifyId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        DateTime? PublishTime { get; set; }
        /// <summary>
        /// 对应的URL
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
}
