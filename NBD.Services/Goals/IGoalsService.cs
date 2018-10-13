using NBD.SDK;
using System;
using System.Threading.Tasks;

namespace NBD.Services.Goals
{
    public interface IGoalsService
    {
        Task<Goal> GetGoalAsync(Guid id);
    }
}
