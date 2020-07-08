using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Lists
{
    /// <summary>
    /// 以类作命名空间用
    /// </summary>
    public class ListItems
    {
        /// <summary>
        /// cshtml片段类型
        /// </summary>
        [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class PartCshtmlAttribute : Attribute
            , Models.Interfaces.IPartCshtmlAttribute
        {
            /// <summary>
            /// 部分布局路径
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
            /// cshtml片段类型
            /// </summary>
            public PartCshtmlAttribute(string PartPath)
            {
                this.PartPath = PartPath;
            }
        }
    }
}
