using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.Forms
{
    class FormPageResult : PageResultTemplateBasic
    {
        public override IActionResult View()
        {
            return Controller.View("/Views/Templates/FormPage.cshtml");
        }
    }
}
