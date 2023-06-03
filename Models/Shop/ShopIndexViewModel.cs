namespace ASP121.Models.Shop
{
    public class ShopIndexViewModel
    {
        public List<Data.Entity.ProductGroup> ProductGroups { get; set; }
        public List<Data.Entity.Product> Products { get; set; }
        public String? AddMessage { get; set; }
    }
}
