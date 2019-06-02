using System;
using Codeping.Utils.Logging;
using Codeping.Utils.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// 将 Util Logging 服务加入 Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TContext">用于写入日志的数据库上下文, 基于 Entity Framework</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUtilLogging<TContext>(this IServiceCollection services)
            where TContext : LoggingContext<LogEntry>
        {
            return services.AddUtilLogging<TContext>(options => { });
        }

        /// <summary>
        /// 将 Util Logging 服务加入 Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TContext">用于写入日志的数据库上下文, 基于 Entity Framework</typeparam>
        /// <param name="services"></param>
        /// <param name="options">配置日志保存策略等</param>
        /// <returns></returns>
        public static IServiceCollection AddUtilLogging<TContext>(this IServiceCollection services, Action<LoggingOptions> options)
            where TContext : LoggingContext<LogEntry>
        {
            services.AddScoped<LoggingOptions>(x =>
            {
                var option = new LoggingOptions();

                options?.Invoke(option);

                return option;
            });

            services.AddScoped<ILogQueue, LogQueue<TContext, LogEntry>>();

            services.AddScoped<IUtilLogger, UtilLogger<LogEntry>>();

            services.ConfigureOptions<ManifestEmbeddedFileProviderConfigureOptions>();

            return services;
        }
        /// <summary>
        /// 将 Util Logging 服务加入 Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TContext">用于写入日志的数据库上下文, 基于 Entity Framework</typeparam>
        /// <typeparam name="TLogEntry">用于写入日志的实体类型</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUtilLogging<TContext, TLogEntry>(this IServiceCollection services)
            where TContext : LoggingContext<TLogEntry>
            where TLogEntry : LogEntry, new()
        {
            return services.AddUtilLogging<TContext, TLogEntry>(options => { });
        }

        /// <summary>
        /// 将 Util Logging 服务加入 Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TContext">用于写入日志的数据库上下文, 基于 Entity Framework</typeparam>
        /// <typeparam name="TLogEntry">用于写入日志的实体类型</typeparam>
        /// <param name="services"></param>
        /// <param name="options">配置日志保存策略等</param>
        /// <returns></returns>
        public static IServiceCollection AddUtilLogging<TContext, TLogEntry>(this IServiceCollection services, Action<LoggingOptions> options)
            where TContext : LoggingContext<TLogEntry>
            where TLogEntry : LogEntry, new()
        {
            services.AddScoped<LoggingOptions>(x =>
            {
                var option = new LoggingOptions();

                options?.Invoke(option);

                return option;
            });

            services.AddScoped<ILogQueue, LogQueue<TContext, TLogEntry>>();

            services.AddScoped<IUtilLogger, UtilLogger<TLogEntry>>();

            services.ConfigureOptions<ManifestEmbeddedFileProviderConfigureOptions>();

            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// 从管道捕获同步和异步 System.Exception 实例, 记录到日志中并生成 HTML 错误响应;
        /// 请尽量把该中间件至于管道最前端, 以捕获更多的异常。
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUtilLogging(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggingMiddleware>();

            return builder;
        }
    }
}
