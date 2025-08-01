﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Database;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250708135526_AdminData")]
    partial class AdminData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.Database.Entites.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("WorkShopId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("WorkShopId");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("63418e00-38f8-4228-ac4b-d0a6a4a61e37"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2918),
                            Email = "email@gmail.com",
                            Password = "$2a$11$8kGJAGweY0hhIeCEmX7Rp.b1rRL5YBBF3r3Xo/ztzQ.oo4ZDj/fFa",
                            WorkShopId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                        },
                        new
                        {
                            Id = new Guid("b559804b-a346-4d08-9456-02725d865b9c"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 632, DateTimeKind.Utc).AddTicks(3131),
                            Email = "email2@gmail.com",
                            Password = "$2a$11$ZkA4c24Xfu0mabG9FFh52ugt2UL2RA/QeQI/hMx6Fg918j1gEiHea",
                            WorkShopId = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
                        });
                });

            modelBuilder.Entity("backend.Database.Entites.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Dni")
                        .HasColumnType("text");

                    b.Property<string[]>("Email")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("WorkShopId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkShopId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 804, DateTimeKind.Utc).AddTicks(3438),
                            Email = new[] { "lucaspirez42@gmail.com" },
                            FirstName = "Juan ",
                            LastName = "Perez",
                            PhoneNumber = new[] { "3424388239" },
                            WorkShopId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                        },
                        new
                        {
                            Id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 804, DateTimeKind.Utc).AddTicks(3482),
                            Email = new string[0],
                            FirstName = "Maria ",
                            LastName = "Lopez",
                            PhoneNumber = new string[0],
                            WorkShopId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                        });
                });

            modelBuilder.Entity("backend.Database.Entites.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Method")
                        .HasColumnType("text");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("backend.Database.Entites.RepairOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Budget")
                        .HasColumnType("double precision");

                    b.Property<string>("Cause")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("FinalAmount")
                        .HasColumnType("double precision");

                    b.Property<bool>("NotifycationSent")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReceptionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkShopId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.HasIndex("WorkShopId");

                    b.ToTable("VehicleEntries");
                });

            modelBuilder.Entity("backend.Database.Entites.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("backend.Database.Entites.WorkShop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("WorkShops");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2747),
                            Name = "Taller Jesus"
                        },
                        new
                        {
                            Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                            CreatedAt = new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2774),
                            Name = "Taller Silvana"
                        });
                });

            modelBuilder.Entity("backend.Database.Entites.Admin", b =>
                {
                    b.HasOne("backend.Database.Entites.WorkShop", "WorkShop")
                        .WithMany()
                        .HasForeignKey("WorkShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkShop");
                });

            modelBuilder.Entity("backend.Database.Entites.Customer", b =>
                {
                    b.HasOne("backend.Database.Entites.WorkShop", "WorkShop")
                        .WithMany("Customers")
                        .HasForeignKey("WorkShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkShop");
                });

            modelBuilder.Entity("backend.Database.Entites.Payment", b =>
                {
                    b.HasOne("backend.Database.Entites.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("backend.Database.Entites.RepairOrder", b =>
                {
                    b.HasOne("backend.Database.Entites.Vehicle", "Vehicle")
                        .WithMany("VehicleEntries")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Database.Entites.WorkShop", "WorkShop")
                        .WithMany("VehicleEntries")
                        .HasForeignKey("WorkShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("backend.Database.Entites.SparePart", "SpareParts", b1 =>
                        {
                            b1.Property<Guid>("RepairOrderId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<double>("Price")
                                .HasColumnType("double precision");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.HasKey("RepairOrderId", "Id");

                            b1.ToTable("SparePart");

                            b1.WithOwner()
                                .HasForeignKey("RepairOrderId");
                        });

                    b.Navigation("SpareParts");

                    b.Navigation("Vehicle");

                    b.Navigation("WorkShop");
                });

            modelBuilder.Entity("backend.Database.Entites.Vehicle", b =>
                {
                    b.HasOne("backend.Database.Entites.Customer", "Customer")
                        .WithMany("Vehicle")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("backend.Database.Entites.Customer", b =>
                {
                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("backend.Database.Entites.Vehicle", b =>
                {
                    b.Navigation("VehicleEntries");
                });

            modelBuilder.Entity("backend.Database.Entites.WorkShop", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("VehicleEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
