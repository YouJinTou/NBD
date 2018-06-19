using System;
using System.Collections.Generic;

namespace NBD.SDK
{
    public class Goal
    {
        private Guid id;
        private string title;
        private DateTime startDate;
        private DateTime endDate;
        private Recurrence recurrence;
        private Progress progress;
        private int? target;
        private ICollection<Goal> subGoals;

        public Goal(
            string title, 
            DateTime startDate, 
            DateTime endDate, 
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

            if (recurrence.Type != RecurrenceType.None && recurrence.Value == 0)
            {
                throw new ArgumentException(
                    "Recurrence value must be greater than 0 when there is a recurrency type.");
            }

            this.id = Guid.NewGuid();
            this.title = title;
            this.startDate = startDate;
            this.endDate = endDate;
            this.recurrence = recurrence;
            this.progress = new Progress(this.recurrence);
            this.subGoals = new List<Goal>();
            this.target = target;
        }

        public Guid Id => this.id;

        public string Title => this.title;

        public DateTime StartDate => this.startDate;

        public DateTime EndDate => this.endDate;

        public Progress Progress => this.progress;

        public Recurrence Recurrence => this.recurrence;

        public int? Target => this.target;

        public void MakeProgress()
        {
            this.progress.MakeProgress();
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
