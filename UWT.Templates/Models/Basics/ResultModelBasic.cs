using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Basics
{
    /// <summary>
    /// 返回模型
    /// </summary>
    public interface IResultModelBasic
    {
        /// <summary>
        /// 错误码
        /// </summary>
        int Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        string Msg { get; set; }
    }
    /// <summary>
    /// 返回模型,有实体
    /// </summary>
    public interface IResultModelBasicT : IResultModelBasic
    {
        /// <summary>
        /// 实体
        /// </summary>
        object Data { get; set; }
    }
    class ResultModelBasic : IResultModelBasic
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }
    }
    class ResultModelBasicT : ResultModelBasic, IResultModelBasicT
    {
        /// <summary>
        /// 实体
        /// </summary>
        public object Data { get; set; }
    }
}
