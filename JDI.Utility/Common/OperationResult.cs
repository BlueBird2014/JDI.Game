﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Common
{
    public class OperationResult
    {
        #region 构造函数
        /// <summary>
        ///     初始化一个 业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        public OperationResult()
        {
        }

        /// <summary>
        ///     初始化一个 业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        public OperationResult(OperationResultType resultType)
        {
            ResultType = resultType;
        }

        /// <summary>
        ///     初始化一个 定义返回消息的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType)
        {
            Message = message;
        }

        /// <summary>
        ///     初始化一个 定义返回消息与附加数据的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult(OperationResultType resultType, string message, object appendData)
            : this(resultType, message)
        {
            AppendData = appendData;
        }

        #endregion
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        /// 操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作附加信息
        /// </summary>
        public object AppendData { get; set; }
    }
}