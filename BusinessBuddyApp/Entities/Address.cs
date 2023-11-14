namespace BusinessBuddyApp.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; } // Optional
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int OrderDetailId { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
