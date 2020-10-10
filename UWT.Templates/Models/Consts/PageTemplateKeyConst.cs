using Microsoft.AspNetCore.Mvc;
using System;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Consts
{
    class PageTemplateKeyConst
    {
        public static IPageResult GetPageResult<TResultBasic>(Controller controller)
            where TResultBasic : IPageResult
        {
            var pageResult = Activator.CreateInstance<TResultBasic>();
            if (pageResult is PageResultTemplateBasic tb)
            {
                tb.Controller = controller;
            }
            return pageResult;
        }
    }
}
