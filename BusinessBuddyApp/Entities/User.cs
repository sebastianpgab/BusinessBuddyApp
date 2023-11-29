using System.ComponentModel.DataAnnotations;

namespace BusinessBuddyApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; } = 1;
        public virtual Role? Role { get; set; }
    }
}
