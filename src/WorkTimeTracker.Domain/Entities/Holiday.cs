namespace WorkTimeTracker.Domain.Entities
{
    public class Holiday
    {
        public required string Id { get; set; }
        public DateTime Date { get; set; }
        public required string Name { get; set; }
    }
}
