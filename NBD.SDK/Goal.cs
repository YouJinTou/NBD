using System;
using System.Collections.Generic;

namespace NBD.SDK
{
    public class Goal
    {
        private Guid id;
        private string title;
        private DateTime? startDate;
        private DateTime? endDate;
        private Recurrence recurrence;
        private int? target;
        private uint progress;
        private ICollection<Goal> subGoals;

        public Goal(
            string title, 
            DateTime? startDate, 
            DateTime? endDate, 
            Recurrence recurrence,
            int? target = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title must not be empty.");
            }

            if (startDate <= DateTime.Now)
            {
                throw new ArgumentException("Start time must be in the future.");
            }

            if (endDate <= DateTime.Now || endDate <= startDate)
            {
                throw new ArgumentException("End time must be greater than start time.");
            }

            this.id = Guid.NewGuid();
            this.title = title;
            this.startDate = startDate;
            this.endDate = endDate;
            this.recurrence = recurrence;
            this.target = target;
            this.subGoals = new List<Goal>();
        }

        public Guid Id => this.id;

        public string Title => this.title;

        public DateTime? StartDate => this.startDate;

        public DateTime? EndDate => this.endDate;

        public int? Target => this.target;

        public bool IsReached => 
            (this.progress >= target) || 
            (this.progress > 0 && target == null);

        public void MakeProgress(uint chunk)
        {
            if (chunk == 0)
            {
                throw new ArgumentException("Progress cannot be 0.");
            }

            this.progress += chunk;
        }

        public void AddSubGoal(Goal goal)
        {
            if (goal.StartDate <= this.StartDate || goal.StartDate >= this.EndDate)
            {
                throw new ArgumentException("Invalid subgoal start date.");
            }

            if (goal.EndDate <= this.StartDate || goal.EndDate >= this.EndDate)
            {
                throw new ArgumentException("Invalid subgoal end date.");
            }

            this.subGoals.Add(goal);
        }
    }
}
