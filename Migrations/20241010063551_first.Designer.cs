﻿// <auto-generated />
using System;
using CisternasGAMC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CisternasGAMC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241010063551_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CisternasGAMC.Model.Cistern", b =>
                {
                    b.Property<byte>("CisternId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("CisternId"));

                    b.Property<short>("Capacity")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("date");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("CisternId");

                    b.ToTable("Cisterns");
                });

            modelBuilder.Entity("CisternasGAMC.Model.Otb", b =>
                {
                    b.Property<short>("OtbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("OtbId"));

                    b.Property<byte>("District")
                        .HasColumnType("tinyint");

                    b.Property<short>("FamilyCount")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("OtbId");

                    b.ToTable("Otbs");
                });

            modelBuilder.Entity("CisternasGAMC.Model.User", b =>
                {
                    b.Property<short>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("date");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CisternasGAMC.Model.WaterDelivery", b =>
                {
                    b.Property<int>("WaterDeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WaterDeliveryId"));

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("CisternId")
                        .HasColumnType("tinyint");

                    b.Property<float>("DeliveredAmount")
                        .HasColumnType("real");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("DeliveryStatus")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<short>("DriverId")
                        .HasColumnType("smallint");

                    b.Property<short>("OtbId")
                        .HasColumnType("smallint");

                    b.HasKey("WaterDeliveryId");

                    b.ToTable("WaterDeliveries");
                });
#pragma warning restore 612, 618
        }
    }
}
