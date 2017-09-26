/// <summary>
///     类型说明：字符串辅助操作类
/// </summary>
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace JDI.Utility
{
    public class StringHelper
    {
        // <summary>
        ///     判断指定字符串是否对象（Object）类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonObjectString(string input)
        {
            return input != null && input.StartsWith("{") && input.EndsWith("}");
        }

        /// <summary>
        ///     判断指定字符串是否集合类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonArrayString(string input)
        {
            return input != null && input.StartsWith("[") && input.EndsWith("]");
        }

        /// <summary>
        /// 根据IP地址获取城市名字
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string GetAddressByIp(string ipAddress)
        {
            string url = "http://whois.pconline.com.cn/ip.jsp";
            string ip = ipAddress;
            string query = "ip=" + ip;
            url += "?ip=" + ip;
            string text = GetResponseText(url, query);
            return text.Substring(text.IndexOf("省") + 1, 2);

        }
        /// <summary>
        /// 获取响应的数据流。
        /// </summary>
        /// <returns></returns>
        private static Stream GetResponseStream(string API_URL, string query)
        {

            var data = Encoding.UTF8.GetBytes(query);
            var request = (HttpWebRequest)WebRequest.Create(API_URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
                stream.Write(data, 0, data.Length);
            return request.GetResponse().GetResponseStream();
        }
        /// <summary>
        /// 获取响应的文本。
        /// </summary>
        /// <returns></returns>
        private static string GetResponseText(string API_URL, string query)
        {
            var text = string.Empty;
            using (var reader = new StreamReader(GetResponseStream(API_URL, query), Encoding.Default))
                text = reader.ReadToEnd();
            return text;
        }


        public static string StripHTML(string Htmlstring)
        {
            string regexstr = @"<[^>]*>";
            string _value = Regex.Replace(Htmlstring, regexstr, string.Empty, RegexOptions.IgnoreCase);
            return _value.Replace(" ", "").Replace("&nbsp;", "");
        }

        /// <summary>
        ///     祛除字符串中的HTML标签代码
        /// </summary>
        /// <param name="strhtml"></param>
        /// <returns></returns>
        //public static string StripHtml(string strhtml)
        //{
        //    string stroutput = strhtml;
        //    Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
        //    stroutput = regex.Replace(stroutput, "");
        //    return stroutput;
        //}


        /// <summary>
        /// 隐藏身份证年月日
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string HiddenPartID(string ID)
        {
            int lenght = ID.Length;
            if (lenght >= 15)
            {
                string strStart = ID.Substring(0, 10);
                string strEnd = ID.Substring(13);
                string hiddenID = strStart + "****" + strEnd;
                return hiddenID;
            }
            return ID;
        }

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";
            return tempString;
        }
        #endregion

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region 清除HTML标记且返回相应的长度
        public static string DropHTML(string Htmlstring, int strLen)
        {
            return CutString(DropHTML(Htmlstring), strLen);
        }
        #endregion

    }
}
