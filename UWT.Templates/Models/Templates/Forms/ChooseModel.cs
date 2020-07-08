using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Templates.Forms
{
    /// <summary>
    /// 选择Id模型
    /// </summary>
    public class ChooseModel : NameIdModel
    {
        /// <summary>
        /// 是否可选
        /// </summary>
        public bool CanSelected { get; set; }
        /// <summary>
        /// 选中后名称
        /// </summary>
        public string SelectedName { get; set; }
        /// <summary>
        /// 树型子节点
        /// </summary>
        public List<ChooseModel> Children { get; set; }
    }
}
