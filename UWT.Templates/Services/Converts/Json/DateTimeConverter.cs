using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Services.Converts.Json
{
    /// <summary>
    /// DateTime-毫秒
    /// </summary>
    public class DateTimeMillisecondsConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetInt64(out long u))
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(u).LocalDateTime;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    /// <summary>
    /// DateTime-秒
    /// </summary>
    public class DateTimeSecondsConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetInt64(out long u))
            {
                return DateTimeOffset.FromUnixTimeSeconds(u).LocalDateTime;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixTimeSeconds());
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
