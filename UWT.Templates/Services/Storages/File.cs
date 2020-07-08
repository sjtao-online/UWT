using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UWT.Templates.Services.Extends;

namespace UWT.Templates.Services.Storages
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public class FileStorage
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="type">类型</param>
        /// <returns>存储相对目录</returns>
        public static string SaveFile(IFormFile file, string type)
        {
            if (file == null)
            {
                return null;
            }
            string wr = type.GetCurrentWebHost().WebRootPath;
            var now = DateTime.Now;
            string dir = System.IO.Path.Combine(wr, type, now.ToShowDateText(), now.ToString("HH"));
            var rand = new Random((int)now.Ticks);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            string filePath = System.IO.Path.Combine(dir, file.FileName);
        RemakeFileName:
            if (System.IO.File.Exists(filePath))
            {
                filePath = System.IO.Path.Combine(dir, now.ToString("mmssfff") + "-" + rand.Next(1000, 10000), file.FileName);
                goto RemakeFileName;
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
            }
            using (System.IO.FileStream stream = System.IO.File.OpenWrite(filePath))
            {
                file.CopyTo(stream);
            }
            return filePath.Substring(wr.Length).Replace('\\', '/');
        }
    }
}
