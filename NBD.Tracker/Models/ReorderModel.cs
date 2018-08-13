﻿using NBD.SDK;
using NBD.Tracker.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Tracker.Models
{
    public class ReorderModel
    {
        [Required]
        public Guid GoalId { get; set; }

        [Required]
        public Guid TargetParentId { get; set; }

        public async Task<bool> IsValidReorderAsync(IRepository<Goal> goals)
        {
            if (this.GoalId == this.TargetParentId)
            {
                return false;
            }

            var goal = goals.Where(g => g.Id == this.GoalId).FirstOrDefault();

            if (goal == null)
            {
                return false;
            }

            var targetParent = await goals.GetAsync(this.TargetParentId);

            if (targetParent == null)
            {
                return false;
            }

            var childrenIds = goal.GetTree().Select(g => g.Id);
            var targetParentIsAChild = childrenIds.Any(c => c == this.TargetParentId);

            return !targetParentIsAChild;
        }
    }
}
