using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UWT.Templates.Services.Converts.Json
{
    /// <summary>
    /// TimeSpan转换器(默认)
    /// 序列化输出为字符串
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var v = TimeSpanConvert.Obj2TimeSpan(ref reader, options);
            if (v == null)
            {
                return new TimeSpan();
            }
            return v.Value;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            TimeSpanConvert.TimeSpanWrite(writer, value, options, TimeSpanWriteCate.String);
        }
    }

    /// <summary>
    /// TimeSpan?转换器(默认)
    /// 序列化输出为字符串
    /// </summary>
    public class TimeSpanNullConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpanConvert.Obj2TimeSpan(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            TimeSpanConvert.TimeSpanWrite(writer, value, options, TimeSpanWriteCate.String);
        }
    }
    /// <summary>
    /// TimeSpan转换器
    /// </summary>
    public class TimeSpanSConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var v = TimeSpanConvert.Obj2TimeSpan(ref reader, options);
            if (v == null)
            {
                return new TimeSpan();
            }
            return v.Value;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            TimeSpanConvert.TimeSpanWrite(writer, value, options, TimeSpanWriteCate.NumberS);
        }
    }

    /// <summary>
    /// TimeSpan?转换器
    /// 序列化转换为秒数字
    /// </summary>
    public class TimeSpanNullSConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpanConvert.Obj2TimeSpan(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            TimeSpanConvert.TimeSpanWrite(writer, value, options, TimeSpanWriteCate.NumberS);
        }
    }

    static class TimeSpanConvert
    {
        public static TimeSpan? Obj2TimeSpan(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.None:
                    break;
                case JsonTokenType.StartObject:
                    break;
                case JsonTokenType.EndObject:
                    break;
                case JsonTokenType.StartArray:
                    break;
                case JsonTokenType.EndArray:
                    break;
                case JsonTokenType.PropertyName:
                    break;
                case JsonTokenType.Comment:
                    break;
                case JsonTokenType.String:
                    try
                    {
                        return TimeSpan.Parse(reader.GetString());
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                case JsonTokenType.Number:
                    return TimeSpan.FromSeconds(reader.GetDouble());
                case JsonTokenType.True:
                    break;
                case JsonTokenType.False:
                    break;
                case JsonTokenType.Null:
                    return null;
                default:
                    break;
            }
            return null;
        }

        public static void TimeSpanWrite(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options, TimeSpanWriteCate cate)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var t = value.Value;
                switch (cate)
                {
                    case TimeSpanWriteCate.String:
                        writer.WriteStringValue(t.ToString());
                        break;
                    case TimeSpanWriteCate.NumberMS:
                        writer.WriteNumberValue((long)t.TotalMilliseconds);
                        break;
                    case TimeSpanWriteCate.NumberS:
                        writer.WriteNumberValue((long)t.TotalSeconds);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    enum TimeSpanWriteCate
    {
        String,
        NumberMS,
        NumberS
    }
}
