using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class BBSRouteAttribute : UwtRouteAttribute, IUwtNoRecordModule, IResultFilter
    {
        public BBSRouteAttribute()
            : base("BBS")
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult)
            {
                (context.Result as ViewResult).ViewData["ThemeCssList"] = BBSEx.ThemeCssList;
            }
        }
    }
}
