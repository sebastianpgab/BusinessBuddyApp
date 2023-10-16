namespace BusinessBuddyApp.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }

    }
}
