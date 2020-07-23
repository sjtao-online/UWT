using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Filters;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Models.Templates.Lists;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.Expressions;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 列表界面扩展方法集合
    /// </summary>
    public static class ListPageEx
    {
        #region 常量 只在本类使用
        const string CustomTitleKey = "CustomTitle";
        const string FiltersConstKey = "FilterList";
        const string DefaultQueryValueMap = "DefaultQueryValueMap";
        #endregion
        #region 筛选项
        /// <summary>
        /// 填充上次的筛选项值
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="controller"></param>
        /// <param name="paramMaps"></param>
        /// <returns></returns>
        static IListToPage<TTable, TListItem> FillLastQuery<TTable, TListItem>(this IListToPage<TTable, TListItem> controller, Dictionary<string, string> paramMaps = null)
            where TTable : class
            where TListItem : class
        {
            var vd = controller.GetController().ViewData;
            if (!vd.ContainsKey(DefaultQueryValueMap))
            {
                var dic = new Dictionary<string, ValueTypeModel>();
                if (controller.GetController().HttpContext.Request.Query.ContainsKey(ListImplEx.QueryParamKey))
                {
                    string query = controller.GetController().HttpContext.Request.Query[ListImplEx.QueryParamKey];
                    var addWhere = ExpBuilder.Build<TTable>(query, paramMaps, dic);
                    vd[ListImplEx.QueryExpression] = addWhere;
                }
                vd[DefaultQueryValueMap] = dic;
            }
            return controller;
        }

        static ValueTypeModel GetQueryDefaultValue<TTable, TListItem>(this IListToPage<TTable, TListItem> controller, string key)
            where TTable : class
            where TListItem : class
        {
            if (controller.GetController().ViewData.ContainsKey(DefaultQueryValueMap))
            {
                var dic = controller.GetController().ViewData[DefaultQueryValueMap] as Dictionary<string, ValueTypeModel>;
                if (dic != null && dic.ContainsKey(key))
                {
                    return dic[key];
                }
            }
            return null;
        }

        /// <summary>
        /// 添加一个过滤器
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="controller"></param>
        /// <param name="title">标题</param>
        /// <param name="member">对应的属性</param>
        /// <param name="filter">过滤方式</param>
        /// <param name="valueType">值类型</param>
        /// <param name="canSelectItems">单选框选项</param>
        /// <returns></returns>
        public static IListToPage<TTable, TListItem> AddFilter<TTable, TListItem, TMember>(this IListToPage<TTable, TListItem> controller, string title, 
            System.Linq.Expressions.Expression<Func<TTable, TMember>> member,
            FilterType filter,
            FilterValueType valueType,
            List<HasFilterTypeChildrenNameKeyModel> canSelectItems = null)
            where TTable : class
            where TListItem : class
        {
            controller.FillLastQuery();
            string body = member.Body.ToString();
            var name = body.Substring(body.LastIndexOf('.') + 1);
            object tag = null;
            var value = controller.GetQueryDefaultValue(name);
            if (valueType == FilterValueType.Money)
            {
                tag = 2;
            }
            if (valueType == FilterValueType.TagSSelector || valueType == FilterValueType.IdComboBox)
            {
                tag = value?.Type;
            }
            controller.AddViewDataList(FiltersConstKey, (IFilterBasicModel)new FilterModel()
            {
                Title = title,
                FilterType = filter,
                ValueType = valueType,
                Name = name,
                CanSelectList = canSelectItems,
                Value = value?.Value,
                Tag = tag
            });
            return controller;
        }

        /// <summary>
        /// 添加自定义过滤器
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="controller">控制器</param>
        /// <param name="viewpath">cshtml路径</param>
        /// <param name="searchCallback">搜索回调要求返回{key:value}，会生成query参数</param>
        /// <param name="resetCallback">重置回调</param>
        /// <param name="initCallback">初始化回调, 参数是lastvalue</param>
        /// <param name="lastvalue">上一次值</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IListToPage<TTable, TListItem> AddFilterFromCshtml<TTable, TListItem>(this IListToPage<TTable, TListItem> controller, 
            string viewpath,
            string searchCallback,
            string resetCallback,
            string initCallback,
            string lastvalue)
            where TTable : class
            where TListItem : class
        {
            controller.AddViewDataList(FiltersConstKey, (IFilterBasicModel)new FilterModelFromCshtml()
            {
                ViewPath = viewpath,
                InitCallback = initCallback,
                LastValue = lastvalue,
                ResetCallback = resetCallback,
                SearchCallback = searchCallback
            });
            return controller;
        }

        /// <summary>
        /// 添加批量处理方法
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="controller"></param>
        /// <param name="title">标题</param>
        /// <param name="target">目标 一般是URL</param>
        /// <param name="askTooltip">是否询问</param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public static IListToPage<TTable, TListItem> AddBatchHandler<TTable, TListItem>(this IListToPage<TTable, TListItem> controller, 
            string title,
            string target, 
            string askTooltip = null,
            Type controllerType = null)
            where TTable : class
            where TListItem : class
        {
            controller.AddViewDataList(TemplateControllerEx.HandlersConstKey, controller.CreateHandleModel(new ListHandleModel()
            {
                IsBatch = true,
                AskContent = askTooltip,
                Title = title,
                Target = target,
                Type = HandleType.ApiPost
            }, controllerType));
            return controller;
        }
        #endregion


        #region List返回
        #region Api
        /// <summary>
        /// API分页列表返回
        /// </summary>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="this"></param>
        /// <param name="items"></param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static object ListObjectResult<TListItem>(this IListToPage<TListItem> @this, 
            List<TListItem> items,
            Action<IToPageModel> callback = null)
            where TListItem : class
        {
            var controller = @this.GetController();
            var toPageModel = new ToPageModel()
            {
                PageIndex = 0,
                PageSize = int.MaxValue,
                ItemTotal = items.Count
            };
            foreach (var item in items)
            {
                toPageModel.Items.Add(item);
            }
            callback?.Invoke(toPageModel);
            return controller.Success(toPageModel);
        }
        /// <summary>
        /// API分页列表返回
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="this"></param>
        /// <param name="selector"></param>
        /// <param name="where"></param>
        /// <param name="paramMaps"></param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static object ListObjectResult<TTable, TListItem>(this IListToPage<TTable, TListItem> @this,
            System.Linq.Expressions.Expression<Func<TTable, TListItem>> selector,
            System.Linq.Expressions.Expression<Func<TTable, bool>> where = null,
            Dictionary<string, string> paramMaps = null,
            Action<IToPageModel> callback = null)
            where TTable : class
            where TListItem : class
        {
            return @this.ListObjectResult<TTable, TListItem, object>(selector, where, null, null, paramMaps, callback);
        }
        /// <summary>
        /// API分页列表返回
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="this"></param>
        /// <param name="selector"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="orderbydesc"></param>
        /// <param name="paramMaps"></param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static object ListObjectResult<TTable, TListItem, TOrder>(this IListToPage<TTable, TListItem> @this,
            System.Linq.Expressions.Expression<Func<TTable, TListItem>> selector,
            System.Linq.Expressions.Expression<Func<TTable, bool>> where = null,
            System.Linq.Expressions.Expression<Func<TTable, TOrder>> orderby = null,
            System.Linq.Expressions.Expression<Func<TTable, TOrder>> orderbydesc = null,
            Dictionary<string, string> paramMaps = null,
            Action<IToPageModel> callback = null)
            where TTable : class
            where TListItem : class
        {
            ToPageModel toPageModel = new ToPageModel();
            var controller = @this.GetController();
            ListImplEx.FillList(controller, ref toPageModel, selector, where, orderby, orderbydesc, paramMaps);
            callback?.Invoke(toPageModel);
            return controller.Success(toPageModel);
        }
        #endregion
        /// <summary>
        /// View列表返回<br/>
        /// 单页显示这些项
        /// </summary>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="this"></param>
        /// <param name="items"></param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static IPageResult ListResult<TListItem>(this IListToPage<TListItem> @this, List<TListItem> items,
            Action<IToPageModel> callback = null)
            where TListItem : class
        {
            var controller = @this.GetController();
            var toPageViewModel = new ToPageViewModel()
            {
                ListViewModel = ModelCache.GetModelFromType(typeof(TListItem), ModelCache.ListViewModel),
                PageSelector = null,
                ItemTotal = items.Count,
                Items = new List<object>(),
                PageIndex = 0,
                PageSize = int.MaxValue,
            };
            toPageViewModel.PageSelector = new PageSelectorModel(toPageViewModel);
            foreach (var item in items)
            {
                toPageViewModel.Items.Add(item);
            }
            if (controller.ViewData.ContainsKey(CustomTitleKey))
            {
                toPageViewModel.CustomTitle = controller.ViewData[CustomTitleKey] as string;
            }
            controller.ViewBag.PageModel = toPageViewModel;
            callback?.Invoke(toPageViewModel);
            controller.ViewBag.ModelType = typeof(TListItem);
            return Models.Consts.PageTemplateKeyConst.GetPageResult(controller, controller.GetTemplatePageKey(Models.Consts.PageTemplateKeyConst.TemplateListKey));
        }
        /// <summary>
        /// View列表返回
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <param name="this">当前列表控制器</param>
        /// <param name="selector">选择器</param>
        /// <param name="where">条件</param>
        /// <param name="paramMaps">参数映射字典</param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static IPageResult ListResult<TTable, TListItem>(this IListToPage<TTable, TListItem> @this,
            System.Linq.Expressions.Expression<Func<TTable, TListItem>> selector,
            System.Linq.Expressions.Expression<Func<TTable, bool>> where = null,
            Dictionary<string, string> paramMaps = null,
            Action<IToPageModel> callback = null)
            where TTable : class
            where TListItem : class
        {
            return @this.ListResult<TTable, TListItem, object>(selector, where, null, null, paramMaps, callback);
        }
        /// <summary>
        /// View列表返回
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TListItem"></typeparam>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="this">当前列表控制器</param>
        /// <param name="selector">选择器</param>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="orderbydesc">排序</param>
        /// <param name="paramMaps">参数映射字典</param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static IPageResult ListResult<TTable, TListItem, TOrder>(this IListToPage<TTable, TListItem> @this,
            System.Linq.Expressions.Expression<Func<TTable, TListItem>> selector,
            System.Linq.Expressions.Expression<Func<TTable, bool>> where = null,
            System.Linq.Expressions.Expression<Func<TTable, TOrder>> orderby = null,
            System.Linq.Expressions.Expression<Func<TTable, TOrder>> orderbydesc = null,
            Dictionary<string, string> paramMaps = null,
            Action<IToPageModel> callback = null)
            where TTable : class
            where TListItem : class
        {
            var controller = @this.GetController();
            ListToPageCallBack(@this, callback, pageModel =>
            {
                ListImplEx.FillList(controller, ref pageModel, selector, where, orderby, orderbydesc, paramMaps);
            });
            return Models.Consts.PageTemplateKeyConst.GetPageResult(controller, controller.GetTemplatePageKey(Models.Consts.PageTemplateKeyConst.TemplateListKey));
        }

        /// <summary>
        /// View列表返回<br/>
        /// 用于已打开DB 使用linq获得方式
        /// </summary>
        /// <typeparam name="TTable">上层Select出来的对象</typeparam>
        /// <typeparam name="TListItem">列表模型</typeparam>
        /// <param name="this">当前列表控制器</param>
        /// <param name="selector">选择器</param>
        /// <param name="query">查询结果</param>
        /// <param name="paramMaps">参数映射字典</param>
        /// <param name="callback">回调方法，可以修改其中的值</param>
        /// <returns></returns>
        public static IPageResult ListResult<TTable, TListItem>(this IListToPage<TTable, TListItem> @this,
            System.Linq.Expressions.Expression<Func<TTable, TListItem>> selector,
            IQueryable<TTable> query,
            Dictionary<string, string> paramMaps = null,
            Action<IToPageModel> callback = null)
            where TTable : class
            where TListItem : class
        {
            var controller = @this.GetController();
            ListToPageCallBack(@this, callback, pageModel =>
            {
                ListImplEx.FillList(controller, ref pageModel, selector, query, paramMaps);
            });
            return Models.Consts.PageTemplateKeyConst.GetPageResult(controller, controller.GetTemplatePageKey(Models.Consts.PageTemplateKeyConst.TemplateListKey));
        }
        private static void ListToPageCallBack<TTable, TListItem>(IListToPage<TTable, TListItem> @this, Action<IToPageModel> callback, Action<ToPageModel> fileitems)
            where TTable : class
            where TListItem : class
        {
            var controller = @this.GetController();
            var toPageViewModel = new ToPageViewModel()
            {
                ListViewModel = ModelCache.GetModelFromType(typeof(TListItem), ModelCache.ListViewModel),
            };
            ToPageModel toPageModel = toPageViewModel;
            fileitems(toPageModel);
            StringBuilder urlBase = new StringBuilder(controller.HttpContext.Request.Path + "?");
            foreach (var item in controller.HttpContext.Request.Query)
            {
                if (item.Key == ListImplEx.PageIndexKey)
                {
                    continue;
                }
                urlBase.Append(item.Key);
                urlBase.Append("=");
                urlBase.Append(System.Web.HttpUtility.UrlEncode(item.Value));
                urlBase.Append("&");
            }
            urlBase.Append(ListImplEx.PageIndexKey);
            urlBase.Append("=");
            toPageViewModel.PageSelector = new PageSelectorModel(toPageViewModel)
            {
                UrlBase = urlBase.ToString()
            };
            if (controller.ViewData.ContainsKey(CustomTitleKey))
            {
                toPageViewModel.CustomTitle = controller.ViewData[CustomTitleKey] as string;
            }
            controller.ViewBag.PageModel = toPageViewModel;
            callback?.Invoke(toPageViewModel);
            controller.ViewBag.ModelType = typeof(TListItem);
        }
        #endregion
    }
}
