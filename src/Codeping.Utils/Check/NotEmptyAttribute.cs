﻿using AspectCore.DynamicProxy.Parameters;
using System;
using System.Threading.Tasks;
using Codeping.Utils;

namespace Codeping.Utils
{
    /// <summary>
    /// 验证不能为空
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotEmptyAttribute : ParameterInterceptorAttribute
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (string.IsNullOrWhiteSpace(context.Parameter.Value.SafeString()))
            {
                throw new ArgumentNullException(context.Parameter.Name);
            }

            return next(context);
        }
    }
}