using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Lists
{
    class ToPageModel : IToPageModel
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 结果总数
        /// </summary>
        public int ItemTotal { get; set; }
        /// <summary>
        /// 项目组
        /// </summary>
        public IList<object> Items { get; set; }
    }
    class ToPageViewModel : ToPageModel, IToPageViewModel
    {
        public string CustomTitle { get; set; }
        public IListViewModel ListViewModel { get; set; }
        public IPageSelectorModel PageSelector { get; set; }
    }
    class PageSelectorModel : IPageSelectorModel
    {
        private IToPageModel ToPageModel;
        public PageSelectorModel(IToPageModel toPageModel)
        {
            ToPageModel = toPageModel;
        }
        public int CurrentPageCount => ToPageModel.Items.Count;

        public string UrlBase { get; set; }
        public int PageIndex => ToPageModel.PageIndex;
        public int ItemTotal => ToPageModel.ItemTotal;
        public int PageSize => ToPageModel.PageSize;
    }
}
