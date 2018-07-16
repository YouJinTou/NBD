using NBD.SDK;
using System;
using System.ComponentModel.DataAnnotations;

namespace NBD.Tracker.Models
{
    public class GoalBindingModel
    {
        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public uint RecurrenceValue { get; set; }

        public int? Target { get; set; }
    }
}
