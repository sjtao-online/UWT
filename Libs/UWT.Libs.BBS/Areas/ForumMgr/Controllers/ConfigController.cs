using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.ForumMgr.Models;
using UWT.Libs.BBS.Areas.ForumMgr.Models.Config;
using UWT.Libs.BBS.Models;
using UWT.Libs.Users;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [ForumAreaRoute(null, "/ForumMgr")]
    [AuthUser]
    public class ConfigController : Controller
        , IFormToPage<ConfigModel>
    {
        static ConfigModel _config = null;
        public static ConfigModel CurrentConfigModel
        {
            get
            {
                if (_config == null)
                {
                    using (var db = TemplateControllerEx.GetDB(null))
                    {
                        var query = from it in db.TableConfig() select it;
                        Dictionary<string, string> dic = query.ToDictionary(k => k.Key, v => v.Value);
                        ConfigModel t = new ConfigModel()
                        {
                            Name = TryValue(dic, "name", "UWT论坛"),
                            Logo = TryValue(dic, "logo", "/bbs/logo.png"),
                        };
                    }
                }
                return _config;
            }
        }
        static string TryValue(Dictionary<string, string> dic, string key, string defaultValue)
        {
            if (!dic.ContainsKey(key))
            {
                return defaultValue;
            }
            return dic[key];
        }
        [UwtMethod("属性配置")]
        public IActionResult Index()
        {
            return this.FormResult(CurrentConfigModel).View();
        }
        [HttpPost]
        [Route("/ForumMgr/Config/Commit")]
        [UwtMethod("修改配置")]
        public virtual object Commit([FromBody]ConfigModel model)
        {

            return this.Success();
        }
    }
}
