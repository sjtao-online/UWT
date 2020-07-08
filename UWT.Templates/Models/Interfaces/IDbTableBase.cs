using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 数据表最基本的项
    /// </summary>
    public interface IDbTableBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        DateTime AddTime { get; set; }
    }
}
