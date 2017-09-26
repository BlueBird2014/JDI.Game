using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Game.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 通过http请求获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static T RequestHttpData<T>(string url, string requestMethod)
        {
            //创建请求
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 60 * 1000; //1分钟
            request.Method = requestMethod;
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            //获取数据
            using (var response = request.GetResponseAsync())
            {
                using (StreamReader sr = new StreamReader(response.Result.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
                }
            }
        }

        /// <summary>
        /// 通过http请求获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static string RequestHttpGet(string url)
        {
            //创建请求
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 60 * 1000; //1分钟
            request.Method = "GET";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            //获取数据
            using (var response = request.GetResponseAsync())
            {
                using (StreamReader sr = new StreamReader(response.Result.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
