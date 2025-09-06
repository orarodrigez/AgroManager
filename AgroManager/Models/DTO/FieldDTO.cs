using AgroManager.Models.Domain;

namespace AgroManager.Models.DTO
{
    public class FieldDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Area { get; set; } // in hectares
        public string CropType { get; set; }    = string.Empty;
        public DateTime LastIrrigationDate { get; set; }
        public DateTime LastFertilizationDate { get; set; }

        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }
      
    }
    public class AddFieldDTO
    {
       
        public string Name { get; set; }= string.Empty; 
        public double Area { get; set; } // in hectares
        public string CropType { get; set; } = string.Empty;
        public DateTime LastIrrigationDate { get; set; }
        public DateTime LastFertilizationDate { get; set; }

        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }
        
    }
    public class UpdateFieldDTO
    {
        public string Name { get; set; }= string.Empty;
        public double Area { get; set; } // in hectares
        public string CropType { get; set; }   = string.Empty;
        public DateTime LastIrrigationDate { get; set; }
        public DateTime LastFertilizationDate { get; set; }
        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }
       
    }
}
