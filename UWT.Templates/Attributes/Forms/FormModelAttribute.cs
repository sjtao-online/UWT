using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Attributes.Forms
{
    /// <summary>
    /// Form模型特性
    /// 一般用于添加或编辑
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class FormModelAttribute : Attribute
    {
        /// <summary>
        /// APIUrl
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// HTTP Method
        /// 默认POST
        /// 一般不用改
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 提交类型
        /// 默认JSON
        /// 一般不用改
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否标题栏显示按钮<br/>
        /// 用于字段特别多，不愿在最下方显示操作按钮的页面
        /// </summary>
        public bool HandleBtnsInTitleBar { get; set; }
        /// <summary>
        /// 返回的页面，默认前一页面
        /// </summary>
        public string BackUrl { get; set; }
        /// <summary>
        /// Form模型特性
        /// </summary>
        /// <param name="url">提交URL</param>
        public FormModelAttribute(string url)
        {
            Method = "POST";
            Type = "JSON";
            Url = url;
        }

        /// <summary>
        /// Form模型特性
        /// </summary>
        /// <param name="category">Form常用类型</param>
        public FormModelAttribute(FormCategory category = FormCategory.Add)
        {
            Method = "POST";
            Type = "JSON";
            switch (category)
            {
                case FormCategory.Add:
                    Url = ".AddModel";
                    break;
                case FormCategory.Modify:
                    Url = ".ModifyModel";
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 常用Form类型
    /// </summary>
    public enum FormCategory
    {
        /// <summary>
        /// 添加 ".AddModel"
        /// </summary>
        Add,
        /// <summary>
        /// 编辑 ".ModifyModel"
        /// </summary>
        Modify
    }
}
