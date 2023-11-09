using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BusinessBuddyApp.Entities
{
    public class Client
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }
        public string TaxNumber { get; set; }
        [RegularExpression(@"^(\+48)? ?(\d{3}[ -]?\d{3}[ -]?\d{3})$", ErrorMessage = "Incorrect phone number.")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
