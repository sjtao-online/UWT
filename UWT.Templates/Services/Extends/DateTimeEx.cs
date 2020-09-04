using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 日期时间扩展
    /// </summary>
    public static class DateTimeEx
    {
        #region 转换为时间戳
        /// <summary>
        /// 转换为时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return ToUnixTimeMilliseconds(dt.Value);
        }
        /// <summary>
        /// 转换为时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime dt)
        {
            return new DateTimeOffset(dt).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 转换为时间戳(秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return ToUnixTimeSeconds(dt.Value);
        }

        /// <summary>
        /// 转换为时间戳(秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime dt)
        {
            return new DateTimeOffset(dt).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 转换为时间戳(秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return dt.Value.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 转换为时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTimeOffset? dt)
        {
            if (dt == null)
            {
                return 0;
            }
            return dt.Value.ToUnixTimeMilliseconds();
        }
        #endregion

        #region 转换为对象
        /// <summary>
        /// 秒值转DateTimeOffset
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffsetFromSecond(this long dt)
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromSeconds(dt));
        }
        /// <summary>
        /// 毫秒值转DateTimeOffset
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffsetFromMS(this long dt)
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromMilliseconds(dt));
        }
        #endregion

        #region 转换字符串
        const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        const string DefaultDateTimeFormatDate = "yyyy-MM-dd";
        const string DefaultDateTimeNull = "-";
        /// <summary>
        /// 转换为显示用字符串(日期型)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowDateText(this DateTime dt)
        {
            return dt.ToString(DefaultDateTimeFormatDate);
        }
        /// <summary>
        /// 转换为显示用字符串(日期型)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowDateText(this DateTime? dt)
        {
            if (dt == null)
            {
                return DefaultDateTimeNull;
            }
            return ToShowDateText(dt.Value);
        }
        /// <summary>
        /// 转换为显示用字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowText(this DateTime dt)
        {
            if (dt <= NewMinDateTimeValue)
            {
                return DefaultDateTimeNull;
            }
            return dt.ToString(DefaultDateTimeFormat);
        }
        static DateTime NewMinDateTimeValue = new DateTime(1970, 1, 1, 8, 0, 0);
        /// <summary>
        /// 转换为显示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowText(this DateTime? dt)
        {
            if (dt == null)
            {
                return DefaultDateTimeNull;
            }
            return ToShowText(dt.Value);
        }
        /// <summary>
        /// 转换为显示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowText(this DateTimeOffset? dt)
        {
            if (dt == null)
            {
                return DefaultDateTimeNull;
            }
            return ToShowText(dt.Value);
        }
        /// <summary>
        /// 转换为显示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowText(this DateTimeOffset dt)
        {
            if (dt.ToUnixTimeSeconds() <= 0)
            {
                return DefaultDateTimeNull;
            }
            return dt.ToString(DefaultDateTimeFormat);
        }
        /// <summary>
        /// 转换为显示字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToShowTextS(this long dt)
        {
            return dt.GetDateTimeOffsetFromSecond().ToString(DefaultDateTimeFormat);
        }

        /// <summary>
        /// 显示为最近时间
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToShowLatestText(this DateTime datetime)
        {
            var now = DateTime.Now;
            string date = "";
            var dt = now.Date - datetime.Date;
            if (dt > TimeSpan.FromDays(2))
            {
                if (datetime.Year != now.Year)
                {
                    return datetime.ToString(DefaultDateTimeFormatDate);
                }
                if (now.Month != datetime.Month)
                {
                    return datetime.ToString("MM-dd");
                }
                return "本月" + datetime.ToString("dd");
            }
            else if (dt == TimeSpan.FromDays(2))
            {
                date = "前天 ";
            }
            else if (DateTime.Now.Date != datetime.Date)
            {
                date = "昨天 ";
            }
            return date + datetime.ToString("HH:mm");
        }
        #endregion
    }
}
