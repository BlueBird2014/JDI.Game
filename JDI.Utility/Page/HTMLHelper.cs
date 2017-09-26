using System;
using System.IO;
using System.Net;
using System.Text;

namespace JDI.Utility
{
    public class HTMLHelper
    {
        /// <summary>
        /// 生成静态页面通过访问页面地址下保存静态页方式
        /// </summary>
        /// <param name="url">动态访问地址</param>
        /// <param name="path">保存页面地址</param>
        /// <param name="encoding">网页编码</param>
        /// <returns></returns>
        public static bool GenerateHtmlByUrl(string url,string dir,string page,Encoding encoding)
        {
            WebClient wc = new WebClient();
            wc.Encoding = encoding;
            string strHtml = wc.DownloadString(url);
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var path = Path.Combine(dir, page);
                File.WriteAllText(path, strHtml, encoding);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


    }
}
