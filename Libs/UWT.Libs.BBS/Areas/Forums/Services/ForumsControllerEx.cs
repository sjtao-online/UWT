using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWT.Templates.Models.Consts;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    static class ForumsControllerEx
    {
        public static int GetCurrentUserId(this Controller controller)
        {
            return controller.GetClaimValue(AuthConst.AccountIdKey, 0);
        }
    }
}
