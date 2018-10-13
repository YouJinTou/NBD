using System;
using System.Linq;
using System.Threading.Tasks;
using NBD.DAL;
using NBD.SDK;
using NBD.Services.Core.Exceptions;

namespace NBD.Services.Goals
{
    public class GoalsService : IGoalsService
    {
        private readonly IRepository<Goal> goals;

        public GoalsService(IRepository<Goal> goals)
        {
            this.goals = goals;
        }

        public async Task<Goal> GetGoalAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var goal = this.goals.Where(g => g.Id == id).FirstOrDefault();

                if (goal == null)
                {
                    throw new GoalNotFoundException(id.ToString());
                }

                return goal;
            });
        }
    }
}
