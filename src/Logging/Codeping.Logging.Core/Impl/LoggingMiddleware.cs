using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codeping.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Codeping.Utils.Logging
{
    internal class LoggingMiddleware
    {
        private readonly ILogQueue _logger;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, ILogQueue logger)
        {
            _next = next;

            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var entry = new LogEntry
                {
                    Title = "全局异常",
                    Content = ex.Message,
                    Level = LogLevel.Error,
                    StackTrace = ex.StackTrace,
                    CreateTime = DateTime.Now,
                    Exception = ex,
                    Sender = ex.TargetSite.GetFullName(),
                    Num = Guid.NewGuid().ToString(F_Guid.D),
                };

                _logger.Write(entry);

                httpContext.Response.Redirect($"/UtilLog/Message?num={entry.Num}");
            }
        }
    }
}
