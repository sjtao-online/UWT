using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UWT.Templates.Services.Filters
{
    class DebugRequestBodyMiddleware : IMiddleware
    {
        public DebugRequestBodyMiddleware()
        {

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();
            await next(context);
        }
    }
}
