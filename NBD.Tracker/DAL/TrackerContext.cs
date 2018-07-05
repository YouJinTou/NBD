using Microsoft.EntityFrameworkCore;
using NBD.SDK;
using System.Linq;

namespace NBD.Tracker.DAL
{
    public class TrackerContext : DbContext, ITrackerContext
    {
        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Goal> DbGoals { get; set; }

        public IQueryable<Goal> Goals
        {
            get
            {
                return this.DbGoals;
            }
            set
            {
                this.DbGoals = (DbSet<Goal>)value;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.ParentGoal)
                .WithMany(pg => pg.SubGoals);

            base.OnModelCreating(modelBuilder);
        }
    }
}
