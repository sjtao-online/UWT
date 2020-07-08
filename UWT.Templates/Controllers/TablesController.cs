using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Controllers
{
    /// <summary>
    /// 数据表转换器
    /// </summary>
    public class TablesController : Controller
        , ITemplateController
    {
        /// <summary>
        /// 转换Id到文本
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="idColumnName">id列名</param>
        /// <param name="nameColumnName">名称列名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [NonAction]
        public string ChangeIdToText(string tableName, string idColumnName, string nameColumnName, int id)
        {
            string title = null;
            this.UsingDb(db=>
            {
                var cmdText = $"select {nameColumnName} as title from {tableName} where {idColumnName} = '{id}'";
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Connection = db.Connection;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title = reader.GetString(0);
                        }
                    }
                }
            });
            return title;
        }
        /// <summary>
        /// 转换Id列表到文本
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="idColumnName"></param>
        /// <param name="nameColumnName"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [NonAction]
        public string ChangeIdsToText(string tableName, string idColumnName, string nameColumnName, List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return "";
            }
            List<string> titles = new List<string>();
            using (var db = this.GetDB())
            {
                var cmdText = $"select {nameColumnName} as title from {tableName} where {idColumnName} in ({string.Join(',', ids)})";
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Connection = db.Connection;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            titles.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return string.Join(",", titles);
        }


        private void FillChildren(LinqToDB.Data.DataConnection db, string tableName, string nameColumnName, string idColumnName, string parentIdColumnName, int parentId, string where, ref List<TitleIdModel> children)
        {
            var cmdText = $"select {idColumnName} as id, {nameColumnName} as title from {tableName}";
            List<string> wheres = new List<string>();
            if (!string.IsNullOrEmpty(parentIdColumnName))
            {
                wheres.Add(parentIdColumnName + " = " + parentId);
            }
            if (!string.IsNullOrEmpty(where))
            {
                wheres.Add(where);
            }
            if (wheres.Count > 0)
            {
                cmdText += " where " + string.Join(" and ", wheres);
            }
            List<TitleIdModel> list = new List<TitleIdModel>();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Connection = db.Connection;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TitleIdModel()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }
            }
            if (!string.IsNullOrEmpty(parentIdColumnName))
            {
                foreach (var item in list)
                {
                    List<TitleIdModel> c = new List<TitleIdModel>();
                    FillChildren(db, tableName, nameColumnName, idColumnName, parentIdColumnName, item.Id, where, ref c);
                    if (c.Count != 0)
                    {
                        children.Add(new HasChildrenTitleIdModelX()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Children = c
                        });
                    }
                    else
                    {
                        children.Add(item);
                    }
                }
            }
            else
            {
                children.AddRange(list);
            }
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="rkey">key</param>
        /// <returns></returns>
        public object GetListRun(string rkey)
        {
            using var db = this.GetDB();
            List<TitleIdModel> list = new List<TitleIdModel>();
            if (Services.Caches.ModelCache.TableRKeyMap.ContainsKey(rkey))
            {
                var r = Services.Caches.ModelCache.TableRKeyMap[rkey];
                try
                {
                    FillChildren(db, r.TableName, r.NameColumnName, r.IdColumnName, r.ParentIdColumnName, 0, r.Where, ref list);
                }
                catch (Exception ex)
                {
                    this.LogError("database error: " + ex.ToString());
                    return this.Error(ErrorCode.DatatableFaled);
                }
            }
            return this.Success(list);
        }
    }
    class QueryTitleModel
    {
        public string Title { get; set; }
    }
    class HasChildrenTitleIdModelX : TitleIdModel
    {
        public List<TitleIdModel> Children { get; set; }
    }
}