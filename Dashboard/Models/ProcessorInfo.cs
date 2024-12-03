namespace Dashboard.Models
{
    public class ProcessorInfo
    {
        public int ID { get; set; }

        public int? UsageProcessor { get; set; }

        public int? IdleProcessor { get; set; }

        public int? ThreadsProcessor { get; set; }

        public int? TotalProcessor { get; set; }

    }
}
