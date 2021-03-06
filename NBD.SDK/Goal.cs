﻿using NBD.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NBD.SDK
{
    public class Goal
    {
        public Goal()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ParentGoal")]
        public Guid? ParentId { get; set; }

        public virtual Goal Parent { get; set; }

        [Required]
        [StringLength(512)]
        public string Title { get; set; }

        [FutureDateTime]
        public DateTime? StartDate { get; set; }

        [FutureDateTime]
        // TO-DO [AfterDateTimeAttribute]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        [Required]
        public RecurrenceType RecurrenceType { get; set; }

        [Required]
        public uint RecurrenceValue { get; set; }

        public int? Target { get; set; }

        public uint Progress { get; set; }

        public virtual ICollection<Goal> SubGoals { get; set; }

        [Required]
        public bool IsReached { get; set; }

        public void MakeProgress(uint chunk)
        {
            if (chunk == 0)
            {
                throw new ArgumentException("Progress cannot be 0.");
            }

            var progress = this.Progress + chunk;
            var isReached = (this.Target == null) ? true : progress >= this.Target;

            if (this.SubGoals != null)
            {
                if (isReached && !this.SubGoals.All(sg => sg.IsReached))
                {
                    throw new InvalidOperationException(
                        "Cannot complete goal before all of its subgoals are reached.");
                }
            }

            this.Progress += chunk;
            this.IsReached = isReached;
        }

        public void AddSubGoal(Goal goal)
        {
            if ((goal.StartDate != null && goal.EndDate == null) || 
                (goal.StartDate == null && goal.EndDate != null))
            {
                throw new ArgumentException("One of the goal dates is null.");
            }

            if (goal.StartDate <= this.StartDate || goal.StartDate >= this.EndDate)
            {
                throw new ArgumentException("Invalid subgoal start date.");
            }

            if (goal.EndDate <= this.StartDate || goal.EndDate >= this.EndDate)
            {
                throw new ArgumentException("Invalid subgoal end date.");
            }

            this.SubGoals.Add(goal);
        }

        public IEnumerable<Goal> GetTree()
        {
            var tree = new List<Goal> { this };

            this.GetTreeRecursive(this, tree);

            return tree;
        }

        private IEnumerable<Goal> GetTreeRecursive(Goal root, ICollection<Goal> goals)
        {
            if (root.SubGoals == null || root.SubGoals.Count == 0)
            {
                return goals;
            }

            foreach (var child in root.SubGoals)
            {
                goals.Add(child);

                this.GetTreeRecursive(child, goals);
            }

            return goals;
        }
    }
}
