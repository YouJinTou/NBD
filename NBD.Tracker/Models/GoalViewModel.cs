using NBD.SDK;
using System;
using System.Collections.Generic;

namespace NBD.Tracker.Models
{
    public class GoalViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public uint RecurrenceValue { get; set; }

        public int? Target { get; set; }

        public uint Progress { get; set; }

        public ICollection<GoalViewModel> SubGoals { get; set; }

        public bool IsReached { get; set; }
    }
}
