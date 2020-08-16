using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Areas.Forums.Models
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ForumRouteAttribute : UwtRouteAttribute, IUwtNoRecordModule
    {
        public ForumRouteAttribute()
            : base("Forum")
        {
        }
    }
}
