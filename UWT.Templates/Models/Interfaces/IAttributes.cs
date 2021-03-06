﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Interfaces
{
    /// <summary>
    /// 不收录Module数据库(接口)
    /// 不参与权限相关的不用收录
    /// </summary>
    public interface IUwtNoRecordModule { }
    /// <summary>
    /// 显示名接口<br/>
    /// 一般用于UwtControllerName，UwtMethod之类的特性
    /// </summary>
    public interface IUwtShowName
    {
        /// <summary>
        /// 显示名
        /// </summary>
        string ShowName { get; }
    }
    /// <summary>
    /// 带模板的显示名接口<br/>
    /// 暂只用于UwtMethod特性
    /// </summary>
    public interface IUwtShowNameHasTemplate : IUwtShowName
    {
        /// <summary>
        /// 模板字符串<br/>
        /// 默认值为UwtMethodAttribute.TemplateText_AreaControllerMethod
        /// </summary>
        string TemplateText { get; }
    }
}
