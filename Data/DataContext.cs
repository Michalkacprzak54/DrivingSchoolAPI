﻿using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Entities;

namespace DrivingSchoolAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<ZipCode> ZipCodes{ get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<ClientService> ClientServices{ get; set; }
        public DbSet<Instructor> Instructors{ get; set; }
        public DbSet<InstructorDetails> InstructorDetails { get; set; }

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

            modelBuilder.Entity<Service>()
                .ToTable("usluga")
                .HasKey(s => s.IdService);

            modelBuilder.Entity<ClientService>()
                .ToTable("klient_usluga")
                .HasKey(cs => cs.IdClientService);

            modelBuilder.Entity<Instructor>()
                .ToTable("instruktor")
                .HasKey(i => i.IdInstructor);

            modelBuilder.Entity<InstructorDetails>()
                .ToTable("szczegoly_instruktor")
                .HasKey(id => id.IdInstructorDetails);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.City)
                .WithMany()
                .HasForeignKey(c => c.ClientIdCity);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.ZipCode)
                .WithMany()
                .HasForeignKey(c => c.ClientIdZipCode);

            modelBuilder.Entity<ClientService>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.ClientServices)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<ClientService>()
                .HasOne(cs => cs.Service)
                .WithMany()
                .HasForeignKey(cs => cs.ServiceId);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.InstructorDetails)
                .WithOne(d => d.Instructor)
                .HasForeignKey<InstructorDetails>(d => d.IdInstructor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstructorDetails>()
                .HasOne(id => id.City)
                .WithMany()
                .HasForeignKey(id => id.InstructorCityId);

            modelBuilder.Entity<InstructorDetails>()
                .HasOne(id => id.ZipCode)
                .WithMany()
                .HasForeignKey(id => id.InstructorZipCodeId);



        }

    }
}
