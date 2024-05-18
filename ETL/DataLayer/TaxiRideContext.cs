using ETL.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETL.DataLayer
{
    public class TaxiRideContext : DbContext
    {
        public DbSet<TaxiRide> TaxiRides { get; set; }

        string connectionString = @"Server=(localdb)\local;Database=TaxiData;Integrated Security=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxiRide>()
                .HasKey(tr => tr.Id);
        }
    }
}
