using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Basics;

namespace UWT.Libs.WeChats
{
    class WeChatConst : Templates.Models.Interfaces.IErrorCodeMap
    {
        public const int ErrorCodeBeginCode = 3000;

        public List<DescNameIdModel> EnumErrorCodeMsgList()
        {
            return new List<DescNameIdModel>()
            {
                new DescNameIdModel()
                {
                    Id = 3001,
                    Name = "",
                    Desc = ""
                }
            };
        }
    }
}
