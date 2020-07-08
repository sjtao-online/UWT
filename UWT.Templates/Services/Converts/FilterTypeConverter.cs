using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using UWT.Templates.Models.Filters;

namespace UWT.Templates.Services.Converts
{
    /// <summary>
    /// 筛选过滤器
    /// </summary>
    public class FilterTypeConverter : System.Text.Json.Serialization.JsonConverter<FilterType>
    {
        static Dictionary<FilterType, string> FilterType2StringMap = new Dictionary<FilterType, string>()
        {
            [FilterType.Equal] = "EQ",
            [FilterType.NotEqual] = "NE",
            [FilterType.StartWith] = "%L",
            [FilterType.EndWith] = "L%",
            [FilterType.Between] = "BT",
            [FilterType.GreaterThan] = "GT",
            [FilterType.GreaterThanOrEqual] = "GE",
            [FilterType.In] = "IN",
            [FilterType.LessThan] = "LT",
            [FilterType.LessThanOrEqual] = "LE",
            [FilterType.Like] = "%%",
        };
        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override FilterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, FilterType value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 转换FilterType为String
        /// </summary>
        /// <param name="filterType">类似</param>
        /// <returns></returns>
        public static string FilterTypeToString(FilterType filterType)
        {
            return FilterType2StringMap[filterType];
        }
    }
}
