using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;

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
            if (context.Parameter.Value.SafeString().IsEmpty())
            {
                throw new ArgumentNullException(context.Parameter.Name);
            }

            return next(context);
        }
    }
}
