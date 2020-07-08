using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Templates.Models.Consts
{
    /// <summary>
    /// 常用文件类型
    /// </summary>
    public class FileTypeConst
    {
        /// <summary>
        /// 所有图片<br/>默认最大1MB
        /// </summary>
        public const string Image = ".jpg,.png,.bmp,jpeg,.gif,.svg,.tif";
        /// <summary>
        /// 所有音频<br/>默认最大10MB
        /// </summary>
        public const string Audio = ".mp3,.wma,.wav,.acc,.midi,.ogg,.ape,.flac";
        /// <summary>
        /// 所有视频<br/>默认最大1GB
        /// </summary>
        public const string Video = ".mp4,.avi,.wmv,.asf,.flv,.3gp,.rm,.rmvb,.mov";
        /// <summary>
        /// 压缩包<br/>默认最大20MB
        /// </summary>
        public const string Zip = ".zip,.7z,.rar,.iso,.bz2,.gzip";
        /// <summary>
        /// PDF<br/>默认最大2MB
        /// </summary>
        public const string Pdf = ".pdf";
        /// <summary>
        /// WORD<br/>默认最大2MB
        /// </summary>
        public const string Word = ".doc,.docx";
        /// <summary>
        /// EXCEL<br/>默认最大2MB
        /// </summary>
        public const string Excel = ".xls,.xlsx";
        /// <summary>
        /// PPT<br/>默认最大2MB
        /// </summary>
        public const string PowerPoint = ".ppt,.pptx";
        /// <summary>
        /// OffsetFile<br/>默认最大2MB
        /// </summary>
        public const string OfficeFiles = ".ppt,.pptx,.xls,.xlsx,.doc,.docx";
        /// <summary>
        /// JS文件<br/>默认最大1MB
        /// </summary>
        public const string JavaScript = ".js,.jsx";
        /// <summary>
        /// HTML文件<br/>默认最大2MB
        /// </summary>
        public const string Html = ".html,.htm,.mhtml";

    }
}
