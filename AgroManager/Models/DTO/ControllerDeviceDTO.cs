using AgroManager.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AgroManager.Models.DTO
{
    public class ControllerDeviceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; }
        public string FirmwareVersion { get; set; }= string.Empty;


        public string? Type { get; set; } // e.g., "Irrigation", "Sensor"

        public string? SerialNumber { get; set; }

     
        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }
    }
    public class AddControllerDeviceDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; }
        public string FirmwareVersion { get; set; }= string.Empty;
        public Guid OwnerUserId { get; set; }
    }
    public class UpdateControllerDeviceDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; }
        public string FirmwareVersion { get; set; } = string.Empty;
        public Guid OwnerUserId { get; set; }
    }
}
