using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.Forums.Models;
using UWT.Libs.BBS.Areas.Forums.Services;
using UWT.Libs.BBS.Models;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.Forums.Controllers
{
    [ForumRoute, AuthForum]
    public class AreasController : Controller
        , ITemplateController
    {
        [HttpPost]
        public virtual object GetHomeAreaTree()
        {
            return this.Success(new AreaService().GetHomeAreaList());
        }
    }
}
