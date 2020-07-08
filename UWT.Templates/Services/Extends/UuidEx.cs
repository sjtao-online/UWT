using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 全局唯一值相关扩展
    /// </summary>
    public static class UuidEx
    {
        /// <summary>
        /// 转为压缩字符串
        /// 22长度
        /// </summary>
        /// <returns></returns>
        public static string ToStringZ(this Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray()).Substring(0, 22);
        }
        /// <summary>
        /// 转为压缩字符串
        /// 26长度(不带特殊字符)
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToStringZ2(this Guid guid)
        {
            return ConvertTo32(guid.ToByteArray());
        }


        /// <summary>
        /// 转为压缩字符串
        /// 11长度
        /// </summary>
        /// <param name="uwtid"></param>
        /// <returns></returns>
        public static string ToStringZ(this Uwtid uwtid)
        {
            return Convert.ToBase64String(uwtid.ToByteArray()).Substring(0, 11);
        }
        /// <summary>
        /// 转为压缩字符串
        /// 13长度(不带特殊字符)
        /// </summary>
        /// <param name="uwtid"></param>
        /// <returns></returns>
        public static string ToStringZ2(this Uwtid uwtid)
        {
            return ConvertTo32(uwtid.ToByteArray());
        }
        private static string ConvertTo32(byte[] bufs)
        {
            const string Keys1 = "rstu01mn2kpq9ab4def3hjvw56xy78cz";
            const int keylen = 5;
            int last = 0;
            int lastvalue = 0;
            var bits = bufs.Length * 8;
            var size = bits / keylen;
            if (bits % keylen != 0)
            {
                size += 1;
            }
            char[] r = new char[bufs.Length * 8 / keylen + 1];
            var index = 0;
            foreach (var item in bufs)
            {
                int s = item >> (3 + last);
                int s2 = lastvalue << (keylen - last);
                int value = s2 + s;
                r[index] = Keys1[value];
                var offset = 8 - (3 + last);
                var sxs = (byte)(item << offset);
                lastvalue = sxs >> offset;
                last = 8 + last - keylen;
                index++;
                if (last >= keylen)
                {
                    offset = last - keylen;
                    int s3 = lastvalue >> offset;
                    r[index] = Keys1[s3];
                    if (offset == 0)
                    {
                        last = 0;
                        lastvalue = 0;
                    }
                    else
                    {
                        last = offset;
                        sxs = (byte)(lastvalue << (8 - offset));
                        lastvalue = sxs >> (8 - offset);
                    }
                    index++;
                }
            }
            if (last > 0)
            {
                r[index] = Keys1[lastvalue];
            }
            return new string(r);
        }
    }
}
