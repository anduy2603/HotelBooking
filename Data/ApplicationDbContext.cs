using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;

namespace HotelBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<BookingModification> BookingModifications { get; set; }
        public DbSet<CheckInOut> CheckInOuts { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired();
            });

            // Configure Room entity
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoomNumber).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Amenities).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
                entity.Property(e => e.Images).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            });

            // Configure Booking entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Room)
                    .WithMany(r => r.Bookings)
                    .HasForeignKey(e => e.RoomId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Guest)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(e => e.GuestId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DepositAmount).HasColumnType("decimal(18,2)");
            });

            // Configure BookingModification entity
            modelBuilder.Entity<BookingModification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Booking)
                    .WithMany(b => b.Modifications)
                    .HasForeignKey(e => e.BookingId);
            });

            // Configure CheckInOut entity
            modelBuilder.Entity<CheckInOut>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Booking)
                    .WithOne(b => b.CheckInOut)
                    .HasForeignKey<CheckInOut>(e => e.BookingId);
                entity.Property(e => e.Damages).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            });

            // Configure Rating entity
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Booking)
                    .WithOne(b => b.Rating)
                    .HasForeignKey<Rating>(e => e.BookingId);
                entity.Property(e => e.Photos).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            });

            // Configure RoomPrice entity
            modelBuilder.Entity<RoomPrice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Room)
                    .WithMany(r => r.PriceHistory)
                    .HasForeignKey(e => e.RoomId);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            // Configure EmailTemplate entity
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Subject).IsRequired();
                entity.Property(e => e.Body).IsRequired();
            });

            // Configure Guest entity
            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });

            // Configure relationships
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Modifications)
                .WithOne(m => m.Booking)
                .HasForeignKey(m => m.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.PriceHistory)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123", // Trong thực tế nên mã hóa password
                    PasswordHash = "ICy5YqxZB1uWSwcVLSNLcA==",
                    Email = "admin@example.com",
                    Role = "Admin"
                }
            );

            // Seed rooms
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Single Room",
                    RoomNumber = "101",
                    Type = RoomType.Single,
                    Price = 99,
                    BedType = "Single Bed",
                    MaxGuests = 1,
                    Size = 25,
                    MainImage = "/images/room-single.jpg",
                    Description = "Perfect for solo travelers, featuring a comfortable single bed and modern amenities.",
                    Amenities = new List<string>(),
                    SeasonalPrice = 0,
                    Status = RoomStatus.Available
                },
                new Room
                {
                    Id = 2,
                    Name = "Double Room",
                    RoomNumber = "201",
                    Type = RoomType.Double,
                    Price = 149,
                    BedType = "Double Bed",
                    MaxGuests = 2,
                    Size = 35,
                    MainImage = "/images/room-double.jpg",
                    Description = "Spacious room with two comfortable beds, ideal for couples or friends.",
                    Amenities = new List<string>(),
                    SeasonalPrice = 0,
                    Status = RoomStatus.Available
                },
                new Room
                {
                    Id = 3,
                    Name = "Deluxe Suite",
                    RoomNumber = "301",
                    Type = RoomType.Suite,
                    Price = 299,
                    BedType = "King Bed",
                    MaxGuests = 2,
                    Size = 50,
                    MainImage = "/images/room-suite.jpg",
                    Description = "Luxurious suite with separate living area, king-size bed, and premium amenities.",
                    Amenities = new List<string>(),
                    SeasonalPrice = 0,
                    Status = RoomStatus.Available
                },
                new Room
                {
                    Id = 4,
                    Name = "Family Suite",
                    RoomNumber = "401",
                    Type = RoomType.Suite,
                    Price = 399,
                    BedType = "2 Queen Beds",
                    MaxGuests = 4,
                    Size = 65,
                    MainImage = "/images/room-family.jpg",
                    Description = "Perfect for families, featuring two queen beds, a spacious living area, and family-friendly amenities.",
                    Amenities = new List<string>(),
                    SeasonalPrice = 0,
                    Status = RoomStatus.Available
                },
                new Room
                {
                    Id = 5,
                    Name = "Executive Suite",
                    RoomNumber = "501",
                    Type = RoomType.Deluxe,
                    Price = 499,
                    BedType = "King Bed",
                    MaxGuests = 2,
                    Size = 75,
                    MainImage = "/images/room-executive.jpg",
                    Description = "Our most luxurious suite with panoramic views, private balcony, and exclusive access to executive lounge.",
                    Amenities = new List<string>(),
                    SeasonalPrice = 0,
                    Status = RoomStatus.Available
                }
            );
        }
    }
} 