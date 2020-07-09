using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Models.Templates.Commons
{
    /// <summary>
    /// 行内操作模型
    /// </summary>
    public class HandleModel : ViewModelBasic, Interfaces.IHandleModel, Interfaces.IViewModelBasic
    {
        #region 常量
        /// <summary>
        /// 删除提示<br/>
        /// 用于AskTooltip属性
        /// </summary>
        public const string TipDel = "是否确定要删除本条记录？";
        /// <summary>
        /// 发布提示<br/>
        /// 用于AskTooltip属性
        /// </summary>
        public const string TipPublish = "是否确定要发布本条记录？";
        /// <summary>
        /// 撤下提示<br/>
        /// 用于AskTooltip属性
        /// </summary>
        public const string TipPublishRemove = "是否确定要撤下本条记录？";
        /// <summary>
        /// 删除用<br/>
        /// 用于Class属性
        /// </summary>
        public const string ClassBtnDel = "layui-btn-danger";
        /// <summary>
        /// 消息用<br/>
        /// 用于Class属性
        /// </summary>
        public const string ClassBtnBlue = "layui-btn-info";
        /// <summary>
        /// 成功用<br/>
        /// 用于Class属性
        /// </summary>
        public const string ClassBtnSuccess = "layui-btn-success";
        /// <summary>
        /// 普通白色按钮
        /// 用于Class属性
        /// 默认值
        /// </summary>
        public const string ClassBtnDefault = "layui-btn-primary";
        /// <summary>
        /// A标签,用于直接跳转URL<br/>
        /// 用于Type属性<br/>
        /// Type默认值
        /// </summary>
        public const string TypeTagNavigate = "navigate";
        /// <summary>
        /// A标签,用于直接下载<br/>
        /// 用于Type属性
        /// </summary>
        public const string TypeTagDownload = "download";
        /// <summary>
        /// BUTTON标签,用于执行一个GET操作<br/>
        /// 用于Type属性
        /// </summary>
        public const string TypeTagApiGet = "api-get";
        /// <summary>
        /// BUTTON标签,用于执行一个POST操作<br/>
        /// 用于Type属性
        /// </summary>
        public const string TypeTagApiPost = "api-post";
        /// <summary>
        /// BUTTON标签,用于执行一段JS动作<br/>
        /// 用于Type属性
        /// </summary>
        public const string TypeTagEvalJS = "evaljs";
        /// <summary>
        /// BUTTON标签,用于执行弹窗确认<br/>
        /// </summary>
        public const string TypeTagComfirm = "comfirm";
        #endregion
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = TypeTagNavigate;
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 询问提示语
        /// </summary>
        public string AskTooltip { get; set; }
        /// <summary>
        /// 构建多筛选确认功能目标
        /// </summary>
        /// <param name="hrefs">多操作模型接口</param>
        /// <param name="tipContent">提示信息</param>
        public void BuildMultiComfirmTarget(List<ChildrenHandleModel> hrefs, string tipContent)
        {
            Build(this, hrefs, tipContent);
        }
        internal static void Build(Interfaces.IHandleModel handle, List<ChildrenHandleModel> hrefs, string tipContent)
        {
            handle.Type = TypeTagComfirm;
            handle.AskTooltip = tipContent;
            handle.Target = JsonSerializer.Serialize(hrefs);
        }
    }
    /// <summary>
    /// 子操作项
    /// </summary>
    public class ChildrenHandleModel : Interfaces.IHandleModel
    {
        /// <summary>
        /// 类型<br/>与HandleModel中类型行为一致
        /// </summary>
        public string Type { get; set; } = HandleModel.TypeTagNavigate;
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 询问提示语
        /// </summary>
        public string AskTooltip { get; set; }
        /// <summary>
        /// 构建多筛选确认功能目标
        /// </summary>
        /// <param name="hrefs">多操作模型接口</param>
        /// <param name="tipContent">提示信息</param>
        public void BuildMultiComfirmTarget(List<ChildrenHandleModel> hrefs, string tipContent)
        {
            HandleModel.Build(this, hrefs, tipContent);
        }
    }
}
