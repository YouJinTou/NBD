using NBD.SDK;
using NBD.SDK.Attributes;
using NBD.Tracker.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NBD.Tracker.Models
{
    public class GoalBindingModel
    {
        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; }

        [FutureDateTime]
        public DateTime? StartDate { get; set; }

        [FutureDateTime]
        public DateTime? EndDate { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public uint RecurrenceValue { get; set; }

        public int? Target { get; set; }

        public async Task<bool> IsWithinParentDatesAsync(IRepository<Goal> goals)
        {
            if (this.ParentId == null)
            {
                return true;
            }

            var parent = await goals.GetAsync(this.ParentId);
            var startDateValid =
                this.StartDate == null ||
                (this.StartDate >= parent.StartDate);
            var endDateValid =
                this.EndDate == null ||
                (this.EndDate <= parent.EndDate);

            return startDateValid && endDateValid;
        }
    }
}
