using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 分页扩展方法
    /// </summary>
    public static class PageSelectorEx
    {
        /// <summary>
        /// 获得总页数
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int GetPageCount(this IPageSelectorModel page)
        {
            return page.ItemTotal / page.PageSize + ((page.ItemTotal % page.PageSize) == 0 ? 0 : 1);
        }

        /// <summary>
        /// 是否首页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool IsFirstPage(this IPageSelectorModel page)
        {
            return page.PageIndex == 0;
        }

        /// <summary>
        /// 是否尾页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool IsLastPage(this IPageSelectorModel page)
        {
            return page.GetPageCount() - 1 == page.PageIndex;
        }

        /// <summary>
        /// 获得可显示页码区间
        /// </summary>
        /// <param name="page"></param>
        /// <param name="maxPages">最大显示多少个按钮</param>
        /// <returns></returns>
        public static Range<int> GetRenderRange(this IPageSelectorModel page, int maxPages = 10)
        {
            int halfPageCount = maxPages / 2;
            int pageCount = page.GetPageCount();
            int pageBegin = 0;
            int pageEnd = 0;
            if (pageCount > maxPages)
            {
                if (page.PageIndex <= halfPageCount)
                {
                    pageEnd = maxPages;
                }
                else if (page.PageIndex >= pageCount - halfPageCount)
                {
                    pageEnd = pageCount;
                    pageBegin = pageCount - maxPages;
                    if (pageBegin < 0)
                    {
                        pageBegin = 0;
                    }
                }
                else
                {
                    pageBegin = page.PageIndex - halfPageCount;
                    pageEnd = page.PageIndex + halfPageCount;
                }
            }
            else
            {
                pageEnd = pageCount;
            }
            return new Range<int>()
            {
                Max = pageEnd,
                Min = pageBegin
            };
        }
    }
}
