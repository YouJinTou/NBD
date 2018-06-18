using System;

namespace NBD.SDK
{
    public abstract class Goal
    {
        private Guid id;
        private DateTime startTime;

        public Goal(string title, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title must not be empty.");
            }

            if (startTime <= DateTime.Now)
            {
                throw new ArgumentException("Start time must be in the future.");
            }

            if (endTime <= DateTime.Now || endTime <= startTime)
            {
                throw new ArgumentException("End time must be greater than start time.");
            }

            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
