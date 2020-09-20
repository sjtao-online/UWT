using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.Forums.Services;
using UWT.Libs.BBS.Models;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.Forums.Controllers
{
    [UwtRoute("Forums")]
    public class UserController : Controller
        , ITemplateController
    {
        public object Info(int id)
        {
            var info = new UserService().Find(id);
            if (info == null)
            {
                return this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound);
            }
            return this.Success(info);
        }

    }
}
