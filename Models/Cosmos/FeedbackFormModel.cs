namespace ASP121.Models.Cosmos
{
    public class FeedbackFormModel
    {
        public String Name    { get; set; } = null!;
        public String Message { get; set; } = null!;

        public Guid     id     { get; set; }
        public DateTime moment { get; set; }
        public String   type   { get; set; } = "Feedback";
        public String   partitionKey { get; set; }
    }
}
