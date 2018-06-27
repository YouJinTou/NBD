using System;

namespace NBD.SDK
{
    public class Recurrence
    {
        private RecurrenceType type;
        private uint value;

        public Recurrence(RecurrenceType type, uint value)
        {
            if (type == RecurrenceType.Single && value != 0)
            {
                throw new ArgumentException(
                    $"Type is {type.ToString()}, so value cannot be {value}.");
            }

            this.type = type;
            this.value = value;
        }

        public RecurrenceType Type => this.type;

        public uint Value => this.value;
    }
}
