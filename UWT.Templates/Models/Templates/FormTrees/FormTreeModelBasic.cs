using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UWT.Templates.Models.Templates.FormTrees
{
    /// <summary>
    /// 树型Form表单模型基类
    /// </summary>
    /// <typeparam name="TFormTreeModel"></typeparam>
    public class FormTreeModelBasic<TFormTreeModel>
        where TFormTreeModel : FormTreeModelBasic<TFormTreeModel>
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// 子列表
        /// </summary>
        [JsonPropertyName("children")]
        public List<TFormTreeModel> Children { get; set; }
    }
}
