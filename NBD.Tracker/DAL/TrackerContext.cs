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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Parent)
                .WithMany(pg => pg.SubGoals);

            base.OnModelCreating(modelBuilder);
        }
    }
}
