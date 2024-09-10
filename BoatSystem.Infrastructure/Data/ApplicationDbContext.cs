namespace BoatSystem.Infrastructure.Data
{
    using BoatRentalSystem.Core.Entities;
    using BoatSystem.Core.Entities;
    using BoatSystem.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationAddition> ReservationAdditions { get; set; }
        public DbSet<Addition> Additions { get; set; }
        public DbSet<BoatBooking> BoatBookings { get; set; }
        public DbSet<BookingAddition> BookingAdditions { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.ApplicationUser)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(c => c.ApplicationUser)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Boat>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(b => b.Owner)
                      .WithMany(o => o.Boats)
                      .HasForeignKey(b => b.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(t => t.Owner)
                      .WithMany(o => o.Trips)
                      .HasForeignKey(t => t.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Boat)
                      .WithMany(b => b.Trips)
                      .HasForeignKey(t => t.BoatId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Define relationship with Customer
                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Reservations)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Define relationship with Trip
                entity.HasOne(r => r.Trip)
                      .WithMany(t => t.Reservations)
                      .HasForeignKey(r => r.TripId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Define relationship with Boat
                entity.HasOne(r => r.Boat)
                      .WithMany(b => b.Reservations)
                      .HasForeignKey(r => r.BoatId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Convert Status enum to string in database
                entity.Property(r => r.Status)
                      .HasConversion(
                          v => v.ToString(),  // Convert enum to string for storage
                          v => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), v)  // Convert string to enum for retrieval
                      );
            });

            modelBuilder.Entity<ReservationAddition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(ra => ra.Reservation)
                      .WithMany(r => r.ReservationAdditions)
                      .HasForeignKey(ra => ra.ReservationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ra => ra.Addition)
                      .WithMany(a => a.ReservationAdditions)
                      .HasForeignKey(ra => ra.AdditionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BoatBooking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(bb => bb.Customer)
                      .WithMany(c => c.BoatBookings)
                      .HasForeignKey(bb => bb.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(bb => bb.Boat)
                      .WithMany(b => b.BoatBookings)
                      .HasForeignKey(bb => bb.BoatId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BookingAddition>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(ba => ba.BoatBooking)
                      .WithMany(bb => bb.BookingAdditions)
                      .HasForeignKey(ba => ba.BookingId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ba => ba.Addition)
                      .WithMany(a => a.BookingAdditions)
                      .HasForeignKey(ba => ba.AdditionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Cancellation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(c => c.Customer)
                      .WithMany(c => c.Cancellations)
                      .HasForeignKey(c => c.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Reservation)
                      .WithMany()
                      .HasForeignKey(c => c.ReservationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.BoatBooking)
                      .WithMany()
                      .HasForeignKey(c => c.BookingId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasData(
                    new IdentityRole { Id = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "f117b498-2e53-4686-86dc-d3c13072850e", Name = "Customer", NormalizedName = "CUSTOMER" },
                    new IdentityRole { Id = "936c5f84-e463-49c2-bb6a-93347bbd5103", Name = "Owner", NormalizedName = "OWNER" }
                );
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "Ziad",
                    NormalizedUserName = "ZIAD",
                    Email = "ziadhani64@gmail.com",
                    NormalizedEmail = "ZIADHANI64@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "Nour",
                    NormalizedUserName = "NOUR",
                    Email = "Nour@gmail.com",
                    NormalizedEmail = "NOUR@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "Ahmed",
                    NormalizedUserName = "AHMED",
                    Email = "Ahmed@gmail.com",
                    NormalizedEmail = "AHMED@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a", UserId = "1" },
                new IdentityUserRole<string> { RoleId = "f117b498-2e53-4686-86dc-d3c13072850e", UserId = "2" },
                new IdentityUserRole<string> { RoleId = "936c5f84-e463-49c2-bb6a-93347bbd5103", UserId = "3" }
            );
        }
    }
}





//namespace BoatSystem.Infrastructure.Data
//{
//    using BoatRentalSystem.Core.Entities;
//    using BoatSystem.Core.Entities;
//    using BoatSystem.Core.Models;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore;

//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

//        public DbSet<City> Cities { get; set; }
//        public DbSet<Country> Countries { get; set; }
//        public DbSet<Owner> Owners { get; set; }
//        public DbSet<Customer> Customers { get; set; }
//        public DbSet<Boat> Boats { get; set; }
//        public DbSet<Trip> Trips { get; set; }
//        public DbSet<Reservation> Reservations { get; set; }
//        public DbSet<ReservationAddition> ReservationAdditions { get; set; }
//        public DbSet<Addition> Additions { get; set; }
//        public DbSet<BoatBooking> BoatBookings { get; set; }
//        public DbSet<BookingAddition> BookingAdditions { get; set; }
//        public DbSet<Cancellation> Cancellations { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<City>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//            });

//            modelBuilder.Entity<Owner>(entity =>
//            {
//                entity.HasKey(e => e.Id);

//                entity.HasOne(e => e.ApplicationUser)
//                      .WithMany()
//                      .HasForeignKey(e => e.UserId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<Customer>(entity =>
//            {
//                entity.HasKey(e => e.Id);

