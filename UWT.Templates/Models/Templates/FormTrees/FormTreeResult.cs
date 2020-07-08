using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.FormTrees
{
    class FormTreeResult : PageResultTemplateBasic
    {
        public override IActionResult View()
        {
            return Controller.View("/Views/Templates/FormTreePage.cshtml");
        }
    }
}
