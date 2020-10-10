using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Forms;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Models.Templates.Forms;
using UWT.Templates.Services.Caches;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Services.Extends
{
    /// <summary>
    /// 表单控制器扩展方法
    /// </summary>
    public static class FormPageEx
    {
        /// <summary>
        /// 返回添加/编辑页面
        /// </summary>
        /// <typeparam name="TFormModel"></typeparam>
        /// <param name="page">controller</param>
        /// <param name="model">编辑的对像</param>
        /// <returns></returns>
        public static IPageResult FormResult<TFormModel>(this IFormToPage<TFormModel> page, TFormModel model = null)
            where TFormModel : class
        {
            var controller = page.GetController();
            var formModel = new FormViewModel()
            {
                Item = model,
                FormModel = ModelCache.GetModelFromType(typeof(TFormModel), ModelCache.FormModel)
            };
            controller.ViewBag.FileManagerOptional = ServiceCollectionEx.FileManagerOptional;
            controller.ViewBag.ChooseId2Name = ServiceCollectionEx.ChooseId2Name;
            controller.ViewBag.ChooseIds2Names = ServiceCollectionEx.ChooseIds2Names;
            controller.ViewBag.ModelType = typeof(TFormModel);
            controller.ViewData.Model = formModel;
            return Models.Consts.PageTemplateKeyConst.GetPageResult<FormPageResult>(controller);
        }
        /// <summary>
        /// 检查合法性
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static async Task<bool> CheckCommitModel<TModel>(this IFormToPage<TModel> page, TModel model, List<FormValidModel> ret)
            where TModel : class
        {
            if (model == null)
            {
                using (StreamReader sr = new StreamReader(page.GetController().Request.Body))
                {
                    sr.BaseStream.Position = 0;
                    string bodyText = await sr.ReadToEndAsync();
                    try
                    {
                        //  反序列化 观察出错信息
                        System.Text.Json.JsonSerializer.Deserialize<TModel>(bodyText);
                    }
                    catch (System.Text.Json.JsonException ex)
                    {
                        ret.Add(new FormValidModel()
                        {
                            PropertyName = ex.Path,
                            ErrorMsg = ex.Message
                        });
                    }
                }
                return false;
            }
            var f = ModelCache.GetModelFromType(typeof(TModel), ModelCache.FormModel);
            foreach (var item in f.FormItems)
            {
                var value = item.PropertyInfo.GetValue(model);
                var propName = item.PropertyInfo.Name;
                switch (item.ItemType)
                {
                    case FormItemType.Hidden:

                        break;
                    case FormItemType.Text:
                        string textValue = value as string;
                        //  必存在
                        if (item.IsRequired)
                        {
                            if (string.IsNullOrEmpty(textValue))
                            {
                                ret.Add(new FormValidModel()
                                {
                                    PropertyName = propName,
                                    ErrorMsg = "必填项"
                                });
                                continue;
                            }
                        }
                        var textEx = item.ModelEx as IFormTextEx;
                        //  最小长度
                        if (textEx.MinLength > textValue.Length)
                        {
                            ret.Add(new FormValidModel()
                            {
                                PropertyName = propName,
                                ErrorMsg = $"长度不可低于{textEx.MinLength}"
                            });
                            continue;
                        }
                        //  最大长度
                        if (textEx.MaxLength != 0 && textEx.MaxLength < textValue.Length)
                        {
                            ret.Add(new FormValidModel()
                            {
                                PropertyName = propName,
                                ErrorMsg = $"长度不可高于{textEx.MaxLength}"
                            });
                            continue;
                        }
                        if (!string.IsNullOrEmpty(textEx.Regex) && !(new Regex(textEx.Regex).IsMatch(textValue)))
                        {
                            ret.Add(new FormValidModel()
                            {
                                PropertyName = propName,
                                ErrorMsg = "不符合正则规则"
                            });
                            continue;
                        }
                        break;
                    case FormItemType.Integer:
                    case FormItemType.Float:
                    case FormItemType.Money:
                    case FormItemType.Date:
                    case FormItemType.DateTime:
                    case FormItemType.TimeSpan:
                        break;
                    case FormItemType.Password:
                        break;
                    case FormItemType.File:
                        break;
                    case FormItemType.ChooseId:
                        break;
                    case FormItemType.Color:
                        break;
                    case FormItemType.Time:
                        break;
                    case FormItemType.SimpleSelect:
                        break;
                    case FormItemType.Slider:
                        break;
                    case FormItemType.PartCshtml:
                        break;
                    //  不参与计算
                    case FormItemType.ShowImage:
                    case FormItemType.ShowText:
                        break;
                    default:
                        break;
                }
            }
            return ret.Count == 0;
        }
    }
}
