using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Codeping.Utils.Logging
{
    public class LoggingContext<TLogEntry> : DbContext where TLogEntry : LogEntry
    {
        public DbSet<TLogEntry> Logs { get; set; }
    }
}
