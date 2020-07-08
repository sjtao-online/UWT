using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Database
{
    /// <summary>
    /// 数据库配置模型
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 当前配置项的Key
        /// </summary>
        public string Current { get; set; }
        /// <summary>
        /// 配置字典
        /// 可以缓存多组配置
        /// </summary>
        public Dictionary<string, DbSettingModel> DbSettings { get; set; }
    }
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbSettingModel
    {
        /// <summary>
        /// 数据类型
        /// 默认MySql
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        /// 数据库名
        /// 0
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// 服务器
        /// 1
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 端口
        /// 2
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 用户名
        /// 3
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// 4
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 字符集
        /// 5
        /// </summary>
        public string Charset { get; set; }
        /// <summary>
        /// 格式化字符串
        /// 默认:$"Database={0:DbName};Data Source={1:Server};Port={2:Port};User={3:User};Password={4:Pwd};Charset={5:Charset}"
        /// </summary>
        public string Format { get; set; }
    }
}
