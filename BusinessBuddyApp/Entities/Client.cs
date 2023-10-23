using System.Net;

namespace BusinessBuddyApp.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
