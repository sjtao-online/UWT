using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWT.Templates.Attributes.Details;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Details
{
    /// <summary>
    /// 详情页面模型<br/>
    /// [内部使用]
    /// </summary>
    class DetailViewModel : IDetailViewModel
    {
        /// <summary>
        /// 详情对象
        /// </summary>
        public object Detail { get; set; }
        /// <summary>
        /// 属性模型
        /// </summary>
        public IDetailModel DetailModel { get; set; }
    }
    /// <summary>
    /// 属性模型
    /// </summary>
    class DetailItemModel : IDetailItemModel
    {
        /// <summary>
        /// 显示类型
        /// </summary>
        public DetailItemCategory Cate { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 属性<br/>
        /// 用于获得属性值
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public IDetailItemExBasicModel ModelEx { get; set; }
    }
    /// <summary>
    /// 详情数据模型
    /// </summary>
    class DetailModel : IDetailModel
    {
        /// <summary>
        /// 每个属性的模型
        /// </summary>
        public List<IDetailItemModel> ItemModels { get; set; }
    }
}
