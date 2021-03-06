// <auto-generated />
using System;
using Avander.VehicleApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Avander.VehicleApp.Persistence.Migrations
{
    [DbContext(typeof(VehicleDbContext))]
    [Migration("20211230102228_Modifications")]
    partial class Modifications
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Avander.VehicleApp.Domain.Entities.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Flush")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("Gap")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("MeasurementPointId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementPointId");

                    b.HasIndex("ShopId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Avander.VehicleApp.Domain.Entities.MeasurementPoint", b =>
                {
                    b.Property<int>("MeasurementPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MeasurementPointId");

                    b.ToTable("MeasurementPoints");
                });

            modelBuilder.Entity("Avander.VehicleApp.Domain.Entities.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ShopId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Avander.VehicleApp.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("JSN")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("VehicleModel")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Avander.VehicleApp.Domain.Entities.Measurement", b =>
                {
                    b.HasOne("Avander.VehicleApp.Domain.Entities.MeasurementPoint", "MeasurementPoint")
                        .WithMany()
                        .HasForeignKey("MeasurementPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Avander.VehicleApp.Domain.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Avander.VehicleApp.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasurementPoint");

                    b.Navigation("Shop");

                    b.Navigation("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
