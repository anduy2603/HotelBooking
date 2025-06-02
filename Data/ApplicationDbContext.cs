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

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Room entity
            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,2)");

            // Configure Booking entity
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId);

            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123", // Trong thực tế nên mã hóa password
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
                    Description = "Perfect for solo travelers, featuring a comfortable single bed and modern amenities."
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
                    Description = "Spacious room with two comfortable beds, ideal for couples or friends."
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
                    Description = "Luxurious suite with separate living area, king-size bed, and premium amenities."
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
                    Description = "Perfect for families, featuring two queen beds, a spacious living area, and family-friendly amenities."
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
                    Description = "Our most luxurious suite with panoramic views, private balcony, and exclusive access to executive lounge."
                }
            );
        }
    }
} 