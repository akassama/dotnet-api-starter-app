using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetApiStarterApp.Models.Vehicle
{
    [Table("Vehicles")]
    public class VehicleModel
    {
        [Key]
        public Guid VehicleId { get; set; }

        public Guid UploadedBy { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public string? Color { get; set; }

        public int Mileage { get; set; }

        public double Price { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
