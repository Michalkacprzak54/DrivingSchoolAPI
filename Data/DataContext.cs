using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPI.Entities;

namespace DrivingSchoolAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<City> Cities{ get; set; }
        public DbSet<ZipCode> ZipCodes{ get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<Entitlement> Entitlements{ get; set; }


        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientLogin> ClientLogins { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ServicePromotion> ServicePromotions { get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<ClientService> ClientServices{ get; set; }
        public DbSet<Instructor> Instructors{ get; set; }
        public DbSet<InstructorDetails> InstructorDetails { get; set; }
        public DbSet<TraineeCourse> TraineeCourses { get; set; }
        public DbSet<Invoice> Invocies { get; set; }
        public DbSet<InvoiceItem> InvocieItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CourseDetails> CourseDetails { get; set; }
        public DbSet<InscrutorEntitlement> InscrutorEntitlements { get; set; }
        public DbSet<TheorySchedule> TheorySchedules { get; set; }
        public DbSet<LecturePresence> LecturePresences { get; set; }
        public DbSet<Pratice> Pratices { get; set; }
        public DbSet<PraticeSchedule> PraticeSchedules { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TABELE SŁOWNIKOWE
            ///////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<City>()
                .ToTable("miasto")
                .HasKey(city => city.IdCity);

            modelBuilder.Entity<Status>()
                .ToTable("status")
                .HasKey(s => s.IdStatus);

            modelBuilder.Entity<ZipCode>()
                .ToTable("kod_pocztowy")
                .HasKey(z => z.IdZipCode);

            modelBuilder.Entity<Entitlement>()
                .ToTable("uprawnienia")
                .HasKey(z => z.IdEntitlement);

            ///////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Client>()
                .ToTable("klient")
                .HasKey(c => c.IdClient);

            modelBuilder.Entity<ClientLogin>()
                .ToTable("login_klient")
                .HasKey(cl => cl.IdClientLogin);

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

            modelBuilder.Entity<TraineeCourse>()
                .ToTable("kursant_kurs")
                .HasKey(tc => tc.IdTraineeCourse);

            modelBuilder.Entity<Promotion>()
                .ToTable("promocja")
                .HasKey(p => p.IdPromotion);

            modelBuilder.Entity<ServicePromotion>()
                .ToTable("usluga_promocja")
                .HasKey(p => p.IdServicePromotion);

            modelBuilder.Entity<Invoice>()
                .ToTable("faktura")
                .HasKey(i => i.IdInvocie);

            modelBuilder.Entity<InvoiceItem>()
                .ToTable("pozycja_faktura")
                .HasKey(it => it.IdInvoiceItem);

            modelBuilder.Entity<Payment>()
                .ToTable("platnosc")
                .HasKey(p => p.IdPayment);

            modelBuilder.Entity<CourseDetails>()
                .ToTable("szczegoly_kurs")
                .HasKey(cd => cd.IdCourseDetails);

            modelBuilder.Entity<InscrutorEntitlement>()
                .ToTable("instruktor_uprawnienie")
                .HasKey(cd => cd.IdInscrutorEntitlement);

            modelBuilder.Entity<TheorySchedule>()
                .ToTable("harmonogram_wyklad")
                .HasKey(ts => ts.IdTheorySchedule);

            modelBuilder.Entity<LecturePresence>()
                .ToTable("obecnosc_wyklad")
                .HasKey(lp => lp.IdLecturePresence);

            modelBuilder.Entity<Pratice>()
                .ToTable("praktyka")
                .HasKey(p => p.IdPratice);

            modelBuilder.Entity<PraticeSchedule>()
               .ToTable("harmonogram_praktyka")
               .HasKey(p => p.IdPraticeSchedule);

            modelBuilder.Entity<Photo>()
               .ToTable("zdjecie")
               .HasKey(p => p.IdPhoto);

            ///////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Client>()
                .HasOne(c => c.ClientLogin)
                .WithOne()  
                .HasForeignKey<ClientLogin>(cl => cl.IdClient);


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

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.InstructorEntitlements)
                .WithOne(ie => ie.Instructor)
                .HasForeignKey(ie => ie.IdInstructor);

            modelBuilder.Entity<InscrutorEntitlement>()
                .HasOne(ie => ie.Entitlement)
                .WithMany(e => e.InstructorEntitlements)
                .HasForeignKey(ie => ie.IdEntitlement);

            modelBuilder.Entity<InstructorDetails>()
                .HasOne(id => id.City)
                .WithMany()
                .HasForeignKey(id => id.InstructorCityId);

            modelBuilder.Entity<InstructorDetails>()
                .HasOne(id => id.ZipCode)
                .WithMany()
                .HasForeignKey(id => id.InstructorZipCodeId);

            modelBuilder.Entity<TraineeCourse>()
                .HasOne(tc => tc.Client)
                .WithMany()
                .HasForeignKey(tc => tc.IdClient)
                .HasConstraintName("FK_kursant_kurs_klient");

            modelBuilder.Entity<TraineeCourse>()
                .HasOne(tc => tc.ClientService)
                .WithMany()
                .HasForeignKey(tc => tc.IdClientService)
                .HasConstraintName("FK_kursant_kurs_klient_usluga");

            modelBuilder.Entity<TraineeCourse>()
                .HasOne(tc => tc.CourseDetails)
                .WithOne(cd => cd.TraineeCourse)
                .HasForeignKey<CourseDetails>(cd => cd.IdTraineeCourse)
                .HasConstraintName("FK_szczegoly_kurs_kursant_kurs");

            modelBuilder.Entity<TraineeCourse>()
                .HasOne(tc => tc.Status)
                .WithMany()
                .HasForeignKey(tc => tc.IdStatus);

            modelBuilder.Entity<ServicePromotion>()
                .HasOne(sp => sp.Service)
                .WithOne(s => s.ServicePromotion)
                .HasForeignKey<ServicePromotion>(sp => sp.IdService);

            modelBuilder.Entity<ServicePromotion>()
                .HasOne(sp => sp.Promotion)
                .WithOne(p => p.ServicePromotion)
                .HasForeignKey<ServicePromotion>(sp => sp.IdPromotion);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.IdClient);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoviceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.IdInvocie);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.IdInvoice);

            

            modelBuilder.Entity<Service>()
                .HasMany(s => s.Photos)
                .WithOne(p => p.Servcie)
                .HasForeignKey(p => p.IdService);

            




        }

    }
}
