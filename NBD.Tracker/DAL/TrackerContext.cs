using Microsoft.EntityFrameworkCore;
using NBD.SDK;

namespace NBD.Tracker.DAL
{
    public class TrackerContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; }
    }
}
