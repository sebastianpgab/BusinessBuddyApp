namespace BusinessBuddyApp.Entities
{
    public class Clothe
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Brand { get; set; }
        public string Style { get; set; }
        public int ProductId { get; set; }
        public int? IdCompany { get; set; }
    }
}
