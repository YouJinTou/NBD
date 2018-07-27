using System;
using System.ComponentModel.DataAnnotations;

namespace NBD.Tracker.Models
{
    public class ProgressBindingModel
    {
        [Required(ErrorMessage = "The goal ID is required.")]
        public Guid GoalId { get; set; }

        [Required(ErrorMessage = "Progress value missing.")]
        [Range(1, uint.MaxValue, ErrorMessage = "Progress value must be positive.")]
        public uint Progress { get; set; }
    }
}
