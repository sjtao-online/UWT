using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;
using UWT.Templates.Models.Templates.Details;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Models.Templates.FormTrees;
using UWT.Templates.Models.Templates.Lists;

namespace UWT.Templates.Models.Consts
{
    class PageTemplateKeyConst
    {
        public const string TemplateFormKey = "__form_page";
        public const string TemplateDetailKey = "__detail_page";
        public const string TemplateListKey = "__list_page";
        public const string TemplateFormTreeKey = "__form_tree_page";
        public static Dictionary<string, Type> PageTemplateMap = new Dictionary<string, Type>()
        {
            [TemplateDetailKey] = typeof(DetailPageResult),
            [TemplateFormKey] = typeof(FormPageResult),
            [TemplateListKey] = typeof(ListPageResult),
            [TemplateFormTreeKey] = typeof(FormTreeResult)
        };
        public static IPageResult GetPageResult(Controller controller, string key)
        {
            if (PageTemplateMap.ContainsKey(key))
            {
                var type = PageTemplateMap[key];
                var pageResult = type.Assembly.CreateInstance(type.FullName) as PageResultTemplateBasic;
                pageResult.Controller = controller;
                return (IPageResult)pageResult;
            }
            else
            {
                return null;
            }
        }
    }
}
