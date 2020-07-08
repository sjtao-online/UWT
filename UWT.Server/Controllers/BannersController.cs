using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Basics;
using UWT.Templates.Models.Interfaces;

namespace UWT.Server.Controllers
{
    [Templates.Attributes.Auths.Auth]
    public class BannersController : UWT.Libs.Normals.Banners.BannerBasicController<DataModels.UwtNormalsBanner, BannerCateSimpleSelectItemBuilder, LinkTypeSimpleSelectItemBuilder>
    {
    }
    public class LinkTypeSimpleSelectItemBuilder : SelectItemBuilderBasic
    {
        public override List<NameKeyModel> BuildItemList()
        {
            return new List<NameKeyModel>()
            {
                new NameKeyModel()
                {
                    Key = "None",
                    Name = "不跳转"
                },
                new NameKeyModel()
                {
                    Key = "Mini",
                    Name = "其它小程序"
                },
                new NameKeyModel()
                {
                    Key = "Web",
                    Name = "网页",
                },
                new NameKeyModel()
                {
                    Key = "Page",
                    Name = "小程序页面"
                }
            };
        }
    }
    public class BannerCateSimpleSelectItemBuilder : SelectItemBuilderBasic
    {
        public override List<NameKeyModel> BuildItemList()
        {
            return new List<NameKeyModel>()
            {
                new NameKeyModel()
                {
                    Key = "Banner",
                    Name = "一般"
                },
                new NameKeyModel()
                {
                    Key = "Environment",
                    Name = "环境"
                },
                new NameKeyModel()
                {
                    Key = "Course",
                    Name = "课程"
                }
            };
        }
    }
}