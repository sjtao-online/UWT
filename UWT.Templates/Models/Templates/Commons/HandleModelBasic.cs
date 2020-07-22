using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace UWT.Templates.Models.Templates.Commons
{
    /// <summary>
    /// 行内操作模型
    /// </summary>
    public class HandleModelBasic : ViewModelBasic
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
        #endregion
        /// <summary>
        /// 操作类型
        /// </summary>
        public HandleType Type { get; internal set; }
        /// <summary>
        /// 按钮标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 询问内容
        /// </summary>
        public string AskContent { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }

        private static HandleModelBasic Build(string title, string target, string askContent, string tooltip, HandleType type)
        {
            return new HandleModelBasic()
            {
                Title = title,
                AskContent = askContent,
                Target = target,
                Tooltip = tooltip,
                Type = type
            };
        }
        /// <summary>
        /// 构建下载
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">URL</param>
        /// <param name="savefilename">默认保存文件名</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModelBasic BuildDownload(string title, string url, string savefilename = null, string askContent = null, string tooltip = null)
        {
            string target = JsonSerializer.Serialize(new Dictionary<string, string>()
            {
                ["url"] = url,
                ["filename"] = savefilename,
            });
            return Build(title, target, askContent, tooltip, HandleType.Download);
        }

        /// <summary>
        /// 构建链接
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">URL</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModelBasic BuildNavigate(string title, string url, string askContent = null, string tooltip = null)
        {
            return Build(title, url, askContent, tooltip, HandleType.Navigate);
        }
        /// <summary>
        /// 构建API-POST
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">URL</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModelBasic BuildApiPost(string title, string url, string askContent = null, string tooltip = null)
        {
            return Build(title, url, askContent, tooltip, HandleType.ApiPost);
        }

        /// <summary>
        /// 构建“删除”
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static HandleModelBasic BuildDel(string url)
        {
            return BuildApiPost("删除", url, TipDel, "删除当前条目");
        }

        /// <summary>
        /// 构建“发布”
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static HandleModelBasic BuildPublish(string url)
        {
            return BuildApiPost("发布", url, TipPublish, "发布当前条目");
        }

        /// <summary>
        /// 构建“撤下”
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static HandleModelBasic BuildPublishRemove(string url)
        {
            return BuildApiPost("撤下", url, TipPublishRemove, "撤下当前条目");
        }

        /// <summary>
        /// 构建执行JS
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="jscontent">js内容</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModelBasic BuildEvalJS(string title, string jscontent, string askContent = null, string tooltip = null)
        {
            return Build(title, jscontent, askContent, tooltip, HandleType.EvalJS);
        }

        /// <summary>
        /// 构建多层询问结构
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="list">操作结构列表</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModelBasic BuildComfirm(string title, List<HandleModelBasic> list, string askContent = null, string tooltip = null)
        {
            return Build(title, JsonSerializer.Serialize(list), askContent, tooltip, HandleType.Comfirm);
        }
        /// <summary>
        /// 构建对话框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">对话框URL</param>
        /// <param name="width">对话框宽[css]</param>
        /// <param name="height">对话框高[css]</param>
        /// <returns></returns>
        public static HandleModelBasic BuildPopupDlg(string title, string url, string width, string height)
        {
            string target = JsonSerializer.Serialize(new Dictionary<string, string>()
            {
                ["url"] = url,
                ["width"] = width,
                ["height"] = height
            });
            return Build(title, target, null, null, HandleType.PopupDlg);
        }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum HandleType
    {
        /// <summary>
        /// BUTTON标签,用于执行一段JS动作
        /// </summary>
        EvalJS,
        /// <summary>
        /// BUTTON标签,用于弹出对话框
        /// </summary>
        PopupDlg,
        /// <summary>
        /// A标签,用于直接下载
        /// </summary>
        Download,
        /// <summary>
        /// A标签,用于直接跳转URL
        /// </summary>
        Navigate,
        /// <summary>
        /// BUTTON标签,用于执行一个GET操作
        /// </summary>
        ApiGet,
        /// <summary>
        /// BUTTON标签,用于执行一个POST操作
        /// </summary>
        ApiPost,
        /// <summary>
        /// BUTTON标签,用于执行弹窗确认
        /// </summary>
        Comfirm
    }
}
