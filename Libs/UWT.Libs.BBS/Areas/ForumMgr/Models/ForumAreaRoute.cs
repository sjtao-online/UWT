using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Routes;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ForumAreaRouteAttribute : UwtRouteAttribute
    {
        public ForumAreaRouteAttribute(string template = null)
            : base("ForumMgr", template, "论坛")
        {
        }
    }
}
