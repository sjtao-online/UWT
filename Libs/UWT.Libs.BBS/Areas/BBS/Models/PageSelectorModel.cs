using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    class PageSelectorModel : IPageSelectorModel
    {
        public string UrlBase { get; set; }
        public int CurrentPageCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int ItemTotal { get; set; }
    }
}
