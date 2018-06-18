namespace NBD.SDK
{
    public class Progress
    {
        private Goal goal;
        private ushort percentComplete;

        public Progress(Goal goal)
        {
            this.goal = goal;
            this.percentComplete = 0;
        }

        public ushort PercentComplete => this.percentComplete;

        public bool IsFinished => this.PercentComplete == 100;

        public void MakeProgress()
        {
        }
    }
}
