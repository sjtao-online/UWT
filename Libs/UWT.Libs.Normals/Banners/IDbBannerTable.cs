using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.Normals.Banners
{
    /// <summary>
    /// Banner数据库模型
    /// </summary>
    public interface IDbBannerTable : IDbTableBase
    {
        /// <summary>
        /// 跳转目录
        /// </summary>
        string Target { get; set; }
        /// <summary>
        /// 跳转类型
        /// </summary>
        string TargetType { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        string Image { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        string Cate { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        string SubTitle { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        bool Valid { get; set; }
    }
}
