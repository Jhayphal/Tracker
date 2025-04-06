using System;

namespace Tracker
{
    public sealed class Record
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public string Comment { get; set; }
    }
}
