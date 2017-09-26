using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JDI.Utility.Common
{
    public class Upload
    {
        private string filePath;//上传文件路径
        private string fileType;//上传文件类型
        public Upload()
        {
            filePath = "UploadFiles/Images";
            fileType = "GIF|JPEG|JPG|PNG|BMP|RAR|DOC|XLS";
        }
        public string SaveFile(HttpPostedFileBase _postedFile, string folder,int limitWidth,int limitHeight)
        {
            try
            {
                string _dirName = this.filePath;
                //文件已日期归类
                string folderName = string.Empty;
                if (string.IsNullOrEmpty(folder))
                {
                    folderName = DateTime.Now.ToString("yyyyMMdd") + "/";
                }
                else
                {
                    folderName = folder + "/";
                }
                string extName = _postedFile.FileName.Substring(_postedFile.FileName.IndexOf('.') + 1);
                //验证文件合法性
                if (!CheckExt(this.fileType, extName))
                {
                    return "{msg: 0,msbox: \"文件" + extName + "不是合法类型！\"}";
                }
                if (_postedFile.InputStream == null || _postedFile.InputStream.Length > 1*1024*1024)
                {
                    return "{msg: 0,msbox: \"文件上传文件大小超过限制！\"}";
                }
                Image uImg = Image.FromStream(_postedFile.InputStream);
                int img_height = uImg.Height;
                int img_width = uImg.Width;
                if (limitWidth > 0 && limitHeight>0)
                {
                    if (img_width != limitWidth)
                    {
                        return "{msg: 0,msbox: \"图片宽度不符合规定宽度：" + limitWidth + "\"}";
                    }
                    if (img_height != limitHeight)
                    {
                        return "{msg: 0,msbox: \"图片高度不符合规定高度" + limitHeight + "\"}";
                    }
                }
                //文件名为当时时间命名
                //Guid g = new Guid();
                //string fileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_"+img_width+"x"+img_height+"." + extName;
                string fileName = Guid.NewGuid().ToString("N") + "_" + img_width + "x" + img_height + "." + extName;
                if (!_dirName.StartsWith("/")) { _dirName = "/" + _dirName; }
                if (!_dirName.EndsWith("/")) { _dirName += "/"; }
                string dirPath = _dirName + folderName;
                //文件网站相对地址
                string sererName = dirPath + fileName;
                //文件物理地址
                string physicalDir = HttpContext.Current.Server.MapPath(dirPath);
                //己栏目日期创建目录
                if (!Directory.Exists(physicalDir))
                {
                    Directory.CreateDirectory(physicalDir);
                }
                //文件物理目录
                string toFile = physicalDir + fileName;
                _postedFile.SaveAs(toFile);
                //返回文件网站相对地址
                return "{msg: 1,msbox: \"" + sererName + "\"}";
            }
            catch (Exception ex)
            {
                return "{msg: 0,msbox: \"发生意外了，啊呀呀呀！" + ex.Message + "\"}";
            }
        }

        /// <summary>
        /// 检查文件合法性
        /// </summary>
        /// <param name="fileType">合法文件类型</param>
        /// <param name="fileExt">文件类型</param>
        /// <returns></returns>
        private bool CheckExt(string fileType, string fileExt)
        {
            string[] allowExt = fileType.Split('|');
            foreach (string item in allowExt)
            {
                if (item.ToLower() == fileExt.ToLower())
                { return true; }
            }
            return false;
        }
    }
}
