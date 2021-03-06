﻿using System;
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

        public async Task AddGoalAsync(Goal goal)
        {
            await this.goals.AddAsync(goal);
        }

        public async Task DeleteCascadingAsync(Guid id)
        {
            var goal = await this.GetGoalAsync(id);

            await this.goals.DeleteManyAsync(goal.GetTree());
        }

        public async Task<Goal> EditGoalAsync(Goal goal)
        {
            var currentGoal = await this.GetGoalAsync(goal.Id);
            currentGoal.Target = goal.Target;
            currentGoal.StartDate = goal.StartDate;
            currentGoal.EndDate = goal.EndDate;
            currentGoal.Description = goal.Description;
            currentGoal.Title = goal.Title;
            currentGoal.RecurrenceType = goal.RecurrenceType;
            currentGoal.RecurrenceValue = goal.RecurrenceValue;

            await this.goals.EditAsync(currentGoal);

            return currentGoal;
        }

        public async Task<Goal> MakeProgressAsync(Guid id, uint chunk)
        {
            var goal = await this.GetGoalAsync(id);

            goal.MakeProgress(chunk);

            return await this.EditGoalAsync(goal);
        }

        public async Task<Goal> ReorderTreeAsync(Guid movableId, Guid newParentId)
        {
            var movable = await this.GetGoalAsync(movableId);
            movable.ParentId = newParentId;

            return await this.EditGoalAsync(movable);
        }
    }
}
