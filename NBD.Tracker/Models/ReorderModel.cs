using NBD.Services.Core.Exceptions;
using NBD.Services.Goals;
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

        public async Task<bool> IsValidReorderAsync(IGoalsService goalsService)
        {
            if (this.GoalId == this.TargetParentId)
            {
                return false;
            }

            try
            {
                var goal = await goalsService.GetGoalAsync(this.GoalId);
                var targetParent = await goalsService.GetGoalAsync(this.TargetParentId);
                var childrenIds = goal.GetTree().Select(g => g.Id);
                var targetParentIsAChild = childrenIds.Any(c => c == this.TargetParentId);

                return !targetParentIsAChild;
            }
            catch (GoalNotFoundException)
            {
                return false;
            }
        }
    }
}
