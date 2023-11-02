namespace BusinessBuddyApp.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string Color { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime CreateDate { get; set; }
        public Clothe Clothe { get; set; }
        public Mug Mug { get; set; }
        public Other Other { get; set; }

    }
}
