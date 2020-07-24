using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Filters;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Lists
{
    class FilterModel : IFilterModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public FilterType FilterType { get; set; }
        public string Value { get; set; }
        public FilterValueType ValueType { get; set; }
        public List<HasFilterTypeChildrenNameKeyModel> CanSelectList { get; set; }
        public object Tag { get; set; }
    }
    class FilterModelFromCshtml : IFilterModelFromCshtml
    {
        public string ViewPath { get; set; }
        public string SearchCallback { get; set; }
        public string ResetCallback { get; set; }
        public string InitCallback { get; set; }
        public string LastValue { get; set; }
        public object Tag { get; set; }
    }
}
