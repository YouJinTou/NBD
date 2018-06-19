namespace NBD.SDK
{
    public class Progress
    {
        private Recurrence recurrence;
        private ushort percentComplete;

        public Progress(Recurrence recurrence)
        {
            this.recurrence = recurrence;
            this.percentComplete = 0;
        }

        public ushort PercentComplete => this.percentComplete;

        public bool IsFinished => this.PercentComplete == 100;

        public void MakeProgress()
        {
            switch (this.recurrence.Type)
            {
                case RecurrenceType.None:
                    break;
                case RecurrenceType.Daily:
                    break;
                case RecurrenceType.Weekly:
                    break;
                case RecurrenceType.Monthly:
                    break;
                case RecurrenceType.Yearly:
                    break;
                default:
                    break;
            }
        }
    }
}
