using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UWT.Templates.Models.Templates.Commons
{
    /// <summary>
    /// 行内操作模型
    /// </summary>
    public class HandleModel : ViewModelBasic
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
        #region 属性
        /// <summary>
        /// 操作类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
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
        public object Target { get; internal set; }
        #endregion

        private static string HandleFormatFromId(string type, int id)
        {
            return string.Format(".{0}?id={1}", type, id);
        }

        private static HandleModel Build(string title, object target, string askContent, string tooltip, HandleType type)
        {
            return new HandleModel()
            {
                Title = title,
                AskContent = askContent,
                Target = target,
                Tooltip = tooltip,
                Type = type
            };
        }
        #region 常用构建
        /// <summary>
        /// 构建下载
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">URL</param>
        /// <param name="savefilename">默认保存文件名</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildDownload(string title, string url, string savefilename = null, string askContent = null, string tooltip = null)
        {
            return Build(title, new Dictionary<string, string>()
            {
                ["url"] = url,
                ["filename"] = savefilename,
            }, askContent, tooltip, HandleType.Download);
        }

        /// <summary>
        /// 构建链接
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">URL</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildNavigate(string title, string url, string askContent = null, string tooltip = null)
        {
            return Build(title, url, askContent, tooltip, HandleType.Navigate);
        }
        /// <summary>
        /// 构建API-POST
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="apiurl">URL</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildApiPost(string title, string apiurl, string askContent = null, string tooltip = null)
        {
            return Build(title, apiurl, askContent, tooltip, HandleType.ApiPost);
        }

        /// <summary>
        /// 构建API-GET<br/>
        /// 比较少用
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="apiurl">URL</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildApiGet(string title, string apiurl, string askContent = null, string tooltip = null)
        {
            return Build(title, apiurl, askContent, tooltip, HandleType.ApiGet);
        }

        /// <summary>
        /// 构建“删除”
        /// </summary>
        /// <param name="apiurl">URL</param>
        /// <returns></returns>
        public static HandleModel BuildDel(string apiurl)
        {
            var del = BuildApiPost("删除", apiurl, TipDel, "删除当前条目");
            del.Class = HandleModel.ClassBtnDel;
            return del;
        }

        /// <summary>
        /// 构建“删除”<br/>
        /// .Del?id=id
        /// </summary>
        /// <param name="id">当前Id</param>
        /// <returns></returns>
        public static HandleModel BuildDel(int id)
        {
            return BuildDel(HandleFormatFromId("Del", id));
        }

        /// <summary>
        /// 构建“发布”
        /// </summary>
        /// <param name="apiurl">URL</param>
        /// <returns></returns>
        public static HandleModel BuildPublish(string apiurl)
        {
            return BuildApiPost("发布", apiurl, TipPublish, "发布当前条目");
        }

        /// <summary>
        /// 构建“发布”<br/>
        /// .Publish?id=id
        /// </summary>
        /// <param name="id">当前Id</param>
        /// <returns></returns>
        public static HandleModel BuildPublish(int id)
        {
            return BuildPublish(HandleFormatFromId("Publish", id));
        }

        /// <summary>
        /// 构建“撤下”
        /// </summary>
        /// <param name="apiurl">URL</param>
        /// <returns></returns>
        public static HandleModel BuildPublishRemove(string apiurl)
        {
            return BuildApiPost("撤下", apiurl, TipPublishRemove, "撤下当前条目");
        }

        /// <summary>
        /// 构建“撤下”<br/>
        /// .PublishRemove?id=id
        /// </summary>
        /// <param name="id">当前Id</param>
        /// <returns></returns>
        public static HandleModel BuildPublishRemove(int id)
        {
            return BuildPublishRemove(HandleFormatFromId("PublishRemove", id));
        }

        /// <summary>
        /// 构建“编辑”
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static HandleModel BuildModify(string url)
        {
            return BuildNavigate("编辑", url);
        }

        /// <summary>
        /// 生成“编辑”<br/>
        /// .Modify?id=id
        /// </summary>
        /// <param name="id">当前Id</param>
        /// <returns></returns>
        public static HandleModel BuildModify(int id)
        {
            return BuildModify(HandleFormatFromId("Modify", id));
        }

        /// <summary>
        /// 生成“详情”<br/>
        /// </summary>
        /// <param name="url">当前URL</param>
        /// <returns></returns>
        public static HandleModel BuildDetail(string url)
        {
            return BuildNavigate("详情", url);
        }

        /// <summary>
        /// 生成“详情”<br/>
        /// .Detail?id=id
        /// </summary>
        /// <param name="id">当前Id</param>
        /// <returns></returns>
        public static HandleModel BuildDetail(int id)
        {
            return BuildDetail(HandleFormatFromId("Detail", id));
        }

        /// <summary>
        /// 构建执行JS
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="jscontent">js内容</param>
        /// <param name="askContent">询问内容</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildEvalJS(string title, string jscontent, string askContent = null, string tooltip = null)
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
        public static HandleModel BuildComfirm(string title, List<HandleModel> list, string askContent = null, string tooltip = null)
        {
            return Build(title, list, askContent, tooltip, HandleType.Comfirm);
        }
        /// <summary>
        /// 创建类似“更多”扩展按钮
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="list">按钮列表</param>
        /// <param name="tooltip">悬停提示</param>
        /// <returns></returns>
        public static HandleModel BuildMultiButtons(string title, List<HandleModel> list, string tooltip = null)
        {
            return Build(title + "<i class=\"layui-icon layui-colorpicker-trigger-i layui-icon-down\"></i>", list, null, tooltip, HandleType.MultiButtons);
        }

        /// <summary>
        /// 构建对话框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="url">对话框URL</param>
        /// <param name="width">对话框宽[css]</param>
        /// <param name="height">对话框高[css]</param>
        /// <returns></returns>
        public static HandleModel BuildPopupDlg(string title, string url, string width, string height)
        {
            return Build(title, new Dictionary<string, string>()
            {
                ["url"] = url,
                ["width"] = width,
                ["height"] = height
            }, null, null, HandleType.PopupDlg);
        }

        #endregion
    }

    /// <summary>
    /// 列表操作项
    /// </summary>
    public class ListHandleModel : HandleModel
    {
        /// <summary>
        /// 是否批量操作
        /// </summary>
        public bool IsBatch { get; set; }
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
        Comfirm,
        /// <summary>
        /// BUTTON标签,用于扩展多操作
        /// </summary>
        MultiButtons
    }
}
