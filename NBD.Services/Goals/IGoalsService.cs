using NBD.SDK;
using System;
using System.Threading.Tasks;

namespace NBD.Services.Goals
{
    public interface IGoalsService
    {
        Task<Goal> GetGoalAsync(Guid id);

        Task AddGoalAsync(Goal goal);

        Task DeleteCascadingAsync(Guid id);

        Task<Goal> EditGoalAsync(Goal goal);

        Task<Goal> MakeProgressAsync(Guid id, uint chunk);
    }
}
