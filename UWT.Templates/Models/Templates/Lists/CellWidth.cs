using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Models.Interfaces;

namespace UWT.Templates.Models.Templates.Lists
{
    class CellWidth : ICellWidth
    {
        public CellWidthUnitType UnitType { get; set; }
        public bool IsAuto => UnitType == CellWidthUnitType.Auto;
        public bool IsStar => UnitType == CellWidthUnitType.Star;
        public bool IsAbsolute => UnitType == CellWidthUnitType.Pixel;
        public double Value { get; set; }
        public double MinWidth { get; set; }
        public double? MaxWidth { get; set; }
    }
    enum CellWidthUnitType
    {
        /// <summary>
        /// 该大小由内容对象的大小属性确定。
        /// </summary>
        Auto = 0,
        /// <summary>
        /// 该值表示为一个像素。
        /// </summary>
        Pixel = 1,
        /// <summary>
        /// 该值表示为可用空间的加权比例。
        /// </summary>
        Star = 2
    }
}
