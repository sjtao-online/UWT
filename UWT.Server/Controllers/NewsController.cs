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
        public override string IndexPageTitle => "新闻列表";

        public override string AddPageTitle => "添加";

        public override string ModifyPageTitle => "编辑";

        public override IActionResult ModifyProperties(int id)
        {
            return View();
        }
    }
}