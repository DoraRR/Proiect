﻿// <auto-generated />
using GymManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymManager.Migrations
{
    [DbContext(typeof(GymManagerContext))]
    [Migration("20230111142612_AddEmail")]
    partial class AddEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GymManager.Models.GymUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AspNetUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GymUser");
                });

            modelBuilder.Entity("GymManager.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("GymManager.Models.WorkoutPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkoutPlan");
                });

            modelBuilder.Entity("GymManager.Models.WorkoutWorkoutPlans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutWorkoutPlans");
                });

            modelBuilder.Entity("GymUserWorkoutPlan", b =>
                {
                    b.Property<int>("GymUsersId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutPlansId")
                        .HasColumnType("int");

                    b.HasKey("GymUsersId", "WorkoutPlansId");

                    b.HasIndex("WorkoutPlansId");

                    b.ToTable("GymUserWorkoutPlan");
                });

            modelBuilder.Entity("GymManager.Models.WorkoutWorkoutPlans", b =>
                {
                    b.HasOne("GymManager.Models.Workout", "Workout")
                        .WithMany("WorkoutPlans")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymManager.Models.WorkoutPlan", "WorkoutPlan")
                        .WithMany("Workouts")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("GymUserWorkoutPlan", b =>
                {
                    b.HasOne("GymManager.Models.GymUser", null)
                        .WithMany()
                        .HasForeignKey("GymUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymManager.Models.WorkoutPlan", null)
                        .WithMany()
                        .HasForeignKey("WorkoutPlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymManager.Models.Workout", b =>
                {
                    b.Navigation("WorkoutPlans");
                });

            modelBuilder.Entity("GymManager.Models.WorkoutPlan", b =>
                {
                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}