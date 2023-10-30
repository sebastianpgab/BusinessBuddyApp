namespace BusinessBuddyApp.Entities
{
    public class Mug
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public double Capacity { get; set; }
        public bool IsMicrowaveSafe { get; set; }
        public bool IsDishwasherSafe { get; set; }
        public int ProductId { get; set; } // Klucz obcy

    }
}
