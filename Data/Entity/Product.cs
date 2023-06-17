namespace ASP121.Data.Entity
{
    public class Product
    {
        public Guid      Id             { get; set; }
        public Guid      ProductGroupId { get; set; }
        public String    Title          { get; set; } = null!;
        public String?   Description    { get; set; }
        public DateTime  CreateDt       { get; set; }
        public DateTime? DeleteDt       { get; set; }
        public String    ImageUrl       { get; set; } = null!;
        public float     Price          { get; set; }

        // Navigation property
        public List<Rate> Rates { get; set; } = null!;
    }
}
