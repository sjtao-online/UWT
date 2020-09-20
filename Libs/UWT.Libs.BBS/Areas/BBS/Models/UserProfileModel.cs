using System;
using System.Collections.Generic;
using System.Text;
using UWT.Libs.BBS.Areas.Forums.Models.Users;

namespace UWT.Libs.BBS.Areas.BBS.Models
{
    public class UserProfileModel : UserSimpleInfo
    {
        public int TouchCount { get; set; }
    }
}
