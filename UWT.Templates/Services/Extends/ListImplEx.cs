using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Lists;
using UWT.Templates.Services.Expressions;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 获得列表方法扩展集合
    /// </summary>
    static class ListImplEx
    {
        public const string QueryExpression = "TempSaveQueryExpression";
        public const string PageIndexKey = "pageIndex";
        public const string PageSizeKey = "pageSize";
        public const string QueryParamKey = "query";
        public static void FillList<TTable, TListItem, TOrder>(Controller controller, ref ToPageModel ToPageModel,
            Expression<Func<TTable, TListItem>> selector,
            Expression<Func<TTable, bool>> where,
            Expression<Func<TTable, TOrder>> orderby,
            Expression<Func<TTable, TOrder>> orderbydesc,
            Dictionary<string, string> paramMaps)
            where TTable : class
            where TListItem : class
        {
            int pageIndex, pageSize;
            Expression<Func<TTable, bool>> addWhere;
            CommonHandle<TTable, TListItem>(controller, ToPageModel, paramMaps, out pageIndex, out addWhere, out pageSize);
            using (var db = Models.Database.DbCreator.CreateDb())
            {
                IQueryable<TTable> querys = db.UwtGetTable<TTable>();
                HandleQuerys(ToPageModel, selector, where, orderby, orderbydesc, pageIndex, pageSize, addWhere, querys);
            }
        }

        private static void HandleQuerys<TTable, TListItem, TOrder>(ToPageModel ToPageModel, Expression<Func<TTable, TListItem>> selector, Expression<Func<TTable, bool>> where, Expression<Func<TTable, TOrder>> orderby, Expression<Func<TTable, TOrder>> orderbydesc, int pageIndex, int pageSize, Expression<Func<TTable, bool>> addWhere, IQueryable<TTable> querys)
            where TTable : class
            where TListItem : class
        {
            if (addWhere != null)
            {
                querys = querys.Where(addWhere);
            }
            if (where != null)
            {
                querys = querys.Where(where);
            }
            if (orderby != null)
            {
                querys = querys.OrderBy(orderby);
            }
            if (orderbydesc != null)
            {
                querys = querys.OrderByDescending(orderbydesc);
            }
            var query = querys.Select(selector);
            ToPageModel.ItemTotal = query.Count();
            ToPageModel.Items = new List<object>();
            foreach (var item in query.UwtQueryPageSelector(pageIndex, pageSize).ToList())
            {
                ToPageModel.Items.Add(item);
            }
        }

        private static void CommonHandle<TTable, TListItem>(Controller controller, ToPageModel ToPageModel, Dictionary<string, string> paramMaps, out int pageIndex, out Expression<Func<TTable, bool>> addWhere, out int pageSize)
            where TTable : class
            where TListItem : class
        {
            Type type = typeof(TListItem);
            pageIndex = 0;
            string constPageIndexKey = PageIndexKey;
            if (controller.HttpContext.Request.Query.ContainsKey(constPageIndexKey))
            {
                string value = controller.HttpContext.Request.Query[constPageIndexKey][0];
                if (int.TryParse(value, out int intValue))
                {
                    pageIndex = intValue;
                    //  小于0的值就是最后一页
                    if (pageIndex < 0)
                    {
                        pageIndex = -1;
                    }
                }
            }
            addWhere = null;
            if (controller.ViewData.ContainsKey(QueryExpression) && controller.ViewData[QueryExpression] is Expression<Func<TTable, bool>>)
            {
                addWhere = controller.ViewData[QueryExpression] as Expression<Func<TTable, bool>>;
            }
            else if (controller.HttpContext.Request.Query.ContainsKey(QueryParamKey))
            {
                string value = controller.HttpContext.Request.Query[QueryParamKey];
                addWhere = ExpBuilder.Build<TTable>(value, paramMaps, null);
            }
            int defaultPageSize = GetDefaultPageSize(controller);
            pageSize = defaultPageSize;
            string constPageCountKey = PageSizeKey;
            if (controller.HttpContext.Request.Query.ContainsKey(constPageCountKey))
            {
                string value = controller.HttpContext.Request.Query[constPageIndexKey][0];
                if (int.TryParse(value, out int intValue))
                {
                    pageSize = intValue;
                    //  小于0的值就是默认值
                    if (pageSize <= 0)
                    {
                        pageSize = defaultPageSize;
                    }
                }
            }
            ToPageModel.PageIndex = pageIndex;
            ToPageModel.PageSize = pageSize;
        }

        static int DefaultPageSize = 30;
        private static int GetDefaultPageSize(Controller controller)
        {
            if (controller is IListToPageConfig)
            {
                return ((IListToPageConfig)controller).DefaultPageSize;
            }
            return DefaultPageSize;
        }
        public static void FillList<TTable, TListItem>(Controller controller, ref ToPageModel pageModel, Expression<Func<TTable, TListItem>> selector, IQueryable<TTable> query, Dictionary<string, string> paramMaps)
            where TTable : class
            where TListItem : class
        {
            int pageIndex, pageSize;
            Expression<Func<TTable, bool>> addWhere;
            CommonHandle<TTable, TListItem>(controller, pageModel, paramMaps, out pageIndex, out addWhere, out pageSize);
            HandleQuerys<TTable, TListItem, object>(pageModel, selector, null, null, null, pageIndex, pageSize, addWhere, query);
        }
    }
}
