using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UWT.Templates.Models.Basics;
using UWT.Templates.Services.StartupEx;

namespace UWT.Templates.Services.Filters
{
    class ApiCustomFilter : IResultFilter
    {
        static List<string> keys = null;
        static Dictionary<string, string> propKeyMap = null;
        public static JsonOptions JsonOptional { get; internal set; }

        private void FillPropKeyMap(Type type, string name, List<string> list)
        {
            var codeprop = type.GetProperty(name);
            var jpn = codeprop.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jpn != null)
            {
                propKeyMap[name] = jpn.Name;
                keys.Add(jpn.Name);
            }
            else
            {
                foreach (var item in list)
                {
                    if (item.ToLower() == name.ToLower())
                    {
                        propKeyMap[name] = item;
                        keys.Add(item);
                        break;
                    }
                }
            }
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var r = context.Result as ObjectResult;
                var ro = r.Value;
                if (ro is IResultModelBasic && !(ro is ResultModelBasic))
                {
                    if (keys == null)
                    {
                        var rx = ServiceCollectionEx.ApiResultBuildFuncT("", 0, new { });
                        var type = rx.GetType();
                        var rxt = JsonSerializer.Serialize(rx, type, JsonOptional.JsonSerializerOptions);
                        var v = JsonSerializer.Deserialize<Dictionary<string, object>>(rxt);
                        propKeyMap = new Dictionary<string, string>();
                        keys = new List<string>();
                        var list = v.Keys.ToList();
                        FillPropKeyMap(type, nameof(IResultModelBasicT.Code), list);
                        FillPropKeyMap(type, nameof(IResultModelBasicT.Msg), list);
                        FillPropKeyMap(type, nameof(IResultModelBasicT.Data), list);
                    }
                    var map = new Dictionary<string, object>();
                    foreach (var prop in ro.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                    {
                        var cv = prop.GetValue(ro);
                        if (propKeyMap.ContainsKey(prop.Name))
                        {
                            map[propKeyMap[prop.Name]] = cv;
                        }
                        else
                        {
                            var jpn = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                            if (jpn != null)
                            {
                                map[jpn.Name] = cv;
                            }
                            else
                            {
                                map[prop.Name] = cv;
                            }
                        }
                    }
                    map["__vmap__"] = keys;
                    r.Value = map;
                }
            }
        }
    }
}
