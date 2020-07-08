using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Normals.News;
using UWT.Templates.Services.Extends;

namespace UWT.Server.Controllers
{
    public class NewsController : NewsController<DataModels.UwtNormalsNews>
    {
        public override IActionResult ModifyProperties(int id)
        {
            return View();
        }
    }
}