﻿// <auto-generated />
using System;
using HotelManagementRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelManagementRepository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220314180751_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelManagementCommon.Entities.BookingDetailsEntity", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookingID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("RoomNo");

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("HotelManagementCommon.Entities.CustomerDetailsEntity", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("HotelManagementCommon.Entities.RoomsEntity", b =>
                {
                    b.Property<int>("RoomNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoomType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomNo");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelManagementCommon.Entities.BookingDetailsEntity", b =>
                {
                    b.HasOne("HotelManagementCommon.Entities.CustomerDetailsEntity", "CustomerDetails")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelManagementCommon.Entities.RoomsEntity", "Rooms")
                        .WithMany()
                        .HasForeignKey("RoomNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerDetails");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
