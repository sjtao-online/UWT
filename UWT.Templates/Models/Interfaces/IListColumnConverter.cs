using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 列值转换器接口
    /// </summary>
    public interface IListColumnConverter
    {
        /// <summary>
        /// 执行转换
        /// </summary>
        /// <param name="item">当前条目值</param>
        /// <param name="columnValue">列值</param>
        /// <returns></returns>
        object Convert(object item, object columnValue);
    }
}
