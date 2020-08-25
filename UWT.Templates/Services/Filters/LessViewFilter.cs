using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UWT.Templates.Services.Filters
{
    class LessViewFilter : IResultFilter
    {
        public async void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ViewResult)
            {
                var vr = context.Result as ViewResult;
                if (context.HttpContext.Items.ContainsKey(Models.TagHelpers.Basic.LessLinkerTagHelper.hasLess))
                {
                    string rPath =
#if DEBUG
                    ""
#else
                    "/_content/UWT.Templates"
#endif
                    ;
                    await context.HttpContext.Response.WriteAsync($"<script src=\"{rPath}/admins/js/less.min.js\" type=\"text/javascript\"></script>");
                }
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
