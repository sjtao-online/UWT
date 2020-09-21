using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS.Areas.Forums.Models.Users
{
    public class UserPropertiesModel
    {
        public string GroupName { get; set; }
        public Dictionary<int, string> TitleMap { get; set; }
        public List<TitleIdModel> Children { get; set; }
    }
}
