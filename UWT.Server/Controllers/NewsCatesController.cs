using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.Normals.News;

namespace UWT.Server.Controllers
{
    public class NewsCatesController : NewsCatesController<DataModels.UwtNormalsNewsCate>
    {
        public override string IndexPageTitle => "列表";

        public override string AddPageTitle => "添加";

        public override string ModifyPageTitle => "编辑";
    }
}