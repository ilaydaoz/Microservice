﻿// <auto-generated />
using System;
using CasgemMicroservice.Services.Cargo.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CasgemMicroservice.Services.Cargo.DataAccessLayer.Migrations
{
    [DbContext(typeof(CargoContext))]
    partial class CargoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CasgemMicroservice.Services.Cargo.EntityLayer.Entities.CargoDetail", b =>
                {
                    b.Property<int>("CargoDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoDetailId"), 1L, 1);

                    b.Property<string>("CargoStateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CargoStatesCargoStateID")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("CargoDetailId");

                    b.HasIndex("CargoStatesCargoStateID");

                    b.ToTable("CargoDetails");
                });

            modelBuilder.Entity("CasgemMicroservice.Services.Cargo.EntityLayer.Entities.CargoState", b =>
                {
                    b.Property<int>("CargoStateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoStateID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CargoStateID");

                    b.ToTable("CargoStates");
                });

            modelBuilder.Entity("CasgemMicroservice.Services.Cargo.EntityLayer.Entities.CargoDetail", b =>
                {
                    b.HasOne("CasgemMicroservice.Services.Cargo.EntityLayer.Entities.CargoState", "CargoStates")
                        .WithMany("CargoDetails")
                        .HasForeignKey("CargoStatesCargoStateID");

                    b.Navigation("CargoStates");
                });

            modelBuilder.Entity("CasgemMicroservice.Services.Cargo.EntityLayer.Entities.CargoState", b =>
                {
                    b.Navigation("CargoDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
