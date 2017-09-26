/// <summary>
///     类说明：Cookie操作类
/// </summary>
using System;
using System.Web;

namespace JDI.Utility
{
    public class CookieHelper
    {
        /// <summary>
        ///     获取Cookie值
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                return HttpContext.Current.Request.Cookies[cookieName].Value;
            }
            return "";
        }

        /// <summary>
        ///     读取cookie值，cookie[key]
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null && HttpContext.Current.Request.Cookies[cookieName][key] != null)
            {
                return HttpContext.Current.Request.Cookies[cookieName][key].ToString();
            }
            return "";
        }

        /// <summary>
        ///     添加一个cookie,带过期时间
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cooke值</param>
        /// <param name="expires">过期时间</param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName, cookieValue);
            }
            cookie.Expires = expires;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     添加一个cookie,默认过期时间是一年
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, DateTime.Now.AddYears(1));
        }

        /// <summary>
        ///     添加一个cookie,cookie[key],带过期时间
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="key">key</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="expires">过期时间</param>
        public static void SetCookie(string cookieName, string key, string cookieValue, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookieName == null)
            {
                cookie = new HttpCookie(cookieName);
            }
            cookie[key] = cookieValue;
            cookie.Expires = expires;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     添加一个cookie,cookie[key],默认过期时间是一年
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="key">key</param>
        /// <param name="cookieValue">cookie值</param>
        public static void SetCookie(string cookieName, string key, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, key, DateTime.Now.AddYears(1));
        }

        /// <summary>
        ///     删除指定cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if(cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
