using Microsoft.EntityFrameworkCore;
using NetApiStarterApp.Models.Vehicle;

namespace NetApiStarterApp.Data
{
    public class DataConnection : DbContext
    {
        public DataConnection(DbContextOptions<DataConnection> options) : base(options)
        {
        }

        public DbSet<VehicleModel> Vehicles { get; set; }

        //Override the OnConfiguring method to read the connection string of database from SQL server via appsettings.json file
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DataConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

}
