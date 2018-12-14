using System;

namespace NBD.Services.Core.Exceptions
{
    public class GoalNotFoundException : Exception
    {
        public GoalNotFoundException()
        {
        }

        public GoalNotFoundException(string message)
            : base(message)
        {
        }

        public GoalNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
