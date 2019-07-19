using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Codeping.Constants;
using Microsoft.Extensions.Logging;

namespace Codeping.Utils.Logging
{
    internal class UtilLogger<TLogEntry> : IUtilLogger
        where TLogEntry : LogEntry, new()
    {
        private readonly ILogQueue _queue;

        public UtilLogger(ILogQueue queue)
        {
            _queue = queue;
        }

        public void Log(LogLevel logLevel, string title, Exception exception, string message, params object[] args)
        {
            var entry = new TLogEntry
            {
                Title = title,
                Level = logLevel,
                Content = string.Format(message, args),
                Exception = exception,
                CreateTime = DateTime.Now,
                StackTrace = exception.StackTrace,
                Num = Guid.NewGuid().ToString(F_Guid.D),
                Sender = exception.TargetSite.GetFullName(),
            };

            _queue.Enqueue(entry);
        }

        public void Log(LogLevel logLevel, string title, string message, params object[] args)
        {
            var (stackTrace, sender) = this.GetStack();

            var entry = new TLogEntry
            {
                Title = title,
                Level = logLevel,
                Content = string.Format(message, args),
                StackTrace = stackTrace,
                CreateTime = DateTime.Now,
                Num = Guid.NewGuid().ToString(F_Guid.D),
                Sender = sender,
            };

            _queue.Enqueue(entry);
        }

        public void Log(TLogEntry entry, bool isSaveNow = false)
        {
            if (entry == null)
            {
                return;
            }

            if (entry.Sender.IsEmpty() ||
                entry.StackTrace.IsEmpty())
            {
                var (stackTrace, sender) = this.GetStack();

                if (entry.Sender.IsEmpty())
                {
                    entry.Sender = sender;
                }

                if (entry.StackTrace.IsEmpty())
                {
                    entry.StackTrace = stackTrace;
                }
            }

            if (entry.Num.IsEmpty())
            {
                entry.Num = Guid.NewGuid().ToString(F_Guid.D);
            }

            if (isSaveNow)
            {
                _queue.Write(entry);
            }
            else
            {
                _queue.Enqueue(entry);
            }
        }

        public void Log(LogEntry entry, bool isSaveNow = false)
        {
            this.Log(entry as TLogEntry, isSaveNow);
        }

        public void LogCritical(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Critical, title, exception, message, args);
        }

        public void LogCritical(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Critical, title, message, args);
        }

        public void LogDebug(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Debug, title, exception, message, args);
        }

        public void LogDebug(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Debug, title, message, args);
        }

        public void LogError(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Error, title, message, args);
        }

        public void LogError(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Error, title, exception, message, args);
        }

        public void LogInformation(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Information, title, exception, message, args);
        }

        public void LogInformation(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Information, title, message, args);
        }

        public void LogTrace(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Trace, title, exception, message, args);
        }

        public void LogTrace(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Trace, title, message, args);
        }

        public void LogWarning(string title, string message, params object[] args)
        {
            this.Log(LogLevel.Warning, title, message, args);
        }

        public void LogWarning(string title, Exception exception, string message, params object[] args)
        {
            this.Log(LogLevel.Warning, title, exception, message, args);
        }

        private (string stackTrace, string sender) GetStack()
        {
            var stackTrace = new StackTrace();

            using var reader = new StringReader(stackTrace.ToString());

            var line = reader.ReadLine();

            while (line.TrimStart().StartsWith("at Codeping.Utils.Logging"))
            {
                line = reader.ReadLine();
            }

            var builder = new StringBuilder();

            builder.AppendLine(line);

            builder.Append(reader.ReadToEnd());

            var sender = line.Trim().Substring("at".Length);

            return (builder.ToString(), sender);
        }
    }
}
