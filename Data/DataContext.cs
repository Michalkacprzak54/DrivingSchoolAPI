using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Entities;

namespace DrivingSchoolAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<City> City{ get; set; }
        public DbSet<ZipCode> ZipCode{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .ToTable("klient")
                .HasKey(c => c.IdClient);


            modelBuilder.Entity<City>()
                .ToTable("miasto")
                .HasKey(city => city.IdCity);

            modelBuilder.Entity<ZipCode>()
                .ToTable("kod_pocztowy")
                .HasKey(z => z.IdZipCode);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.City)
                .WithMany()
                .HasForeignKey(c => c.ClientIdCity);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.ZipCode)
                .WithMany()
                .HasForeignKey(c => c.ClientIdZipCode);

        }
        
    }
}
