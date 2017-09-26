/// <summary>
///     类说明：log4net日志操作类
/// </summary>
using log4net;
using System;

namespace JDI.Utility
{
    public class LogHelper
    {
        private static readonly ILog logInfo = LogManager.GetLogger("loginfo");      //静态只读 日志Info对象
        private static readonly ILog logError = LogManager.GetLogger("logerror");     //静态只读 日志Error对象

        /// <summary>
        ///     添加info日志
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog(string info)
        {
            try
            { 
                if(logInfo.IsInfoEnabled)
                {
                    logInfo.Info(info);
                }
            }
            catch
            { }
        }

        /// <summary>
        ///     添加异常日志
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLog(string info,Exception ex)
        {
            try
            { 
                if(logError.IsErrorEnabled)
                {
                    logError.Error(info,ex);
                }
            }
            catch
            {}
        }
    }
}
