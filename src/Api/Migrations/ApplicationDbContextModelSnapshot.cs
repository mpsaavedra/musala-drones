﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Musala.Drones.Api.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Musala.Drones.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Musala.Drones.Domain.Models.BatteryAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("BatteryCapacity")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "createAt");

                    b.Property<int>("DroneId")
                        .HasColumnType("integer");

                    b.Property<string>("RowVersion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "rowVersion");

                    b.HasKey("Id");

                    b.HasIndex("DroneId");

                    b.ToTable("BatteryAudit");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.Drone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("BatteryCapacity")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "batteryCapacity");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "createAt");

                    b.Property<byte>("Model")
                        .HasColumnType("smallint")
                        .HasAnnotation("Relational:JsonPropertyName", "model");

                    b.Property<string>("RowVersion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "rowVersion");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasAnnotation("Relational:JsonPropertyName", "serialNumber");

                    b.Property<byte>("State")
                        .HasColumnType("smallint")
                        .HasAnnotation("Relational:JsonPropertyName", "state");

                    b.Property<float>("WeightLimit")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "weightLimit");

                    b.HasKey("Id");

                    b.ToTable("Drones");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.DroneCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "createAt");

                    b.Property<int>("DroneId")
                        .HasColumnType("integer");

                    b.Property<string>("RowVersion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "rowVersion");

                    b.HasKey("Id");

                    b.HasIndex("DroneId");

                    b.ToTable("DroneCharges");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "createAt");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("RowVersion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "rowVersion");

                    b.Property<float>("Weight")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "weight");

                    b.HasKey("Id");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.MedicationCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "createAt");

                    b.Property<int>("DroneChargeId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicationId")
                        .HasColumnType("integer");

                    b.Property<string>("RowVersion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "rowVersion");

                    b.HasKey("Id");

                    b.HasIndex("DroneChargeId");

                    b.HasIndex("MedicationId");

                    b.ToTable("MedicationCharges");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.BatteryAudit", b =>
                {
                    b.HasOne("Musala.Drones.Domain.Models.Drone", "Drone")
                        .WithMany("BatteryAudits")
                        .HasForeignKey("DroneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drone");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.DroneCharge", b =>
                {
                    b.HasOne("Musala.Drones.Domain.Models.Drone", "Drone")
                        .WithMany("DroneCharges")
                        .HasForeignKey("DroneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drone");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.MedicationCharge", b =>
                {
                    b.HasOne("Musala.Drones.Domain.Models.DroneCharge", "DroneCharge")
                        .WithMany("MedicationCharges")
                        .HasForeignKey("DroneChargeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Musala.Drones.Domain.Models.Medication", "Medication")
                        .WithMany("MedicationCharges")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DroneCharge");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.Drone", b =>
                {
                    b.Navigation("BatteryAudits");

                    b.Navigation("DroneCharges");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.DroneCharge", b =>
                {
                    b.Navigation("MedicationCharges");
                });

            modelBuilder.Entity("Musala.Drones.Domain.Models.Medication", b =>
                {
                    b.Navigation("MedicationCharges");
                });
#pragma warning restore 612, 618
        }
    }
}
