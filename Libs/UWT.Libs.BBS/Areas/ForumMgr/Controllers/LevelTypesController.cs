using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using UWT.Libs.BBS.Areas.ForumMgr.Models;
using UWT.Libs.BBS.Areas.ForumMgr.Models.LevelTypes;
using UWT.Libs.BBS.Models;
using UWT.Templates.Attributes.Routes;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;

namespace UWT.Libs.BBS.Areas.ForumMgr.Controllers
{
    [ForumAreaRoute]
    [UwtControllerName("等级类型")]
    public class LevelTypesController : Controller
        , IListToPage<UWT.Libs.BBS.Models.UwtBbsUserLevelType, Models.LevelTypes.LevelTypeListItemModel>
        , IFormToPage<Models.LevelTypes.LevelTypeAddModel>
        , IFormToPage<Models.LevelTypes.LevelTypeModifyModel>
    {
        public IActionResult Index()
        {
            this.AddHandler("添加", ".Add");
            this.AddFilter("名称", m => m.Name, Templates.Models.Filters.FilterType.Like, Templates.Models.Filters.FilterValueType.Text);
            return this.ListResult(m => new Models.LevelTypes.LevelTypeListItemModel()
            {
                Id = m.Id,
                Name = m.Name,
            }, orderbydesc: m => m.Id, where: m => m.Valid).View();
        }

        public virtual IActionResult Add()
        {
            return this.FormResult<LevelTypeAddModel>().View();
        }

        [HttpPost]
        public virtual async Task<object> AddModel([FromBody] LevelTypeAddModel model)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel<LevelTypeAddModel>(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var table = db.TableUserLevelType();
                table.Insert(() => new UwtBbsUserLevelType()
                {
                    Name = model.Name
                });
            }
            return this.Success();
        }

        public virtual IActionResult Modify(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.TableUserLevelType();
                var q = (from it in table
                         where it.Id == id && it.Valid
                         select new Models.LevelTypes.LevelTypeModifyModel
                         {
                             Id = it.Id,
                             Name = it.Name
                         }).Take(1);
                if (q.Count() == 0)
                {
                    return this.ItemNotFound();
                }
                return this.FormResult(q.First()).View();
            }
        }

        [HttpPost]
        public virtual async Task<object> ModifyModel([FromBody] Models.LevelTypes.LevelTypeModifyModel model)
        {
            List<Templates.Models.Templates.Forms.FormValidModel> ret = new List<Templates.Models.Templates.Forms.FormValidModel>();
            if (!await this.CheckCommitModel(model, ret))
            {
                return this.Error(Templates.Models.Basics.ErrorCode.FormCheckError, ret);
            }
            using (var db = this.GetDB())
            {
                var table = db.TableUserLevelType();
                table.Update(m => m.Id == model.Id, m => new UWT.Libs.BBS.Models.UwtBbsUserLevelType()
                {
                    Name = model.Name
                });
            }
            return this.Success();
        }


        [HttpPost]
        public virtual object Del(int id)
        {
            using (var db = this.GetDB())
            {
                var table = db.UwtGetTable<UwtBbsUserLevelType>();
                var o = (from it in table where it.Id == id select 1).Take(1);
                if (o.Count() == 0)
                {
                    return this.Error(Templates.Models.Basics.ErrorCode.Item_NotFound);
                }
                table.Update(m => m.Id == id, m => new UwtBbsUserLevelType()
                {
                    Valid = false
                });
            }
            return this.Success();
        }

    }
}
