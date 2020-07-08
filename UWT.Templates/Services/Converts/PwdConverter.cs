using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UWT.Templates.Services.Converts
{
    /// <summary>
    /// 密码转换器
    /// </summary>
    public class PwdConverter
    {
        /// <summary>
        /// 创建MD5密码
        /// </summary>
        /// <param name="pwd">密码串</param>
        /// <returns>返回加密后的串</returns>
        public static string BuildMD5(string pwd)
        {
            return Build(pwd, MD5.Create());
        }

        /// <summary>
        /// 创建SHA1密码
        /// </summary>
        /// <param name="pwd">密码串</param>
        /// <returns>返回加密后的串</returns>
        public static string BuildSHA1(string pwd)
        {
            return Build(pwd, SHA1.Create());
        }

        /// <summary>
        /// 创建SHA256密码
        /// </summary>
        /// <param name="pwd">密码串</param>
        /// <returns>返回加密后的串</returns>
        public static string BuildSHA256(string pwd)
        {
            return Build(pwd, SHA256.Create());
        }

        private static string Build<TCreator>(string pwd, TCreator creator)
            where TCreator : System.Security.Cryptography.HashAlgorithm
        {
            byte[] pwdBuf = Encoding.UTF8.GetBytes(pwd);
            byte[] hashBuf = creator.ComputeHash(pwdBuf);
            var hasPwd = BitConverter.ToString(hashBuf).Replace("-", "");
            return $"{creator.GetType().BaseType.Name.ToLower()}({hasPwd})";
        }
    }
    /// <summary>
    /// 密码保存编码方式
    /// </summary>
    public enum PwdEncoder
    {
        /// <summary>
        /// 原始不编码
        /// </summary>
        None,
        /// <summary>
        /// MD5编码
        /// </summary>
        MD5,
        /// <summary>
        /// SHA1编码
        /// </summary>
        SHA1,
        /// <summary>
        /// SHA256编码
        /// </summary>
        SHA256
    }
}
