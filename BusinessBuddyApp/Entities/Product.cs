namespace BusinessBuddyApp.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
