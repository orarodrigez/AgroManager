using System;
using System.ComponentModel.DataAnnotations;

namespace AgroManager.Models.Domain
{
    public class ControllerDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateTime InstallationDate { get; set; }
        public string FirmwareVersion { get; set; } = string.Empty;
      
        
        public string? Type { get; set; } // e.g., "Irrigation", "Sensor"

        public string? SerialNumber { get; set; }

        public Guid? FieldId { get; set; }
        public Field? Field { get; set; }
        // Ownership boundary (row-level security)
        public Guid OwnerUserId { get; set; }

    }
}
