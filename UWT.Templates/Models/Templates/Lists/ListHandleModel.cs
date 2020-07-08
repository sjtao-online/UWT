using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.Lists
{
    /// <summary>
    /// 列表操作项
    /// </summary>
    public class ListHandleModel : HandleModel
    {
        /// <summary>
        /// 是否批量操作
        /// </summary>
        public bool IsBatch { get; set; }
    }
}
