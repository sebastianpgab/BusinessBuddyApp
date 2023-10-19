namespace BusinessBuddyApp.Entities
{
    public class Other
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; } // Klucz obcy
        public virtual Product Product { get; set; } // Navigational property

    }
}
