using System;
using System.Threading.Tasks;
using NBD.SDK;
using NBD.Services.Core.Exceptions;

namespace NBD.Services.Goals
{
    public class GoalsService : IGoalsService
    {
        //private readonly IRepository<Goal> goals;

        public GoalsService()
        {

        }

        public Task<Goal> GetGoalAsync(Guid id)
        {
            throw new NotImplementedException();
            //var goal = this.goals.Where(g => g.Id == id).FirstOrDefault();

            //if (goal == null)
            //{
            //    throw new GoalNotFoundException(id.ToString());
            //}

            //return goal;
        }
    }
}
