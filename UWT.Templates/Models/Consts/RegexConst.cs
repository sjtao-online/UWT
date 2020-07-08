using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Consts
{
    /// <summary>
    /// 正则常量
    /// </summary>
    public class RegexConst
    {
        /// <summary>
        /// 手机号 (宽容模式)
        /// </summary>
        public const string MobilePhone0 = @"^1\d{10}$";
        /// <summary>
        /// 手机号 (普通模式)
        /// </summary>
        public const string MobilePhone = @"^1[3456789]\d{9}$";
        /// <summary>
        /// 标识符<br/>
        /// 一般可用于用户名(最长30)
        /// </summary>
        public const string Identifier = @"[_a-zA-Z][_a-zA-Z0-9]{0,30}";
        /// <summary>
        /// 标识符<br/>
        /// 支持中文(最长30)
        /// </summary>
        public const string IdentifierCN = @"[_a-zA-Z\u4e00-\u9fa5][_a-zA-Z0-9\u4e00-\u9fa5]{0,30}";
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        /// <summary>
        /// 18位身份证号
        /// </summary>
        public const string IDCardNo = @"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$";
        /// <summary>
        /// 密码 (宽松模式)<br/>
        /// 6-20位
        /// </summary>
        public const string Password0 = @"^\w{6,20}$";
        /// <summary>
        /// 密码 (普通模式)<br/>
        /// 6-20位
        /// </summary>
        public const string Password = "^[_a-zA-Z0-9]{6,20}$";
        /// <summary>
        /// 密码 (严谨模式)<br/>
        /// 6-20位
        /// </summary>
        public const string Password2 = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$";
        /// <summary>
        /// 版本号<br/>
        /// *.*[.*.*]
        /// </summary>
        public const string Version = @"^([1-9]\d*|0)(.([1-9]\d*|0)){1,3}$";
        /// <summary>
        /// 版本号<br/>
        /// 可以有英文后缀
        /// </summary>
        public const string Version2 = @"^([1-9]\d*|0)(\.([1-9]\d*|0)){1,3}[a-zA-Z-_\d ]*$";
    }
}
