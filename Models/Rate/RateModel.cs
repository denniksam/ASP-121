namespace ASP121.Models.Rate
{
    public class RateModel
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
    }
}
