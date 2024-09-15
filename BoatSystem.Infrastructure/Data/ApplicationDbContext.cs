using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Models;

namespace BoatSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TripBooking> TripBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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


            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasOne(e => e.ApplicationUser)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new Owner
                    {
                        Id = 1,
                        UserId = "3",
                        BusinessName = "Nautical Ventures",
                        Address = "123 Marina Bay",
                        WalletBalance = 1000.00m,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });         
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(c => c.ApplicationUser)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(c => c.Reservations)
                      .WithOne(r => r.Customer)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(c => c.BoatBookings)
                      .WithOne(bb => bb.Customer)
                      .HasForeignKey(bb => bb.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(c => c.Cancellations)
                      .WithOne(c => c.Customer)
                      .HasForeignKey(c => c.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(c => c.TripBookings)
                      .WithOne(tb => tb.Customer)
                      .HasForeignKey(tb => tb.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new Customer
                    {
                        Id = 2,
                        UserId = "3",
                        FirstName = "John",
                        LastName = "Doe",
                        WalletBalance = 500.00m,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });

            modelBuilder.Entity<Boat>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(b => b.Owner)
                      .WithMany(o => o.Boats)
                      .HasForeignKey(b => b.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new Boat
                    {
                        Id = 1,
                        OwnerId = 1,
                        Name = "Sea Breeze",
                        Description = "A beautiful sailboat.",
                        Capacity = 10,
                        IsApproved = true,
                        ReservationPrice = 150.00m,
                        Status = "Available",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Boat
                    {
                        Id = 2,
                        OwnerId = 1,
                        Name = "Ocean Explorer",
                        Description = "A powerful motorboat.",
                        Capacity = 8,
                        IsApproved = true,
                        ReservationPrice = 200.00m,
                        Status = "Available",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
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
                entity.HasData(
                    new Trip
                    {
                        Id = 1,
                        OwnerId = 1,
                        BoatId = 1,
                        Name = "Tropical Getaway",
                        Description = "A relaxing trip to tropical islands.",
                        PricePerPerson = 200.00m,
                        MaxPeople = 10,
                        CancellationDeadline = DateTime.UtcNow.AddDays(3),
                        Status = "Available",
                        StartedAt = DateTime.UtcNow.AddDays(1),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DurationHours = 4
                    },
                    new Trip
                    {
                        Id = 2,
                        OwnerId = 1,
                        BoatId = 2,
                        Name = "Adventure Cruise",
                        Description = "An exciting cruise with lots of adventures.",
                        PricePerPerson = 300.00m,
                        MaxPeople = 8,
                        CancellationDeadline = DateTime.UtcNow.AddDays(5),
                        Status = "Available",
                        StartedAt = DateTime.UtcNow.AddDays(2),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DurationHours = 6
                    }
                );
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Reservations)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Trip)
                      .WithMany(t => t.Reservations)
                      .HasForeignKey(r => r.TripId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Boat)
                      .WithMany(b => b.Reservations)
                      .HasForeignKey(r => r.BoatId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(r => r.Status)
                      .HasConversion(
                          v => v.ToString(),
                          v => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), v)
                      );
                entity.HasData(
                    new Reservation
                    {
                        Id = 1,
                        CustomerId = 2,
                        TripId = 1,
                        BoatId = 1,
                        ReservationDate = DateTime.UtcNow,
                        Status = ReservationStatus.Confirmed,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Reservation
                    {
                        Id = 2,
                        CustomerId = 2,
                        TripId = 2,
                        BoatId = 2,
                        ReservationDate = DateTime.UtcNow,
                        Status = ReservationStatus.Pending,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });

            modelBuilder.Entity<TripBooking>(entity =>
            {
                entity.HasKey(tb => tb.Id);
                entity.HasOne(tb => tb.Customer)
                      .WithMany(c => c.TripBookings)
                      .HasForeignKey(tb => tb.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(tb => tb.Trip)
                      .WithMany(t => t.TripBookings)
                      .HasForeignKey(tb => tb.TripId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new TripBooking
                    {
                        Id = 1,
                        CustomerId = 2,
                        TripId = 1,
                        BookingDate = DateTime.UtcNow,
                        Status = TripBookingStatus.Booked,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new TripBooking
                    {
                        Id = 2,
                        CustomerId = 2,
                        TripId = 2,
                        BookingDate = DateTime.UtcNow,
                        Status = TripBookingStatus.Pending,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });


        }
    }

}


















//namespace BoatSystem.Infrastructure.Data
//{
//    using BoatSystem.Core.Entities;
//    using BoatSystem.Core.Models;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore;

//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
//        public DbSet<Booking> Bookings { get; set; }
//        public DbSet<TripBooking> TripBookings { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

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

//                entity.HasOne(r => r.Customer)
//                      .WithMany(c => c.Reservations)
//                      .HasForeignKey(r => r.CustomerId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(r => r.Trip)
//                      .WithMany(t => t.Reservations)
//                      .HasForeignKey(r => r.TripId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.HasOne(r => r.Boat)
//                      .WithMany(b => b.Reservations)
//                      .HasForeignKey(r => r.BoatId)
//                      .OnDelete(DeleteBehavior.Restrict);

//                entity.Property(r => r.Status)
//                      .HasConversion(
//                          v => v.ToString(),
//                          v => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), v)
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

//            modelBuilder.Entity<ApplicationUser>().HasData(
//                new ApplicationUser
//                {
//                    Id = "1",
//                    UserName = "Ziad",
//                    NormalizedUserName = "ZIAD",
//                    Email = "ziadhani64@gmail.com",
//                    NormalizedEmail = "ZIADHANI64@GMAIL.COM",
//                    EmailConfirmed = true,
//                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                    SecurityStamp = Guid.NewGuid().ToString()
//                },
//                new ApplicationUser
//                {
//                    Id = "2",
//                    UserName = "Nour",
//                    NormalizedUserName = "NOUR",
//                    Email = "Nour@gmail.com",
//                    NormalizedEmail = "NOUR@GMAIL.COM",
//                    EmailConfirmed = true,
//                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                    SecurityStamp = Guid.NewGuid().ToString()
//                },
//                new ApplicationUser
//                {
//                    Id = "3",
//                    UserName = "Ahmed",
//                    NormalizedUserName = "AHMED",
//                    Email = "Ahmed@gmail.com",
//                    NormalizedEmail = "AHMED@GMAIL.COM",
//                    EmailConfirmed = true,
//                    PasswordHash = hasher.HashPassword(null, "Zoz332003##"),
//                    SecurityStamp = Guid.NewGuid().ToString()
//                }
//            );

//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
//                new IdentityUserRole<string> { RoleId = "7bdb9275-8cd4-4d86-bea6-bbdb5125e28a", UserId = "1" },
//                new IdentityUserRole<string> { RoleId = "f117b498-2e53-4686-86dc-d3c13072850e", UserId = "2" },
//                new IdentityUserRole<string> { RoleId = "936c5f84-e463-49c2-bb6a-93347bbd5103", UserId = "3" }
//            );
//        }
//    }
//}
