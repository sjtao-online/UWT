using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UWT.Templates.Models.Basics
{
    /// <summary>
    /// 通用错误码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 未定义错误
        /// 请查看Msg
        /// </summary>
        UnkownError_SeeMsg = -1,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,


        #region 登录与权限相关 1-30
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        Login_UserPwdError,
        /// <summary>
        /// 登录失败(通用错误)
        /// </summary>
        [Description("登录失败")]
        Login_Failed,
        /// <summary>
        /// 用户名或验证码错误
        /// </summary>
        [Description("用户名或验证码错误")]
        Login_UserVCodeError,
        /// <summary>
        /// 登录已过期
        /// </summary>
        [Description("登录已过期")]
        Login_SessionExp,
        /// <summary>
        /// 账号已冻结
        /// </summary>
        [Description("账号已冻结")]
        Login_UserDisabled,
        /// <summary>
        /// 账号已存在
        /// </summary>
        [Description("账号已存在")]
        Register_AccountExist,
        /// <summary>
        /// 手机号已存在
        /// </summary>
        [Description("手机号已存在")]
        Register_PhoneExist,
        /// <summary>
        /// 邮箱已存在
        /// </summary>
        [Description("邮箱已存在")]
        Register_MailExist,
        /// <summary>
        /// 账号不存在
        /// </summary>
        [Description("账号不存在")]
        Normal_AccountNoExist,
        /// <summary>
        /// 没权限
        /// </summary>
        [Description("没权限")]
        NotAuthorized,
        #endregion


        #region 常规错误 31-100
        #region 表单验证相关
        /// <summary>
        /// 表示数据检验错误
        /// </summary>
        [Description("表单数据检验错误")]
        FormCheckError = 31,
        /// <summary>
        /// 手机号不合法
        /// </summary>
        [Description("手机号不合法")]
        PhoneInvalid,
        /// <summary>
        /// 邮箱不合法
        /// </summary>
        [Description("邮箱不合法")]
        EmailInvalid,
        /// <summary>
        /// 密码不合法
        /// </summary>
        [Description("密码不合法")]
        PwdInvalid,
        #endregion
        #region 数据库相关
        /// <summary>
        /// 数据表未找到
        /// </summary>
        [Description("数据表未找到")]
        DatatableNotFound,
        /// <summary>
        /// 数据库错误
        /// 但原因未知
        /// </summary>
        [Description("数据库错误(通用)")]
        DatatableFaled,
        #endregion
        #endregion

        #region 一般错误 101-200
        #region 数据问题
        /// <summary>
        /// 上传文件未选择文件
        /// </summary>
        [Description("上传文件未选择文件")]
        UploadNotFile = 101,
        /// <summary>
        /// 指定数据条目未找到
        /// </summary>
        [Description("指定数据条目未找到")]
        Item_NotFound,
        #endregion
        /// <summary>
        /// 验证码已过期
        /// </summary>
        [Description("验证码已过期")]
        VCode_Exp,
        /// <summary>
        /// 验证码错误次数过多，请重新发送
        /// </summary>
        [Description("验证码错误次数过多，请重新发送")]
        VCode_ErrorCountMax,
        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误")]
        VCode_Error,
        /// <summary>
        /// 参数无效
        /// </summary>
        [Description("参数无效")]
        ParamterInvalid,
        #endregion

        #region 其它错误 201-1000
        #region API接口本身相关
        /// <summary>
        /// API接口URL不存在
        /// </summary>
        [Description("无此接口")]
        API_NotFound = 404,
        /// <summary>
        /// API接口不支持指定方法
        /// </summary>
        [Description("接口不支持指定方法：HttpMethod")]
        API_MethodNotAllowed = 405,
        /// <summary>
        /// 接口不支持指定内容格式
        /// </summary>
        [Description("接口不支持指定内容格式：ContentType")]
        API_MethodNotUnsupportedMediaType = 415,
        /// <summary>
        /// 执行中法律原因不可用
        /// </summary>
        [Description("法律原因不可用")]
        API_UnavailableForLegalReasons = 451,
        /// <summary>
        /// 内部异常
        /// </summary>
        [Description("内部异常")]
        API_Carsh = 500,
        #endregion
        #endregion
    }
    class ErrorCodeEx
    {
        static Dictionary<ErrorCode, string> ErrCodeToMsgMap = null;
        static object initMapLocker = new object();
        public static string GetErrorCodeMsg(ErrorCode code)
        {
            if (ErrCodeToMsgMap == null)
            {
                lock (initMapLocker)
                {
                    if (ErrCodeToMsgMap == null)
                    {
                        ErrCodeToMsgMap = new Dictionary<ErrorCode, string>();
                        Type type = typeof(ErrorCode);
                        foreach (ErrorCode item in Enum.GetValues(type))
                        {
                            var field = type.GetField(item.ToString());
                            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                            if (attribute == null)
                            {
                                ErrCodeToMsgMap.Add(item, item.ToString());
                            }
                            else
                            {
                                ErrCodeToMsgMap.Add(item, attribute.Description);
                            }
                        }
                    }
                }
            }
            return ErrCodeToMsgMap[code];
        }
    }
}
