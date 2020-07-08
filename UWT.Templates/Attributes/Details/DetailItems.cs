using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Details
{
    /// <summary>
    /// 将类当空间用 - 详情
    /// </summary>
    public class DetailItems
    {
        /// <summary>
        /// Detail专业cshtml扩展
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class PartCshtmlAttribute : Attribute
            , Models.Interfaces.IPartCshtmlAttribute
        {
            /// <summary>
            /// cshtml路径<br/>
            /// 请使用/Views/xxx/a.html类似
            /// </summary>
            public string PartPath { get; set; }
            /// <summary>
            /// 附加的JS文件<br/>
            /// 应以,分隔多个文件
            /// </summary>
            public string AppendJS { get; set; } = "";
            /// <summary>
            /// 附加的CSS文件<br/>
            /// 应以,分隔多个文件
            /// </summary>
            public string AppendCSS { get; set; } = "";
            /// <summary>
            /// Detail专业cshtml扩展
            /// </summary>
            /// <param name="cshtmlPath">路径</param>
            public PartCshtmlAttribute(string cshtmlPath)
            {
                PartPath = cshtmlPath;
            }
        }
    }
}
