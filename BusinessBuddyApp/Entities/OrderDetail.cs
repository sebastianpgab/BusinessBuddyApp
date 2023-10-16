namespace BusinessBuddyApp.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> OrderProducts { get; set; }

    }
}
