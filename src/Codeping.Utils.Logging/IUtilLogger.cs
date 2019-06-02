using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Codeping.Utils.Logging
{
    public interface IUtilLogger
    {
        void Log(LogLevel logLevel, string title, string message, params object[] args);
        void Log(LogLevel logLevel, string title, Exception exception, string message, params object[] args);
        void LogCritical(string title, string message, params object[] args);
        void LogCritical(string title, Exception exception, string message, params object[] args);
        void LogDebug(string title, string message, params object[] args);
        void LogDebug(string title, Exception exception, string message, params object[] args);
        void LogError(string title, string message, params object[] args);
        void LogError(string title, Exception exception, string message, params object[] args);
        void LogInformation(string title, string message, params object[] args);
        void LogInformation(string title, Exception exception, string message, params object[] args);
        void LogTrace(string title, string message, params object[] args);
        void LogTrace(string title, Exception exception, string message, params object[] args);
        void LogWarning(string title, string message, params object[] args);
        void LogWarning(string title, Exception exception, string message, params object[] args);

        /// <summary>
        /// 扩展接口, 提供保存派生 LogEntry 的能力;
        /// 会自动修正 Num/StackTrace/Sender 属性。
        /// </summary>
        /// <param name="entry">要保存的日志</param>
        /// <param name="isSaveNow"></param>
        void Log(LogEntry entry, bool isSaveNow = false);
    }
}