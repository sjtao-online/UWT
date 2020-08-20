using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 错误码映射表接口
    /// </summary>
    public interface IErrorCodeMap
    {
        /// <summary>
        /// 枚举设置的所有错误信息<br/>
        /// Id为错误码<br/>
        /// Name为标准名，可为空<br/>
        /// Desc为错误信息
        /// </summary>
        /// <returns>返回错误列表</returns>
        List<DescNameIdModel> EnumErrorCodeMsgList();
    }
}
