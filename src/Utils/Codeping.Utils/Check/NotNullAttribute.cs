using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;

namespace Codeping.Utils
{
    /// <summary>
    /// 验证不能为 null
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
