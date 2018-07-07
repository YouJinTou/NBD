using Microsoft.EntityFrameworkCore;
using NBD.SDK;
using System;
using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public class GoalsRepository : IGoalsRepository
    {
        private readonly DbContext context;

        public GoalsRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<Goal> GetGoalAsync(Guid id)
        {
            return await this.context.FindAsync<Goal>(id);
        }

        public async Task AddGoalAsync(Goal goal)
        {
            this.context.Add(goal);

            await this.context.SaveChangesAsync();
        }
    }
}
