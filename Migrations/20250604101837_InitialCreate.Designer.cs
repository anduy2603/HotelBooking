﻿// <auto-generated />
using System;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelBooking.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250604101837_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BookingNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CancellationReason")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialRequests")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HotelBooking.Models.BookingModification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("Changes")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("BookingModifications");
                });

            modelBuilder.Entity("HotelBooking.Models.CheckInOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActualCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActualCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AdditionalCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("CheckInNotes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CheckOutNotes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Damages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("CheckInOuts");
                });

            modelBuilder.Entity("HotelBooking.Models.EmailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("HotelBooking.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("HotelBooking.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Photos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<string>("Response")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("HotelBooking.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BedType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("MainImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("SeasonalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenities = "",
                            BedType = "Single Bed",
                            Description = "Perfect for solo travelers, featuring a comfortable single bed and modern amenities.",
                            Images = "",
                            IsAvailable = true,
                            MainImage = "/images/room-single.jpg",
                            MaxGuests = 1,
                            Name = "Single Room",
                            Price = 99m,
                            RoomNumber = "101",
                            SeasonalPrice = 0m,
                            Size = 25,
                            Status = 0,
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Amenities = "",
                            BedType = "Double Bed",
                            Description = "Spacious room with two comfortable beds, ideal for couples or friends.",
                            Images = "",
                            IsAvailable = true,
                            MainImage = "/images/room-double.jpg",
                            MaxGuests = 2,
                            Name = "Double Room",
                            Price = 149m,
                            RoomNumber = "201",
                            SeasonalPrice = 0m,
                            Size = 35,
                            Status = 0,
                            Type = 1
                        },
                        new
                        {
                            Id = 3,
                            Amenities = "",
                            BedType = "King Bed",
                            Description = "Luxurious suite with separate living area, king-size bed, and premium amenities.",
                            Images = "",
                            IsAvailable = true,
                            MainImage = "/images/room-suite.jpg",
                            MaxGuests = 2,
                            Name = "Deluxe Suite",
                            Price = 299m,
                            RoomNumber = "301",
                            SeasonalPrice = 0m,
                            Size = 50,
                            Status = 0,
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            Amenities = "",
                            BedType = "2 Queen Beds",
                            Description = "Perfect for families, featuring two queen beds, a spacious living area, and family-friendly amenities.",
                            Images = "",
                            IsAvailable = true,
                            MainImage = "/images/room-family.jpg",
                            MaxGuests = 4,
                            Name = "Family Suite",
                            Price = 399m,
                            RoomNumber = "401",
                            SeasonalPrice = 0m,
                            Size = 65,
                            Status = 0,
                            Type = 2
                        },
                        new
                        {
                            Id = 5,
                            Amenities = "",
                            BedType = "King Bed",
                            Description = "Our most luxurious suite with panoramic views, private balcony, and exclusive access to executive lounge.",
                            Images = "",
                            IsAvailable = true,
                            MainImage = "/images/room-executive.jpg",
                            MaxGuests = 2,
                            Name = "Executive Suite",
                            Price = 499m,
                            RoomNumber = "501",
                            SeasonalPrice = 0m,
                            Size = 75,
                            Status = 0,
                            Type = 3
                        });
                });

            modelBuilder.Entity("HotelBooking.Models.RoomPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Season")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomPrices");
                });

            modelBuilder.Entity("HotelBooking.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@example.com",
                            Password = "admin123",
                            PasswordHash = "ICy5YqxZB1uWSwcVLSNLcA==",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.HasOne("HotelBooking.Models.Guest", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HotelBooking.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelBooking.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HotelBooking.Models.User", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserId1");

                    b.Navigation("Guest");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelBooking.Models.BookingModification", b =>
                {
                    b.HasOne("HotelBooking.Models.Booking", "Booking")
                        .WithMany("Modifications")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("HotelBooking.Models.CheckInOut", b =>
                {
                    b.HasOne("HotelBooking.Models.Booking", "Booking")
                        .WithOne("CheckInOut")
                        .HasForeignKey("HotelBooking.Models.CheckInOut", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("HotelBooking.Models.Guest", b =>
                {
                    b.HasOne("HotelBooking.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotelBooking.Models.Rating", b =>
                {
                    b.HasOne("HotelBooking.Models.Booking", "Booking")
                        .WithOne("Rating")
                        .HasForeignKey("HotelBooking.Models.Rating", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBooking.Models.Room", "Room")
                        .WithMany("Ratings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelBooking.Models.RoomPrice", b =>
                {
                    b.HasOne("HotelBooking.Models.Room", "Room")
                        .WithMany("PriceHistory")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.Navigation("CheckInOut");

                    b.Navigation("Modifications");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("HotelBooking.Models.Guest", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("HotelBooking.Models.Room", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("PriceHistory");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("HotelBooking.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
