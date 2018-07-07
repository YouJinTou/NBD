using NBD.SDK;
using System;
using System.Threading.Tasks;

namespace NBD.Tracker.DAL
{
    public interface IGoalsRepository
    {
        Task<Goal> GetGoalAsync(Guid id);

        Task AddGoalAsync(Goal goal);
    }
}
