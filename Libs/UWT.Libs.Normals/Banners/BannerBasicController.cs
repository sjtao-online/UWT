using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Services.Extends;
using UWT.Templates.Models.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UWT.Libs.Normals.Banners
{
    /// <summary>
    /// Banner基础类
    /// 本类需要常量字典(BannerController)
    /// </summary>
    /// <typeparam name="TDbBannerModel">数据库模型</typeparam>
    /// <typeparam name="TBannerCateSimpleSelectBuilder">Banner类型单项选项</typeparam>
    /// <typeparam name="TLinkTypeSimpleSelectBuilder">链接类型单项选择</typeparam>
    public class BannerBasicController<TDbBannerModel, TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder> : Controller
        , IListToPage<TDbBannerModel, BannerListItemModel>
        , IListToPageConfig
        , IFormToPage<BannerAddModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>
        , IFormToPage<BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>
        , IDetailToPage<BannerDetailModel>
        where TDbBannerModel : class, IDbBannerTable, new()
        where TBannerCateSimpleSelectBuilder : ISelectItemBuilder
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public int DefaultPageSize => 10;

        public virtual IActionResult Index()
        {
            this.ActionLog();
            this.AddHandler("添加", ".Add");
            return this.ListResult(m => new BannerListItemModel()
            {
                Id = m.Id,
                Title = m.Title,
                Desc = m.Desc,
                ImageUrl = m.Image
            }).View();
        }

        public virtual IActionResult Add()
        {
            this.ActionLog();
            return this.FormResult<BannerAddModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody]BannerAddModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder> model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<BannerAddModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbBannerModel>();
                table.Insert(() => new TDbBannerModel()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Image = model.ImageUrl,
                    Index = model.Index,
                    Target = model.TargetUrl,
                    TargetType = model.Type,
                    
                });
            });
            return this.Success();
        }

        public virtual IActionResult Modify(int id)
        {
            this.ActionLog();
            BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder> modify = null;
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbBannerModel>();
                var q = from it in table
                        where it.Id == id && it.Valid
                        select new BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>
                        {
                            Id = it.Id,
                            Title = it.Title,
                            SubTitle = it.SubTitle,
                            ImageUrl = it.Image,
                            Index = it.Index,
                            Cate = it.Cate,
                            Type = it.TargetType
                        };
                if (q.Count() != 0)
                {
                    modify = q.First();
                }
            });
            if (modify == null)
            {
                return this.ItemNotFound();
            }
            return this.FormResult<BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>(modify).View();
        }

        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody]BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder> model)
        {
            this.ActionLog();
            List<FormValidModel> ret = new List<FormValidModel>();
            if (!await this.CheckCommitModel<BannerModifyModel<TBannerCateSimpleSelectBuilder, TLinkTypeSimpleSelectBuilder>>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbBannerModel>();
                table.Update(m => m.Id == model.Id, m => new TDbBannerModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Image = model.ImageUrl,
                    SubTitle = model.SubTitle,
                    Index = model.Index,
                    TargetType = model.Type,
                    Cate = model.Cate
                });
            });
            return this.Success();
        }
        [HttpPost]
        public virtual object Del(int id)
        {
            this.ActionLog();
            bool notfound = false;
            this.UsingDb(db =>
            {
                var bannerTable = db.GetTable<TDbBannerModel>();
                var o = from it in bannerTable where it.Id == id && it.Valid select 1;
                if (o.Count() == 0)
                {
                    notfound = true;
                    return;
                }
                bannerTable.Update(m => m.Id == id, m => new TDbBannerModel()
                {
                    Valid = false
                });
            });
            return notfound ? this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound) : this.Success();
        }
        public IActionResult Detail(int id)
        {
            this.ActionLog();
            BannerDetailModel detail = null;
            this.UsingDb(db =>
            {
                var table = db.GetTable<TDbBannerModel>();
                var q = from it in table
                        where it.Id == id && it.Valid
                        select new BannerDetailModel()
                        {
                            Title = it.Title,
                            SubTitle = it.SubTitle,
                            Image = it.Image,
                            Index = it.Index,
                            Desc = it.Desc,
                            // Cate = it.Cate,
                            // Type = it.TargetType
                        };
                if (q.Count() != 0)
                {
                    detail = q.First();
                }
            });
            if (detail == null)
            {
                return this.ItemNotFound();
            }
            return this.DetailResult<BannerDetailModel>(detail).View();
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
