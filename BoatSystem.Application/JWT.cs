namespace BoatSystem.Infrastructure
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInDays { get; set; }
        public int DurationInMinutes { get; set; } // Ensure this property exists

    }
}
