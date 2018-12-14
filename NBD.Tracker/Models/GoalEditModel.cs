using NBD.SDK;
using NBD.SDK.Attributes;
using NBD.Services.Goals;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NBD.Tracker.Models
{
    public class GoalEditModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; }

        [FutureDateTime]
        public DateTime? StartDate { get; set; }

        [FutureDateTime]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public uint RecurrenceValue { get; set; }

        public int? Target { get; set; }

        public async Task<bool> IsWithinParentDatesAsync(IGoalsService goalsService)
        {
            var goal = await goalsService.GetGoalAsync(this.Id);

            if (goal.ParentId == null)
            {
                return true;
            }

            var startDateValid =
                this.StartDate == null ||
                (this.StartDate >= goal.Parent.StartDate);
            var endDateValid =
                this.EndDate == null ||
                (this.EndDate <= goal.Parent.EndDate);

            return startDateValid && endDateValid;
        }
    }
}
