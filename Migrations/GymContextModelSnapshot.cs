﻿// <auto-generated />
using System;
using HomeWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeWork.Migrations
{
    [DbContext(typeof(GymContext))]
    partial class GymContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HomeWork.Models.Abonement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CurrentTrainings")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id_BasicGroup")
                        .HasColumnType("int");

                    b.Property<int>("Id_Client")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("TotalPayed")
                        .HasColumnType("float");

                    b.Property<int>("TotalTrainings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Abonements");
                });

            modelBuilder.Entity("HomeWork.Models.BasicGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Costs")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("MaxClients")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BasicGroups");
                });

            modelBuilder.Entity("HomeWork.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("CHAR(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HomeWork.Models.TrainGroup", b =>
                {
                    b.Property<int>("Id_Client")
                        .HasColumnType("int");

                    b.Property<int>("Id_Training")
                        .HasColumnType("int");

                    b.HasKey("Id_Client", "Id_Training");

                    b.HasIndex("Id_Training");

                    b.ToTable("TrainGroups");
                });

            modelBuilder.Entity("HomeWork.Models.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("HomeWork.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Id_BasicGroup")
                        .HasColumnType("int");

                    b.Property<int>("Id_Creator")
                        .HasColumnType("int");

                    b.Property<int>("Id_Trainer")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("HomeWork.Models.TrainGroup", b =>
                {
                    b.HasOne("HomeWork.Models.Client", "Client")
                        .WithMany("TrainGroups")
                        .HasForeignKey("Id_Client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeWork.Models.Training", "Training")
                        .WithMany("TrainGroups")
                        .HasForeignKey("Id_Training")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
