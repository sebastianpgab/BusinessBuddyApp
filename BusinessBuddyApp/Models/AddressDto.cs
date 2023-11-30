namespace BusinessBuddyApp.Models
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int ClientId { get; set; }
        public int OrderDetailId { get; set; }

    }
}
