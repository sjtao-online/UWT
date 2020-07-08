using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Filters
{
    /// <summary>
    /// 筛选值类型
    /// </summary>
    public enum FilterValueType
    {
        /// <summary>
        /// 浮点数值
        /// </summary>
        FloatNumber,
        /// <summary>
        /// 整数数值
        /// </summary>
        IntegerNumber,
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 金额
        /// </summary>
        Money,
        /// <summary>
        /// 可多级联动的单选框(未实现)
        /// </summary>
        IdComboBox,
        /// <summary>
        /// 多选
        /// </summary>
        TagMSelector,
        /// <summary>
        /// 单选
        /// </summary>
        TagSSelector
    }
}
