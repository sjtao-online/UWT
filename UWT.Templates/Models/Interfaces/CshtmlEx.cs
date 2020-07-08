using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    interface IPartCshtmlAttribute
    {
        string PartPath { get; }
        string AppendCSS { get; }
        string AppendJS { get; }
    }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public interface ICshtmlEx
    {
        string CshtmlPath { get; }
        List<string> JsList { get; }
        List<string> CssList { get; }
    }
    public interface IDetailCshtmlEx : IDetailItemExBasicModel, ICshtmlEx
    {
    }
    public interface IFormCshtmlEx : ICshtmlEx, IFormItemExBasicModel
    {
        string CallBackFunc { get; }
    }
    public interface IListCshtmlEx : ICshtmlEx , IListItemExBasicModel
    {

    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
