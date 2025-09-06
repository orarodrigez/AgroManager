namespace AgroManager.Models.Domain
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Area { get; set; } // in hectares
        public string CropType { get; set; } = string.Empty;
        public DateTime LastIrrigationDate { get; set; }
        public DateTime LastFertilizationDate { get; set; }

        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }
    

    }
}
