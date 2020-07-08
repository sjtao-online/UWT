using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 控制器基础接口
    /// </summary>
    public interface ITemplateController
    {

    }
    /// <summary>
    /// 操作模型接口
    /// </summary>
    public interface IHandleModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        string Target { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 确认提示
        /// </summary>
        string AskTooltip { get; set; }
        /// <summary>
        /// 构建多筛选确认功能目标
        /// </summary>
        /// <param name="hrefs">多操作模型接口</param>
        /// <param name="tipContent">提示文本</param>
        void BuildMultiComfirmTarget(List<Templates.Commons.ChildrenHandleModel> hrefs, string tipContent);
    }
}
