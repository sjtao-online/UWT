using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.BBS
{
    class BBSConst : Templates.Models.Interfaces.IErrorCodeMap
    {
        public const int ErrorCodeBeginCode = 5000;

        public List<DescNameIdModel> EnumErrorCodeMsgList()
        {
            return new List<DescNameIdModel>()
            {
                new DescNameIdModel()
                {
                    Id = 5001,
                    Name = "Fafafa",
                    Desc = "发发发"
                }
            };
        }
    }
}
