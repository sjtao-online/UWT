using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Commons
{
    abstract class PageResultTemplateBasic : IPageResult
    {
        public Controller Controller { get; set; }
        public abstract IActionResult View();
    }
}
