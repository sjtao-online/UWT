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
                    Name = "BBS",
                    Desc = "发发发"
                },
                new DescNameIdModel()
                {
                    Id = 5002,
                    Name = "Auth_HasnotTopic",
                    Desc = "暂无权限发贴"
                },
                new DescNameIdModel()
                {
                    Id = 5003,
                    Name = "Auth_HasnotComment",
                    Desc = "暂无权限回贴"
                },
                new DescNameIdModel()
                {
                    Id = 5004,
                    Name = "Auth_HasnotLike",
                    Desc = "暂无权收藏贴"
                },
                new DescNameIdModel()
                {
                    Id = 5005,
                    Name = "Auth_HasnotThumbsUp",
                    Desc = "暂无权点赞"
                }
            };
        }
    }
}
