using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class BBSRouteAttribute : UwtRouteAttribute, IUwtNoRecordModule
    {
        public BBSRouteAttribute()
            : base("BBS")
        {
        }
    }
}
