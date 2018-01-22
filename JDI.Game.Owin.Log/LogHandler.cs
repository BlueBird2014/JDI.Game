using Microsoft.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JDI.Game.Owin.Log
{
    /// <summary>
    /// 访问日志
    /// </summary>
    public class LogHandler : DelegatingHandler
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// 是否记录返回值
        /// </summary>
        private bool _isLogResult;

        /// <summary>
        /// 记录响应类型
        /// </summary>
        private List<string> _contentTypeLst;

        /// <summary>
        /// 初始化 LogHandler
        /// </summary>
        /// <param name="isLogResult">是否记录结果</param>
        /// <param name="logSelector">日志对象</param>
        public LogHandler(bool isLogResult, string logSelector = "Access")
        {
            _isLogResult = isLogResult;
            _logger = LogManager.GetLogger(logSelector);
            _contentTypeLst = new List<string> { "application/json" };
        }

        /// <summary>
        /// 初始化 LogHandler
        /// </summary>
        /// <param name="contentTypeList">记录日志类型</param>
        /// <param name="isLogResult">是否记录结果</param>
        /// <param name="logSelector">日志对象</param>
        public LogHandler(List<string> contentTypeList, bool isLogResult, string logSelector = "Access")
        {
            _isLogResult = isLogResult;
            _contentTypeLst = contentTypeList;
            _logger = LogManager.GetLogger(logSelector);
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);
            sw.Stop();

            if (_contentTypeLst.Contains(response.Content.Headers.ContentType.MediaType))
            {
                int wt, cpt = 0;
                ThreadPool.GetAvailableThreads(out wt, out cpt);
                var log = String.Format("[{0}] [{1}] 访问 [{2}] 耗时 [{3}]  线程池剩余[{4},{5}] {6}", request.Method, GetClientIP(request), request.RequestUri, sw.ElapsedMilliseconds, wt, cpt, Environment.NewLine);
                if (_isLogResult)
                {
                    log += GetResult(response);
                }
                log += Environment.NewLine;
                _logger.Info(log);
            }

            return response;
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private string GetResult(HttpResponseMessage response)
        {
            /*
               这个StreamReader不能关闭，也不能dispose， 关了就傻逼了
               因为你关掉后，后面的管道  或拦截器就没办法读取了 
            */
            var stream = response.Content.ReadAsStreamAsync().Result;
            var sr = new StreamReader(stream, Encoding.UTF8);
            var result = sr.ReadToEnd();
            stream.Position = 0;
            /*
               这里也要注意：   stream.Position = 0; 
               当你读取完之后必须把stream的位置设为开始
               因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。 
            */

            return result;
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetClientIP(HttpRequestMessage request)
        {
            if (request != null && request.Properties.ContainsKey("MS_OwinContext"))
            {
                return ((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
            }

            return null;
        }
    }
}
