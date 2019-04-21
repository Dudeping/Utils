using Microsoft.EntityFrameworkCore;

namespace Codeping.TimedJob.Core
{
    public class TimedDbContext : DbContext
    {
        public TimedDbContext() : base()
        {
        }

        public TimedDbContext(DbContextOptions options) : base(options)
        {
        }

        
    }
}
