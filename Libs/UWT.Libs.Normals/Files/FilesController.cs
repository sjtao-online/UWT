using LinqToDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UWT.Templates.Models.Interfaces;
using UWT.Templates.Services.Extends;
using UWT.Templates.Services.Storages;

namespace UWT.Libs.Normals.Files
{
    /// <summary>
    /// 文件控制器
    /// </summary>
    /// <typeparam name="TFileTableModel"></typeparam>
    public class FilesController<TFileTableModel> : Controller
        , ITemplateController, IListToPage<TFileTableModel, FileModel>
        where TFileTableModel : class, IDbFileTable, new()
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        [HttpPost]
        [Produces("application/json")]
        public virtual object List(string filename, string filetype)
        {
            this.ActionLog();
            if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(filetype))
            {
                return this.ListObjectResult(m => new FileModel()
                {
                    Id = m.Id,
                    Size = m.FileSize,
                    Src = m.Path,
                    Title = m.Filename,
                    Type = m.Type
                }, m=>m.Filename.Contains(filename) && m.Type == filetype);
            }
            else if (!string.IsNullOrEmpty(filename))
            {
                return this.ListObjectResult(m => new FileModel()
                {
                    Id = m.Id,
                    Size = m.FileSize,
                    Src = m.Path,
                    Title = m.Filename,
                    Type = m.Type
                }, m => m.Filename.Contains(filename));
            }
            else if (!string.IsNullOrEmpty(filetype))
            {
                return this.ListObjectResult(m => new FileModel()
                {
                    Id = m.Id,
                    Size = m.FileSize,
                    Src = m.Path,
                    Title = m.Filename,
                    Type = m.Type
                }, m => m.Type == filetype);
            }
            else
            {
                return this.ListObjectResult(m => new FileModel()
                {
                    Id = m.Id,
                    Size = m.FileSize,
                    Src = m.Path,
                    Title = m.Filename,
                    Type = m.Type
                });
            }
        }
        [HttpPost]
        [Produces("application/json")]
        [DisableRequestSizeLimit]
        public virtual object Upload(IFormFile file, string desc)
        {
            this.ActionLog();
            string path = FileStorage.SaveFile(file, "Uploads");
            if (path == null)
            {
                return this.Error(Templates.Models.Basics.ErrorCode.UploadNotFile);
            }
            int id = 0;
            this.UsingDb(db =>
            {
                id = db.GetTable<TFileTableModel>().InsertWithInt32Identity(() => new TFileTableModel()
                {
                    Path = path,
                    Filename = file.FileName,
                    AddAccountId = this.GetClaimValue("AccountId", 0),
                    FileSize = file.Length,
                    Desc = desc,
                    Type = file.ContentType
                });
            });
            return this.Success(new FileModel(){ Src = path, Size = file.Length, Title = file.FileName, Id = id});
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
    class FileModel
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }
    /// <summary>
    /// 文件数据库模型
    /// </summary>
    public interface IDbFileTable : IDbTableBase
    {
        /// <summary>
        /// 下载路径
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Filename { get; set; }
        /// <summary>
        /// 添加账号Id
        /// </summary>
        int AddAccountId { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        long FileSize { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Desc { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        string Type { get; set; }
    }
}
