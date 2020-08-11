using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Attributes.Routes
{
    /// <summary>
    /// 不收录Module数据库<br/>
    /// 不参与权限相关的不用收录
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class UwtNoRecordModuleAttribute : Attribute, IUwtNoRecordModule
    {
        /// <summary>
        /// 不收录Module数据库<br/>
        /// 不参与权限相关的不用收录
        /// </summary>
        public UwtNoRecordModuleAttribute()
        {
        }
    }
}
