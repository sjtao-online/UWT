using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Details;
using UWT.Templates.Services.Caches;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 详情扩展
    /// </summary>
    public static class DetailPageEx
    {
        /// <summary>
        /// 详情扩展
        /// </summary>
        /// <typeparam name="TDetailModel">详情模型</typeparam>
        /// <param name="detail">详情控制器</param>
        /// <param name="model">详情对象</param>
        /// <returns></returns>
        public static IPageResult DetailResult<TDetailModel>(this IDetailToPage<TDetailModel> detail, TDetailModel model)
        {
            var controller = detail.GetController();
            controller.ViewData.Model = new DetailViewModel()
            {
                Detail = model,
                DetailModel = ModelCache.GetModelFromType(typeof(TDetailModel), ModelCache.DetailModel)
            };
            controller.ViewBag.ModelType = typeof(TDetailModel);
            return Models.Consts.PageTemplateKeyConst.GetPageResult<DetailPageResult>(controller);
        }
    }
}
