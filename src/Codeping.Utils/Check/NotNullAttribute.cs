using AspectCore.DynamicProxy.Parameters;
using System;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    /// <summary>
    /// 验证不能为null
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotNullAttribute : ParameterInterceptorAttribute
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            if (context.Parameter.Value == null)
            {
                throw new ArgumentNullException(context.Parameter.Name);
            }

            return next(context);
        }
    }
}