//                entity.HasOne(c => c.ApplicationUser)
//                      .WithMany()
//                      .HasForeignKey(c => c.UserId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<Boat>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.HasOne(b => b.Owner)
//                      .WithMany(o => o.Boats)
//                      .HasForeignKey(b => b.OwnerId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<Trip>(entity =>
//            {
//                entity.HasKey(e => e.Id);

//                entity.HasOne(t => t.Owner)
//                      .WithMany(o => o.Trips)
//                      .HasForeignKey(t => t.OwnerId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(t => t.Boat)
//                      .WithMany(b => b.Trips)
//                      .HasForeignKey(t => t.BoatId)
//                      .OnDelete(DeleteBehavior.Cascade);
//            });
//            modelBuilder.Entity<Reservation>(entity =>
//            {
//                entity.HasKey(e => e.Id);

//                // تعريف العلاقة مع Customer
//                entity.HasOne(r => r.Customer)
//                      .WithMany(c => c.Reservations)
//                      .HasForeignKey(r => r.CustomerId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                // تعريف العلاقة مع Trip
//                entity.HasOne(r => r.Trip)
//                      .WithMany(t => t.Reservations)
//                      .HasForeignKey(r => r.TripId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                // تعريف العلاقة مع Boat
//                entity.HasOne(r => r.Boat)
//                      .WithMany(b => b.Reservations)
//                      .HasForeignKey(r => r.BoatId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                // تحويل الحقل Status من Enum إلى String في قاعدة البيانات
//                entity.Property(r => r.Status)
//                      .HasConversion(
//                          v => v.ToString(),   // تحويل القيمة إلى String عند التخزين
//                          v => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), v)  // تحويل القيمة من String إلى Enum عند الاسترجاع
//                      );
//            });
//            modelBuilder.Entity<ReservationAddition>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.HasOne(ra => ra.Reservation)
//                      .WithMany(r => r.ReservationAdditions)
//                      .HasForeignKey(ra => ra.ReservationId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(ra => ra.Addition)
//                      .WithMany(a => a.ReservationAdditions)
//                      .HasForeignKey(ra => ra.AdditionId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<BoatBooking>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.HasOne(bb => bb.Customer)
//                      .WithMany(c => c.BoatBookings)
//                      .HasForeignKey(bb => bb.CustomerId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(bb => bb.Boat)
//                      .WithMany(b => b.BoatBookings)
//                      .HasForeignKey(bb => bb.BoatId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<BookingAddition>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.HasOne(ba => ba.BoatBooking)
//                      .WithMany(bb => bb.BookingAdditions)
//                      .HasForeignKey(ba => ba.BookingId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(ba => ba.Addition)
//                      .WithMany(a => a.BookingAdditions)
//                      .HasForeignKey(ba => ba.AdditionId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<Cancellation>(entity =>
//            {
//                entity.HasKey(e => e.Id);
//                entity.HasOne(c => c.Customer)
//                      .WithMany(c => c.Cancellations)
//                      .HasForeignKey(c => c.CustomerId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(c => c.Reservation)
//                      .WithMany()
//                      .HasForeignKey(c => c.ReservationId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(c => c.BoatBooking)
//                      .WithMany()
//                      .HasForeignKey(c => c.BookingId)
//                      .OnDelete(DeleteBehavior.Restrict);
//            });

//            modelBuilder.Entity<IdentityRole>(entity =>
//            {
//                entity.HasData(
//                    new IdentityRole { Id = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a", Name = "Admin", NormalizedName = "ADMIN" },
//                    new IdentityRole { Id = "f117b498-2e53-4686-86dc-d3c13072850e", Name = "Customer", NormalizedName = "CUSTOMER" },
//                    new IdentityRole { Id = "936c5f84-e463-49c2-bb6a-93347bbd5103", Name = "Owner", NormalizedName = "OWNER" }
//                );
//            });



//            var hasher = new PasswordHasher<ApplicationUser>();
//            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = "1",
//                UserName = "Ziad",
//                NormalizedUserName = "ZIAD",
//                Email = "ziadhani64@gmail.com",
//                NormalizedEmail = "ZIADHANI64@GMAIL.COM",
//                EmailConfirmed = true,
//                PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                SecurityStamp = Guid.NewGuid().ToString()
//            });
//            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = "2",
//                UserName = "Nour",
//                NormalizedUserName = "NOUR",
//                Email = "Nour@gmail.com",
//                NormalizedEmail = "NOUR@GMAIL.COM",
//                EmailConfirmed = true,
//                PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                SecurityStamp = Guid.NewGuid().ToString()
//            });
//            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = "3",
//                UserName = "Ahmed",
//                NormalizedUserName = "AHMED",
//                Email = "Ahmed@gmail.com",
//                NormalizedEmail = "AHMED@GMAIL.COM",
//                EmailConfirmed = true,
//                PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                SecurityStamp = Guid.NewGuid().ToString()
//            });


//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a",
//                UserId = "1"
//            });
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = "f117b498-2e53-4686-86dc-d3c13072850e",
//                UserId = "2"
//            });
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = "936c5f84-e463-49c2-bb6a-93347bbd5103",
//                UserId = "3"
//            });



//        }
//    }
//}
