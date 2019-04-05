using Microsoft.EntityFrameworkCore;

namespace Codeping.Utils.TimedJob
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
