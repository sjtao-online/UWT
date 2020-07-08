using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Details
{
    /// <summary>
    /// 详情模型
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DetailModelAttribute : Attribute
    {
        /// <summary>
        /// 详情模型
        /// </summary>
        public DetailModelAttribute()
        {
        }
    }
}
