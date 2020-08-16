using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.BBS.Areas.Forums.Models
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class AuthForumAttribute : Attribute
    {
        public AuthForumAttribute()
        {
        }
    }
}
