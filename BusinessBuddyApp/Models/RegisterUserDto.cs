using BusinessBuddyApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusinessBuddyApp.Models
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }

    }
}
