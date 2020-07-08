using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Templates.Services.Expressions
{
    class ExpBuilder
    {
        #region Expressions
        public delegate Expression ExpressionEventHandler(Expression left, Expression right);
        static Expression ExpressionNull = Expression.Constant(null);
        static Dictionary<string, ExpressionEventHandler> Map = new Dictionary<string, ExpressionEventHandler>()
        {
            ["EQ"] = Expression.Equal,
            ["NE"] = Expression.NotEqual,
            ["GT"] = Expression.GreaterThan,
            ["LT"] = Expression.LessThan,
            ["GE"] = Expression.GreaterThanOrEqual,
            ["LE"] = Expression.LessThanOrEqual,
            ["BT"] = Between,
            ["IN"] = In,
            ["%%"] = Like,
            ["L%"] = StartWith,
            ["%L"] = EndWith
        };
        static Dictionary<Type, Type> ListMap = new Dictionary<Type, Type>()
        {
            [typeof(int)] = typeof(List<int>),
            [typeof(uint)] = typeof(List<uint>),
            [typeof(long)] = typeof(List<long>),
            [typeof(ulong)] = typeof(List<ulong>),
            [typeof(short)] = typeof(List<short>),
            [typeof(ushort)] = typeof(List<ushort>),
            [typeof(bool)] = typeof(List<bool>),
            [typeof(byte)] = typeof(List<byte>),
            [typeof(sbyte)] = typeof(List<sbyte>),
            [typeof(DateTime)] = typeof(List<DateTime>),
            [typeof(string)] = typeof(List<string>),
        };
        static Dictionary<Type, Func<object>> ListObjectMap = new Dictionary<Type, Func<object>>()
        {
            [typeof(int)] = () => new List<int>(),
            [typeof(uint)] = () => new List<uint>(),
            [typeof(long)] = () => new List<long>(),
            [typeof(ulong)] = () => new List<ulong>(),
            [typeof(short)] = () => new List<short>(),
            [typeof(ushort)] = () => new List<ushort>(),
            [typeof(bool)] = () => new List<bool>(),
            [typeof(byte)] = () => new List<byte>(),
            [typeof(sbyte)] = () => new List<sbyte>(),
            [typeof(DateTime)] = () => new List<DateTime>(),
            [typeof(string)] = () => new List<string>(),
        };
        public static Expression StartWith(Expression left, Expression right)
        {
            return Expression.Call(left, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), right);
        }
        public static Expression EndWith(Expression left, Expression right)
        {
            return Expression.Call(left, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), right);
        }
        public static Expression Like(Expression left, Expression right)
        {
            return Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), right);
        }
        public static Expression In(Expression left, Expression right)
        {
            return Expression.Call(right, ListMap[left.Type].GetMethod("Contains", new Type[] { left.Type }), left);
        }
        public static Expression Between(Expression left, Expression right)
        {
            LeftRightExp lr = right as LeftRightExp;
            if (lr.Right == ExpressionNull && lr.Left == ExpressionNull)
            {
                return null;
            }
            else if (lr.Right == ExpressionNull)
            {
                return Expression.GreaterThanOrEqual(left, lr.Left);
            }
            else if (lr.Left == ExpressionNull)
            {
                return Expression.LessThanOrEqual(left, lr.Right);
            }
            return Expression.And(Expression.GreaterThanOrEqual(left, lr.Left), Expression.LessThanOrEqual(left, lr.Right));
        }
        #endregion
        public static Expression<Func<TTable, bool>> Build<TTable>(string build, Dictionary<string, string> keyValuePairs, Dictionary<string, ValueTypeModel> valueMap = null)
            where TTable : class
        {
            if (!string.IsNullOrEmpty(build))
            {
                if (valueMap == null)
                {
                    valueMap = new Dictionary<string, ValueTypeModel>();
                }
                var type = typeof(TTable);
                var param = Expression.Parameter(type, "m");
                int index = 0;
                Expression result = null;
                try
                {
                    result = Eval(build.AsSpan(), ref index, type, param, keyValuePairs, valueMap);
                    if (result == null)
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw new QueryEvalException($"解析\"{build}\"出错,位置：{index}");
                }
                return Expression.Lambda<Func<TTable, bool>>(result, param);
            }
            return null;
        }

        private static Expression Eval(ReadOnlySpan<char> readOnlySpan, ref int index, Type type, ParameterExpression param, Dictionary<string, string> keyValuePairs, Dictionary<string, ValueTypeModel> refKeyValueMap)
        {
            Expression expression = null;
            bool handler = false;
            while (true)
            {
                if (index == readOnlySpan.Length)
                {
                    break;
                }
                char first = readOnlySpan[index];
                if (first == '(')
                {
                    index++;
                    var exp = Eval(readOnlySpan, ref index, type, param, keyValuePairs, refKeyValueMap);
                    if (expression == null)
                    {
                        expression = exp;
                    }
                    else
                    {
                        if (handler)
                        {
                            expression = Expression.And(expression, exp);
                        }
                        else
                        {
                            expression = Expression.Or(expression, exp);
                        }
                    }
                }
                else if (first == '&')
                {
                    handler = true;
                    index++;
                }
                else if (first == '|')
                {
                    handler = false;
                    index++;
                }
                else if (first == ')')
                {
                    index++;
                    break;
                }
                else
                {
                    string key = GetKeyName(readOnlySpan, ref index);
                    if (keyValuePairs != null && keyValuePairs.ContainsKey(key))
                    {
                        key = keyValuePairs[key];
                    }
                    index++;
                    string op = $"{readOnlySpan[index]}{readOnlySpan[index + 1]}";
                    index += 2;
                    string rvalue = null;
                    var exp = Map[op](Expression.Property(param, key), GetValueExpression(readOnlySpan, ref index, op, type.GetProperty(key).PropertyType, ref rvalue));
                    if (exp != null)
                    {
                        refKeyValueMap[key] = new ValueTypeModel()
                        {
                            Value = rvalue,
                            Type = op
                        };
                        if (expression == null)
                        {
                            expression = exp;
                        }
                        else
                        {
                            if (handler)
                            {
                                expression = Expression.And(expression, exp);
                            }
                            else
                            {
                                expression = Expression.Or(expression, exp);
                            }
                        }
                    }
                }
            }
            return expression;
        }

        #region Get
        private static Expression GetValueExpression(ReadOnlySpan<char> readOnlySpan, ref int index, string op, Type type, ref string rvalue)
        {
            switch (op)
            {
                case "EQ":
                case "NE":
                    return Expression.Constant(GetObject(readOnlySpan, ref index, type, ref rvalue));
                //  double,int,long,short,uint,ulong,ushort,DateTime
                case "GT":
                case "LT":
                case "GE":
                case "LE":
                    return Expression.Constant(GetNumber(readOnlySpan, ref index, type, ref rvalue));
                //  double,int,long,short,uint,ulong,ushort,DateTime
                case "BT":
                    return GetBetween(readOnlySpan, ref index, type, ref rvalue);
                case "IN":
                    return GetIn(readOnlySpan, ref index, type, ref rvalue);
                //  string
                case "%%":
                case "L%":
                case "%L":
                    if (readOnlySpan[index] == '\\')
                    {
                        index++;
                        StringBuilder builder = new StringBuilder();
                        for (; index < readOnlySpan.Length; index++)
                        {
                            if (readOnlySpan[index] == '\\')
                            {
                                if (readOnlySpan.Length > index + 1 && readOnlySpan[index + 1] == '\\')
                                {
                                    index++;
                                    builder.Append('\\');
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                builder.Append(readOnlySpan[index]);
                            }
                        }
                        index++;
                        rvalue = builder.ToString();
                        return Expression.Constant(rvalue);
                    }
                    else
                    {
                        //  出错
                        throw new QueryEvalException($"解析出错，位置{index}应该是\"\\\"");
                    }
                default:
                    break;
            }
            return null;
        }
        private static Expression GetIn(ReadOnlySpan<char> readOnlySpan, ref int index, Type type, ref string rvalue)
        {
            rvalue = GetString(readOnlySpan, ref index, SimpleReadOnlySpan);
            string[] sws = rvalue.Split(',');
            var list = ListObjectMap[type]() as System.Collections.IList;
            foreach (var item in sws)
            {
                list.Add(ChangeType(item, type));
            }
            return Expression.Constant(list);
        }

        const string BetweenReadOnlySpan = "|&(,)";
        const string SimpleReadOnlySpan = "|&()";
        private static Expression GetBetween(ReadOnlySpan<char> readOnlySpan, ref int index, Type type, ref string rvalue)
        {
            string sw1 = GetString(readOnlySpan, ref index, BetweenReadOnlySpan);
            index++;
            string sw2 = GetString(readOnlySpan, ref index, BetweenReadOnlySpan);
            rvalue = sw1 + "," + sw2;
            var left = string.IsNullOrEmpty(sw1) ? ExpressionNull : Expression.Constant(ChangeType(sw1, type));
            var right = string.IsNullOrEmpty(sw2) ? ExpressionNull : Expression.Constant(ChangeType(sw2, type));
            return LeftRightExp.Build(left, right);
        }

        private static object GetObject(ReadOnlySpan<char> readOnlySpan, ref int index, Type type, ref string rvalue)
        {
            rvalue = GetString(readOnlySpan, ref index, SimpleReadOnlySpan);
            return ChangeType(rvalue, type);
        }
        /// <summary>
        /// 获得数值
        /// </summary>
        /// <param name="readOnlySpan"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="rvalue"></param>
        /// <returns></returns>
        private static object GetNumber(ReadOnlySpan<char> readOnlySpan, ref int index, Type type, ref string rvalue)
        {
            rvalue = GetString(readOnlySpan, ref index, SimpleReadOnlySpan);
            return ChangeType(rvalue, type);
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object ChangeType(string sw, Type type)
        {
            if (type == typeof(int))
            {
                return int.Parse(sw);
            }
            else if (type == typeof(uint))
            {
                return uint.Parse(sw);
            }
            else if (type == typeof(short))
            {
                return short.Parse(sw);
            }
            else if (type == typeof(ushort))
            {
                return ushort.Parse(sw);
            }
            else if (type == typeof(long))
            {
                return long.Parse(sw);
            }
            else if (type == typeof(ulong))
            {
                return long.Parse(sw);
            }
            else if (type == typeof(double))
            {
                return double.Parse(sw);
            }
            else if (type == typeof(float))
            {
                return float.Parse(sw);
            }
            else if (type == typeof(DateTime))
            {
                if (DateTime.TryParse(sw, out DateTime dateTime))
                {
                    return dateTime;
                }
                return new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(long.Parse(sw));
            }
            else if (type == typeof(string))
            {
                return sw;
            }
            else if (type == typeof(bool))
            {
                return bool.Parse(sw);
            }
            //  出错
            return 0;
        }
        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="readOnlySpan"></param>
        /// <param name="index"></param>
        /// <param name="endChars"></param>
        /// <returns></returns>
        private static string GetString(ReadOnlySpan<char> readOnlySpan, ref int index, string endChars)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool isChangeChar = false;
            for (; index < readOnlySpan.Length; index++)
            {
                if (isChangeChar)
                {
                    stringBuilder.Append(readOnlySpan[index]);
                    isChangeChar = false;
                    break;
                }
                if (readOnlySpan[index] == '\\')
                {
                    isChangeChar = true;
                    break;
                }
                if (endChars.Contains(readOnlySpan[index]))
                {
                    break;
                }
                stringBuilder.Append(readOnlySpan[index]);
            }
            return stringBuilder.ToString();

        }
        /// <summary>
        /// 获得Key名称
        /// </summary>
        /// <param name="readOnlySpan"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetKeyName(ReadOnlySpan<char> readOnlySpan, ref int index)
        {
            return GetString(readOnlySpan, ref index, ":");
        }
        #endregion
    }

    [Serializable]
    class QueryEvalException : Exception
    {
        public QueryEvalException() { }
        public QueryEvalException(string message) : base(message) { }
        public QueryEvalException(string message, Exception inner) : base(message, inner) { }
        protected QueryEvalException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    class LeftRightExp : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public static Expression Build(Expression left, Expression right)
        {
            return new LeftRightExp()
            {
                Left = left,
                Right = right
            };
        }
    }
}
