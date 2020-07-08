using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Commons;

namespace UWT.Templates.Models.Templates.Lists
{
    class ListViewModel : ViewModelBasic, IListViewModel
    {
        public List<IListColumnModel> Columns { get; set; }
        public bool HasMutilCheck => BatchKey != null;
        public PropertyInfo BatchProperty { get; set; }
        public string BatchKey { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }
}
