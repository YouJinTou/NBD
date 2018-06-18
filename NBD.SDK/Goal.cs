using System;

namespace NBD.SDK
{
    public abstract class Goal
    {
        private Guid id;
        private string title;
        private DateTime startDate;
        private DateTime endDate;
        private Recurrence recurrence;
        private Progress progress;

        public Goal(string title, DateTime startDate, DateTime endDate, Recurrence recurrence)
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

            this.id = Guid.NewGuid();
            this.title = title;
            this.startDate = startDate;
            this.endDate = endDate;
            this.recurrence = recurrence; // TODO VALIDATE
            this.progress = new Progress(this);
        }

        public Guid Id => this.id;

        public string Title => this.title;

        public DateTime StartTime => this.startDate;

        public DateTime EndTime => this.endDate;

        public Progress Progress => this.progress;

        public abstract void MakeProgress();
    }
}
