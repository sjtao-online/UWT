using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UWT.Templates.Models.Database;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 数据库扩展方法
    /// </summary>
    public static class DataConnectionEx
    {
        static List<PropertyInfo> PropertiesList = null;
        static Dictionary<Type, PropertyInfo> InterfaceToPropertyMap = null;
        static PropertyInfo IDbTableBaseIdProperty = GetPropertyFromType(typeof(IDbTableBase), nameof(IDbTableBase.Id));
        static PropertyInfo GetPropertyFromType(this Type type, string propname)
        {
            return type.GetProperty(propname, BindingFlags.Public | BindingFlags.Instance);
        }
        /// <summary>
        /// 插入条目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">表</param>
        /// <param name="insert">插入字典</param>
        /// <returns></returns>
        public static int UwtInsertWithInt32<T>(this ITable<T> table, Dictionary<string, object> insert)
        {
            InitDataConnectionType();
            var type = InterfaceToPropertyMap[typeof(T)].PropertyType.GenericTypeArguments[0];
            var exp = (Expression<Func<T>>)Expression.Lambda(typeof(Func<T>), Expression.MemberInit(Expression.New(type), BuildMemberBindings(insert, type)));
            return table.InsertWithInt32Identity(exp);
        }
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> UwtQueryPageSelector<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return query.Skip(pageIndex * pageSize).Take(pageSize);
        }
        /// <summary>
        /// 更新条目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">表</param>
        /// <param name="id">Id</param>
        /// <param name="update">更新字典</param>
        /// <returns></returns>
        public static int UwtUpdate<T>(this ITable<T> table, int id, Dictionary<string, object> update)
        {
            InitDataConnectionType();
            var ttype = typeof(T);
            var type = InterfaceToPropertyMap[typeof(T)].PropertyType.GenericTypeArguments[0];
            var exp = (Expression<Func<T, T>>)Expression.Lambda(typeof(Func<T, T>), Expression.MemberInit(Expression.New(type), BuildMemberBindings(update, type)), Expression.Parameter(typeof(T), "m"));
            var paramter = Expression.Parameter(ttype, "m");
            var pp = ttype.GetPropertyFromType(nameof(IDbTableBase.Id));
            if (pp == null)
            {
                pp = IDbTableBaseIdProperty;
            }
            var prop = Expression.Property(paramter, pp);
            var func = Expression.Equal(prop, Expression.Constant(id));
            var where = (Expression<Func<T, bool>>)Expression.Lambda(typeof(Func<T, bool>), func, paramter);
            return table.Update(where, exp);
        }
        /// <summary>
        /// 获得操作表对象<br/>
        /// 支持从接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static ITable<T> UwtGetTable<T>(this DataConnection connection)
            where T : class
        {
            if (!typeof(T).IsInterface)
            {
                return connection.GetTable<T>();
            }
            InitDataConnectionType(connection);
            var t = typeof(T);
            if (InterfaceToPropertyMap.ContainsKey(t))
            {
                return InterfaceToPropertyMap[t].GetValue(connection) as ITable<T>;
            }
            foreach (var item in PropertiesList)
            {
                if (t.IsAssignableFrom(item.PropertyType.GenericTypeArguments[0]))
                {
                    InterfaceToPropertyMap[t] = item;
                    return item.GetValue(connection) as ITable<T>;
                }
            }
            return null;
        }

        private static void InitDataConnectionType(DataConnection connection = null)
        {
            if (PropertiesList == null)
            {
                if (connection == null)
                {
                    connection = DbCreator.CreateDb();
                }
                PropertiesList = new List<PropertyInfo>();
                InterfaceToPropertyMap = new Dictionary<Type, PropertyInfo>();
                var itabletype = typeof(ITable<>);
                //  获得所有属性
                foreach (var item in connection.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!item.PropertyType.IsGenericType)
                    {
                        continue;
                    }
                    if (itabletype == item.PropertyType.GetGenericTypeDefinition())
                    {
                        PropertiesList.Add(item);
                    }
                }
            }
        }
        private static List<MemberBinding> BuildMemberBindings(Dictionary<string, object> pairs, Type type)
        {
            List<MemberBinding> binds = new List<MemberBinding>();
            foreach (var item in pairs)
            {
                var m = type.GetProperty(item.Key);
                binds.Add(Expression.Bind(m, Expression.Constant(item.Value, m.PropertyType)));
            }
            return binds;
        }

    }
}
