using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Models
{
    public class PurchaseDto
    {
        public ClientDto ClientDto { get; set; }
        public AddressDto AddressDto { get; set; }
        public OrderDto OrderDto { get; set; }
        public OrderDetailDto OrderDetailDto { get; set; }  
        public OrderProductDto OrderProductDto { get; set; }
    }
}
