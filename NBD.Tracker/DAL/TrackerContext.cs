using Microsoft.EntityFrameworkCore;
using NBD.SDK;

namespace NBD.Tracker.DAL
{
    public class TrackerContext : DbContext
    {
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Goal> Goals { get; set; }
    }
}
