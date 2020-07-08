using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Templates.FormTrees;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 树形表单控制器
    /// 比较少用的类型
    /// </summary>
    /// <typeparam name="TFormTreeModel">树形模型</typeparam>
    public interface IFormTreeToPage<TFormTreeModel> : ITemplateController
        where TFormTreeModel : FormTreeModelBasic<TFormTreeModel>
    {
    }
}
