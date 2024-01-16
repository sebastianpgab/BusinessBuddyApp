namespace BusinessBuddyApp.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string NIP { get; set; }
        public bool hasSubscription { get; set; }
    }
}
