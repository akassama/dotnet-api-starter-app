namespace NetApiStarterApp.Models.Vehicle
{
    public class VehicleUpdateDto
    {
        public Guid VehicleId { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public string? Color { get; set; }

        public int Mileage { get; set; }

        public double Price { get; set; }

        public string? ImagePath { get; set; }
    }
}
