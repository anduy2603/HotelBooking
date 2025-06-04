using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if we already have rooms
            if (context.Rooms.Any())
            {
                return;   // DB has been seeded
            }

            var rooms = new Room[]
            {
                // Single Rooms
                new Room
                {
                    Name = "Cozy Single Room",
                    Description = "Perfect for solo travelers, featuring a comfortable single bed and essential amenities",
                    RoomNumber = "101",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 80.00m,
                    Size = 20,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-cozy.jpg",
                    Images = new List<string> { "/images/rooms/single-cozy-1.jpg", "/images/rooms/single-cozy-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon" }
                },
                new Room
                {
                    Name = "Deluxe Single Room",
                    Description = "Spacious single room with premium amenities and city view",
                    RoomNumber = "102",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 100.00m,
                    Size = 25,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-deluxe.jpg",
                    Images = new List<string> { "/images/rooms/single-deluxe-1.jpg", "/images/rooms/single-deluxe-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast" }
                },
                new Room
                {
                    Name = "Executive Single Room",
                    Description = "Premium single room with executive lounge access and premium amenities",
                    RoomNumber = "103",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 120.00m,
                    Size = 28,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-executive.jpg",
                    Images = new List<string> { "/images/rooms/single-executive-1.jpg", "/images/rooms/single-executive-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym" }
                },
                new Room
                {
                    Name = "Garden View Single Room",
                    Description = "Peaceful single room overlooking our beautiful garden",
                    RoomNumber = "104",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 90.00m,
                    Size = 22,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-garden.jpg",
                    Images = new List<string> { "/images/rooms/single-garden-1.jpg", "/images/rooms/single-garden-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "breakfast" }
                },
                new Room
                {
                    Name = "City View Single Room",
                    Description = "Single room with stunning city skyline views",
                    RoomNumber = "105",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 95.00m,
                    Size = 23,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-city.jpg",
                    Images = new List<string> { "/images/rooms/single-city-1.jpg", "/images/rooms/single-city-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "breakfast" }
                },
                new Room
                {
                    Name = "Budget Single Room",
                    Description = "Affordable single room with essential amenities",
                    RoomNumber = "106",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 70.00m,
                    Size = 18,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-budget.jpg",
                    Images = new List<string> { "/images/rooms/single-budget-1.jpg", "/images/rooms/single-budget-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon" }
                },
                new Room
                {
                    Name = "Premium Single Room",
                    Description = "Luxurious single room with premium amenities and services",
                    RoomNumber = "107",
                    Type = RoomType.Single,
                    BedType = "Single",
                    MaxGuests = 1,
                    Price = 130.00m,
                    Size = 30,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/single-premium.jpg",
                    Images = new List<string> { "/images/rooms/single-premium-1.jpg", "/images/rooms/single-premium-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym", "spa" }
                },

                // Double Rooms
                new Room
                {
                    Name = "Standard Twin Room",
                    Description = "Comfortable room with two single beds, perfect for friends or colleagues",
                    RoomNumber = "201",
                    Type = RoomType.Double,
                    BedType = "Twin",
                    MaxGuests = 2,
                    Price = 120.00m,
                    Size = 30,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/double-twin.jpg",
                    Images = new List<string> { "/images/rooms/double-twin-1.jpg", "/images/rooms/double-twin-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "breakfast" }
                },
                new Room
                {
                    Name = "Deluxe Double Room",
                    Description = "Spacious double room with a queen-size bed and premium amenities",
                    RoomNumber = "202",
                    Type = RoomType.Double,
                    BedType = "Queen",
                    MaxGuests = 2,
                    Price = 150.00m,
                    Size = 35,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/double-deluxe.jpg",
                    Images = new List<string> { "/images/rooms/double-deluxe-1.jpg", "/images/rooms/double-deluxe-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool" }
                },
                new Room
                {
                    Name = "Executive Double Room",
                    Description = "Luxurious double room with king-size bed and executive lounge access",
                    RoomNumber = "203",
                    Type = RoomType.Double,
                    BedType = "King",
                    MaxGuests = 2,
                    Price = 180.00m,
                    Size = 40,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/double-executive.jpg",
                    Images = new List<string> { "/images/rooms/double-executive-1.jpg", "/images/rooms/double-executive-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym", "spa" }
                },

                // Suites
                new Room
                {
                    Name = "Junior Suite",
                    Description = "Elegant suite with separate living area and premium amenities",
                    RoomNumber = "301",
                    Type = RoomType.Suite,
                    BedType = "King",
                    MaxGuests = 2,
                    Price = 250.00m,
                    Size = 45,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/suite-junior.jpg",
                    Images = new List<string> { "/images/rooms/suite-junior-1.jpg", "/images/rooms/suite-junior-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym" }
                },
                new Room
                {
                    Name = "Executive Suite",
                    Description = "Spacious suite with separate living room, dining area, and premium amenities",
                    RoomNumber = "302",
                    Type = RoomType.Suite,
                    BedType = "King",
                    MaxGuests = 3,
                    Price = 350.00m,
                    Size = 60,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/suite-executive.jpg",
                    Images = new List<string> { "/images/rooms/suite-executive-1.jpg", "/images/rooms/suite-executive-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym", "spa", "restaurant" }
                },
                new Room
                {
                    Name = "Presidential Suite",
                    Description = "Our most luxurious suite with panoramic views, private balcony, and exclusive services",
                    RoomNumber = "303",
                    Type = RoomType.Suite,
                    BedType = "King",
                    MaxGuests = 4,
                    Price = 500.00m,
                    Size = 80,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/suite-presidential.jpg",
                    Images = new List<string> { "/images/rooms/suite-presidential-1.jpg", "/images/rooms/suite-presidential-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym", "spa", "restaurant", "bar", "parking" }
                },

                // Deluxe Rooms
                new Room
                {
                    Name = "Deluxe King Room",
                    Description = "Spacious deluxe room with king-size bed and premium amenities",
                    RoomNumber = "401",
                    Type = RoomType.Deluxe,
                    BedType = "King",
                    MaxGuests = 2,
                    Price = 200.00m,
                    Size = 40,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/deluxe-king.jpg",
                    Images = new List<string> { "/images/rooms/deluxe-king-1.jpg", "/images/rooms/deluxe-king-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool" }
                },
                new Room
                {
                    Name = "Deluxe Twin Room",
                    Description = "Spacious deluxe room with two queen-size beds and premium amenities",
                    RoomNumber = "402",
                    Type = RoomType.Deluxe,
                    BedType = "Queen",
                    MaxGuests = 3,
                    Price = 220.00m,
                    Size = 45,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/deluxe-twin.jpg",
                    Images = new List<string> { "/images/rooms/deluxe-twin-1.jpg", "/images/rooms/deluxe-twin-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym" }
                },
                new Room
                {
                    Name = "Deluxe Family Room",
                    Description = "Perfect for families, featuring two bedrooms and a spacious living area",
                    RoomNumber = "403",
                    Type = RoomType.Deluxe,
                    BedType = "King + Twin",
                    MaxGuests = 4,
                    Price = 280.00m,
                    Size = 65,
                    Status = RoomStatus.Available,
                    MainImage = "/images/rooms/deluxe-family.jpg",
                    Images = new List<string> { "/images/rooms/deluxe-family-1.jpg", "/images/rooms/deluxe-family-2.jpg" },
                    Amenities = new List<string> { "wifi", "tv", "aircon", "minibar", "breakfast", "pool", "gym", "spa" }
                }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    }
} 