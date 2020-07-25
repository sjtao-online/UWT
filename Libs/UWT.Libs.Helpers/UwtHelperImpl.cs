using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.Helpers.Models;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;
using System.Linq;

namespace UWT.Libs.Helpers
{
    class UwtHelperImpl : IUwtHelper
    {
        public bool HasHelper(string url)
        {
            bool hasHelper = false;
            TemplateControllerEx.UsingDb(null, db =>
            {
                hasHelper = (from it in db.UwtGetTable<IDbHelperTable>() where it.PublishTime != null && it.Url.Contains(";" + url + ";") select 1).Take(1).Count() != 0;
            });
            return hasHelper;
        }
    }
}
