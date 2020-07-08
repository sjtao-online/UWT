using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 任务扩展，方便使用
    /// </summary>
    public static class LoggerEx
    {
        static Dictionary<string, string> Assembily2PathMap = new Dictionary<string, string>();
        /// <summary>
        /// 设置程序集
        /// </summary>
        /// <param name="assemblies"></param>
        public static void ConfigAssembilies(List<Assembly> assemblies)
        {
            lock (Assembily2PathMap)
            {
                foreach (var item in assemblies)
                {
                    Assembily2PathMap.Add($"\\{item.GetName().Name}\\", null);
                }
            }
        }
        /// <summary>
        /// 获得日志管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ILogger<T> GetLogger<T>(this T @this)
        {
            while (Services.StartupEx.ApplicationBuilderEx.ApplicationBuilder == null)
            {
                System.Threading.Thread.Sleep(20);
            }
            return Services.StartupEx.ApplicationBuilderEx.ApplicationBuilder.ApplicationServices.GetService(typeof(ILogger<T>)) as ILogger<T>;
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">信息</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogError<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Error, msg, filename, memberName, lineNo);
        }
        /// <summary>
        /// 记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">内容</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogTrace<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Trace, msg, filename, memberName, lineNo);
        }
        /// <summary>
        /// 记录调试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">信息</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogDebug<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Debug, msg, filename, memberName, lineNo);
        }
        /// <summary>
        /// 记录警告
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">内容</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogWarning<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Warning, msg, filename, memberName, lineNo);
        }
        /// <summary>
        /// 记录消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">内容</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogInformation<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Information, msg, filename, memberName, lineNo);
        }
        /// <summary>
        /// 记录崩溃
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="msg">内容</param>
        /// <param name="filename"></param>
        /// <param name="memberName"></param>
        /// <param name="lineNo"></param>
        public static void LogCritical<T>(this T @this, string msg, [CallerFilePath] string filename = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNo = 0)
        {
            Log(@this, LogLevel.Critical, msg, filename, memberName, lineNo);
        }
        private static void Log<T>(T @this, LogLevel level, string msg, string filename, string memberName, int lineNo)
        {
            //  显示相对目录，一定程度减少日志量级
            foreach (var item in Assembily2PathMap)
            {
                if (item.Value != null)
                {
                    if (filename.StartsWith(item.Value))
                    {
                        filename = filename.Substring(item.Value.Length);
                        break;
                    }
                }
                else
                {
                    int index = filename.IndexOf(item.Key);
                    if (index != -1)
                    {
                        var path = filename.Substring(0, index);
                        lock (Assembily2PathMap)
                        {
                            Assembily2PathMap[item.Key] = path;
                        }
                        filename = filename.Substring(path.Length);
                        break;
                    }
                }
            }
            GetLogger(@this).Log(level, $"{memberName} [{filename},{lineNo}] {msg}");
        }
    }
}
