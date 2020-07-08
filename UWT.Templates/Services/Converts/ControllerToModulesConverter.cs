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
                        var uwtroute = type.GetCustomAttribute<UwtRouteAttribute>();
                        var area = type.GetCustomAttribute<AreaAttribute>();
                        if (area != null)
                        {
                            areaName = area.RouteValue;
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
                            if (typeof(IActionResult) == method.ReturnType || typeof(IActionResult).IsInstanceOfType(method.ReturnType) || method.ReturnType == typeof(Task<IActionResult>))
                            {
                                modules.Add(new ModuleModel()
                                {
                                    Category = ModuleCategory.Page,
                                    Url = url
                                });
                            }
                            else
                            {
                                modules.Add(new ModuleModel()
                                {
                                    Category = ModuleCategory.API,
                                    Url = url
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
