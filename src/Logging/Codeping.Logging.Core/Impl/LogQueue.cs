using System;
using System.Collections.Generic;
using System.Text;
using Codeping.TimedJob.Core;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Codeping.Utils.Logging
{
    // TODO: 线程安全
    internal class LogQueue<TContext, TLogEntry> : ILogQueue, IJob
        where TContext : LoggingContext<TLogEntry>
        where TLogEntry : LogEntry
    {
        private static DateTime _lastWriteTime;
        private static readonly SemaphoreSlim _lock;
        private static readonly ConcurrentQueue<TLogEntry> _logs;

        private readonly LoggingOptions _options;
        private readonly TContext _context;
        private readonly ILogger _logger;

        static LogQueue()
        {
            _lastWriteTime = DateTime.Now;

            _lock = new SemaphoreSlim(1);
            _logs = new ConcurrentQueue<TLogEntry>();
        }

        public LogQueue(LoggingOptions options, ILogger logger)
        {
            _options = options;

            _logger = logger;
        }

        public void Write(LogEntry entry)
        {
            this.Write(entry as TLogEntry);
        }

        public void Enqueue(LogEntry entry)
        {
            this.Enqueue(entry as TLogEntry);
        }

        private void Enqueue(TLogEntry entry)
        {
            if (entry == null)
            {
                return;
            }

            if (_options.IsMerge)
            {
                _logger.Log(entry.Level, entry.Exception, entry.Content);
            }

            if (!_options.EnableBatch)
            {
                this.Write(entry);

                return;
            }

            _logs.Enqueue(entry);
        }

        private void Write(TLogEntry entry)
        {
            if (entry == null)
            {
                return;
            }

            if (_options.IsMerge)
            {
                _logger.Log(entry.Level, entry.Exception, entry.Content);
            }

            try
            {
                _context.Logs.Add(entry);

                _context.SaveChanges();

                _lastWriteTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        [Invoke(IsEnabled = true, Interval = 1000, SkipWhileExecuting = true)]
        private void Enforce()
        {
            if (!_options.EnableBatch ||
                _logs.Count <= 0)
            {
                return;
            }

            if (_logs.Count >= _options.Batch ||
                _lastWriteTime.AddSeconds(_options.Interval) <= DateTime.Now)
            {
                this.Log();
            }
        }

        private void Log()
        {
            if (_lock.CurrentCount <= 0 ||
                _logs.Count <= 0)
            {
                return;
            }

            try
            {
                _lock.Wait();

                bool isChanged = false;

                while (_logs.Count > 0)
                {
                    if (_logs.TryDequeue(out var entry) && entry != null)
                    {
                        _context.Logs.Add(entry);

                        isChanged = true;

                        _lastWriteTime = DateTime.Now;
                    }
                }

                if (isChanged)
                {
                    _context.SaveChanges();

                    _lastWriteTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
