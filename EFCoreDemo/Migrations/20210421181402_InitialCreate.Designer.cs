﻿// <auto-generated />
using System;
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreDemo.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210421181402_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreDemo.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CompanyId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FoundationDate")
                        .HasColumnType("date")
                        .HasColumnName("FoundationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Name");

                    b.Property<decimal?>("Revenue")
                        .HasColumnType("money")
                        .HasColumnName("Revenue");

                    b.HasKey("Id");

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FoundationDate = new DateTime(1998, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Google"
                        },
                        new
                        {
                            Id = 2,
                            FoundationDate = new DateTime(1975, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Microsoft"
                        });
                });

            modelBuilder.Entity("EFCoreDemo.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Laptop"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Phone"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Headphones"
                        });
                });

            modelBuilder.Entity("EFCoreDemo.Entities.SupplyHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SupplyHistoryId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("Price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShipmentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ShipmentDate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProductId");

                    b.ToTable("SupplyHistory");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("HiredDate")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("HiredDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserProfileId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("About");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageUrl");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Supply", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Supply");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.SupplyHistory", b =>
                {
                    b.HasOne("EFCoreDemo.Entities.Company", "Company")
                        .WithMany("SupplyHistory")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCoreDemo.Entities.Product", "Product")
                        .WithMany("SupplyHistory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.User", b =>
                {
                    b.HasOne("EFCoreDemo.Entities.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.UserProfile", b =>
                {
                    b.HasOne("EFCoreDemo.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("EFCoreDemo.Entities.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Supply", b =>
                {
                    b.HasOne("EFCoreDemo.Entities.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCoreDemo.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCoreDemo.Entities.Company", b =>
                {
                    b.Navigation("SupplyHistory");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.Product", b =>
                {
                    b.Navigation("SupplyHistory");
                });

            modelBuilder.Entity("EFCoreDemo.Entities.User", b =>
                {
                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}