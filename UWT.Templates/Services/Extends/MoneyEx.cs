using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 钱方面的扩展
    /// </summary>
    public static class MoneyEx
    {
        /// <summary>
        /// 转换钱开始
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="digitCnt">小数位数</param>
        /// <returns></returns>
        public static string ToMoneyBegin(this string lm, int digitCnt)
        {
            if (string.IsNullOrEmpty(lm))
            {
                return "";
            }
            int index = lm.IndexOf(',');
            return ToMoneyText(lm.Substring(0, index), digitCnt);
        }
        /// <summary>
        /// 转换钱结束
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="digitCnt">小数位数</param>
        /// <returns></returns>
        public static string ToMoneyEnd(this string lm, int digitCnt)
        {
            if (string.IsNullOrEmpty(lm))
            {
                return "";
            }
            int index = lm.IndexOf(',');
            return ToMoneyText(lm.Substring(index + 1), digitCnt);
        }

        /// <summary>
        /// 钱转换为字符串
        /// </summary>
        /// <param name="lMoney"></param>
        /// <param name="digitCnt">小数位置</param>
        /// <returns></returns>
        public static string ToMoneyText(this string lMoney, int digitCnt)
        {
            if (!string.IsNullOrEmpty(lMoney) && long.TryParse(lMoney, out long r))
            {
                return r.ToMoneyText(digitCnt);
            }
            return "";
        }
        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <param name="dbMoney"></param>
        /// <param name="digitCnt">小数位数</param>
        /// <returns></returns>
        public static string ToMoneyText(this long dbMoney, int digitCnt)
        {
            return (((double)dbMoney) / 100).ToString("0.##");
        }
    }
}
