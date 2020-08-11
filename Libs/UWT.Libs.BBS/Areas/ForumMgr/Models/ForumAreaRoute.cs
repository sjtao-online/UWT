using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Areas.ForumMgr.Models
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ForumAreaRouteAttribute : UwtRouteAttribute, IUwtShowName
    {
        public string ShowName { get; set; }
        public ForumAreaRouteAttribute(string showname, string template = null)
            : base("ForumMgr", template, "论坛")
        {
            ShowName = showname;
        }
    }
}
