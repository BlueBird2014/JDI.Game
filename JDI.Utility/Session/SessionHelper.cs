/// <summary>
///     类说明：Session操作类
/// </summary>
using System.Web;

namespace JDI.Utility
{
    public class SessionHelper
    {
        /// <summary>
        ///     获取Session对象 By Session名称
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns></returns>
        public static object GetSession(string name)
        { 
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        ///     获取Sessin对象的值
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns>Session的值</returns>
        public static string GetSessionString(string name)
        {
            if(HttpContext.Current.Session != null && HttpContext.Current.Session[name] != null)
            {
                return HttpContext.Current.Session[name] + "";
            }
            return "";
        }

        /// <summary>
        ///     设置Session 默认20分钟过期
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        public static void SetSession(string name,object value)
        {
            SetSession(name,value,20);
        }

        /// <summary>
        ///     设置Session 带过期时间
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        /// <param name="expires">过期时间(单位分钟)</param>
        public static void SetSession(string name,object value,int expires)
        {
            RemoveSession(name);
            HttpContext.Current.Session.Add(name, value);
            HttpContext.Current.Session.Timeout = expires;
        }

        /// <summary>
        ///     删除指定的Session
        /// </summary>
        /// <param name="name">Session名称</param>
        public static void RemoveSession(string name)
        { 
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        ///     移除所有Session
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        ///     删除所有Session
        /// </summary>
        public static void RemoveAllSession()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}
