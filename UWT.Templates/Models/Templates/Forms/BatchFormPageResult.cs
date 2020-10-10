using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.Forms
{
    class BatchFormPageResult : PageResultTemplateBasic
    {
        public override IActionResult View()
        {
            return Controller.View("/Views/Templates/BatchFormPage.cshtml");
        }
    }
}
