using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UWT.Templates.Models.Filters
{
    /// <summary>
    /// 筛选方法
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterType
    {
        /// <summary>
        /// ==
        /// </summary>
        Equal,
        /// <summary>
        /// !=
        /// </summary>
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 介于两者之内
        /// </summary>
        Between,
        /// <summary>
        /// 在其中
        /// </summary>
        In,
        /// <summary>
        /// %s%
        /// </summary>
        Like,
        /// <summary>
        /// s%
        /// </summary>
        StartWith,
        /// <summary>
        /// %s
        /// </summary>
        EndWith,
    }
}
