using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Basics
{
	/// <summary>
	/// 带Id的模型
	/// </summary>
	public class IdModel
	{
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }
	}
	/// <summary>
	/// 带名称Id的模型
	/// </summary>
	public class NameIdModel : IdModel
	{
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 带备注名称Id的模型
	/// </summary>
	public class DescNameIdModel : NameIdModel
	{
		/// <summary>
		/// 备注
		/// </summary>
		public string Desc { get; set; }
	}
	/// <summary>
	/// 带图标名称Id的模型
	/// </summary>
	public class IconNameIdModel : NameIdModel
	{
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; set; }
	}
	/// <summary>
	/// 带标题Id的模型
	/// </summary>
	public class TitleIdModel : IdModel
	{
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
	}
	/// <summary>
	/// 带图标标题Id的模型
	/// </summary>
	public class IconTitleIdModel : TitleIdModel
	{
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; set; }
	}
	/// <summary>
	/// 带URL标题Id的模型
	/// </summary>
	public class UrlTitleIdModel : TitleIdModel
	{
		/// <summary>
		/// URL
		/// </summary>
		public string Url { get; set; }
	}
	/// <summary>
	/// 带URL图标标题Id的模型
	/// </summary>
	public class IconUrlTitleIdModel : UrlTitleIdModel
	{
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; set; }
	}
	/// <summary>
	/// KeyName模型
	/// </summary>
	public class NameKeyModel
	{
		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// KeyNameChildren模型
	/// </summary>
	public class HasChildrenNameKeyModel : NameKeyModel
	{
		/// <summary>
		/// Children
		/// </summary>
		public List<HasChildrenNameKeyModel> Children { get; set; }
	}
	/// <summary>
	/// 有标题Id树型模型
	/// </summary>
	public class HasChildrenTitleIdModel : TitleIdModel
	{
		/// <summary>
		/// Children
		/// </summary>
		public List<HasChildrenTitleIdModel> Children { get; set; }
	}
	/// <summary>
	/// 值-类型模型
	/// </summary>
    public class ValueTypeModel
    {
		/// <summary>
		/// 类型
		/// </summary>
        public string Type { get; set; }
		/// <summary>
		/// 值
		/// </summary>
        public string Value { get; set; }
    }
}
