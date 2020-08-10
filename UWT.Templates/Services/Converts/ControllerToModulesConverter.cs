using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Services.Converts
{
    /// <summary>
    /// 控制器转换URL
    /// </summary>
    public class ControllerToModulesConverter
    {
        static Dictionary<string, string> _defaultMethodShowNameDictionary = null;
        const string CEditText = "编辑";
        const string CAddText = "添加";
        const string CListText = "列表";
        static Dictionary<string, string> DefaultMethodShowNameDictionary
        {
            get
            {
                if (_defaultMethodShowNameDictionary == null)
                {
                    _defaultMethodShowNameDictionary = new Dictionary<string, string>()
                    {
                        ["add"] = CAddText,
                        ["addmodel"] = CAddText,
                        ["modify"] = CEditText,
                        ["modifymodel"] = CEditText,
                        ["del"] = "删除",
                        ["detail"] = "详情",
                        ["publish"] = "发布",
                        ["publishremove"] = "撤下",
                        ["index"] = CListText,
                        ["list"] = CListText,
                    };
                }
                return _defaultMethodShowNameDictionary;
            }
        }
        /// <summary>
        /// 计算结果
        /// </summary>
        /// <returns></returns>
        public static List<ModuleModel> CalcAllModules()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            List<Assembly> calcAssemblies = new List<Assembly>();
            //  主程序集
            calcAssemblies.Add(Assembly.GetEntryAssembly());
            //  当前程序集
            calcAssemblies.Add(Assembly.GetExecutingAssembly());
            foreach (var item in assemblys)
            {
                if (item.FullName.ToLower().StartsWith("uwt.libs"))
                {
                    //  uwt附加库程集
                    calcAssemblies.Add(item);
                }
            }
            const string controllerTemplateText = "[controller]";
            const string actionTemplateText = "[action]";
            const string areaTemplateText = "[area]";
            const string urlSplitText = "/";
            List<ModuleModel> modules = new List<ModuleModel>();
            var controllerbasetype = typeof(ControllerBase);
            var controllertype = typeof(Controller);
            var objecttype = typeof(object);
            foreach (Assembly ass in calcAssemblies)
            {
                var assName = ass.GetName().Name;
                foreach (var type in ass.GetExportedTypes())
                {
                    if (type.IsSubclassOf(controllerbasetype))
                    {
                        if (type.IsGenericType || type.IsAbstract)
                        {
                            continue;
                        }
                        string controllername = type.Name;
                        string areaName = null;
                        const string controllerNameConst = "controller";
                        if (controllername.ToLower().EndsWith(controllerNameConst))
                        {
                            controllername = controllername.Substring(0, controllername.Length - controllerNameConst.Length);
                        }
                        Dictionary<string, string> ttMapBasic = new Dictionary<string, string>();
                        var nr = type.GetCustomAttribute<UwtNoRecordModuleAttribute>();
                        if (nr != null)
                        {
                            continue;
                        }
                        var uwtroute = type.GetCustomAttribute<UwtRouteAttribute>();
                        var area = type.GetCustomAttribute<AreaAttribute>();
                        if (area != null)
                        {
                            areaName = area.RouteValue;
                        }
                        if (uwtroute != null && !string.IsNullOrEmpty(uwtroute.ShowName))
                        {
                            ttMapBasic.Add("AreaShowName", uwtroute.ShowName);
                        }
                        var cShowName = type.GetCustomAttribute<UwtControllerNameAttribute>();
                        if (cShowName != null && cShowName.ShowName != null)
                        {
                            ttMapBasic.Add("ControllerShowName", cShowName.ShowName);
                        }
                        var route = type.GetCustomAttribute<RouteAttribute>();
                        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (method.DeclaringType == controllertype || method.DeclaringType == controllerbasetype || method.DeclaringType == objecttype)
                            {
                                continue;
                            }
                            if (method.Name.StartsWith("set_") || method.Name.StartsWith("get_"))
                            {
                                continue;
                            }
                            var noaction = method.GetCustomAttribute<NonActionAttribute>();
                            if (noaction != null)
                            {
                                continue;
                            }
                            var mnr = method.GetCustomAttribute<UwtNoRecordModuleAttribute>();
                            if (mnr != null)
                            {
                                continue;
                            }
                            var mroute = method.GetCustomAttribute<RouteAttribute>();
                            string url;
                            if (uwtroute != null)
                            {
                                url = uwtroute.Template.Replace(areaTemplateText, uwtroute.RouteValue).Replace(controllerTemplateText, controllername).Replace(actionTemplateText, method.Name);
                            }
                            else if (route == null && mroute == null)
                            {
                                url = urlSplitText + controllername + urlSplitText + method.Name;
                            }
                            else
                            {
                                if (route != null && mroute != null)
                                {
                                    url = route.Template.Replace(areaTemplateText, areaName).Replace(controllerTemplateText, controllername).Replace(actionTemplateText, method.Name) + urlSplitText
                                        + mroute.Template.Replace(areaTemplateText, areaName).Replace(controllerTemplateText, controllername).Replace(actionTemplateText, method.Name);
                                }
                                else if (route != null)
                                {
                                    url = route.Template.Replace(areaTemplateText, areaName).Replace(controllerTemplateText, controllername).Replace(actionTemplateText, method.Name);
                                }
                                else
                                {
                                    url = mroute.Template.Replace(areaTemplateText, areaName).Replace(controllerTemplateText, controllername).Replace(actionTemplateText, method.Name);
                                }
                            }
                            if (!url.StartsWith(urlSplitText))
                            {
                                url = urlSplitText + url;
                            }
                            if (url.Contains("${"))
                            {
                                url = url.RCalcText(assName);
                            }
                            if (url.Contains("{"))
                            {
                                continue;
                            }
                            string moduleShowName = url;
                            var mShowName = method.GetCustomAttribute<UwtMethodAttribute>();
                            if (mShowName != null && !string.IsNullOrEmpty(mShowName.ShowName))
                            {
                                ttMapBasic["MethodShowName"] = mShowName.ShowName;
                                StringTemplateConverter<string> stringTemplateConverter = new StringTemplateConverter<string>(ttMapBasic);
                                moduleShowName = stringTemplateConverter.ReplacePlaceholder(mShowName.TemplateText);
                            }
                            else if (cShowName != null && DefaultMethodShowNameDictionary.ContainsKey(method.Name.ToLower()))
                            {
                                ttMapBasic["MethodShowName"] = DefaultMethodShowNameDictionary[method.Name.ToLower()];
                                StringTemplateConverter<string> stringTemplateConverter = new StringTemplateConverter<string>(ttMapBasic);
                                moduleShowName = stringTemplateConverter.ReplacePlaceholder(UwtMethodAttribute.TemplateText_AreaControllerMethod);
                            }
                            if (typeof(IActionResult) == method.ReturnType || typeof(IActionResult).IsInstanceOfType(method.ReturnType) || method.ReturnType == typeof(Task<IActionResult>))
                            {
                                modules.Add(new ModuleModel()
                                {
                                    Category = ModuleCategory.Page,
                                    Url = url,
                                    ShowName = moduleShowName
                                });
                            }
                            else
                            {
                                modules.Add(new ModuleModel()
                                {
                                    Category = ModuleCategory.API,
                                    Url = url,
                                    ShowName = moduleShowName
                                });
                            }
                        }
                    }
                }
            }
            return modules;
        }
    }
    /// <summary>
    /// 模块模型
    /// </summary>
    public class ModuleModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ModuleCategory Category { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string ShowName { get; set; }
    }
    /// <summary>
    /// 类型
    /// </summary>
    public enum ModuleCategory
    {
        /// <summary>
        /// 页面
        /// </summary>
        Page,
        /// <summary>
        /// 接口
        /// </summary>
        API
    }
}
