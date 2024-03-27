﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thrifty.Context;

#nullable disable

namespace Thrifty.Migrations
{
    [DbContext(typeof(ThriftyDbContext))]
    [Migration("20230530193816_MigrationV2")]
    partial class MigrationV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Thrifty.Models.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "Amman"
                        },
                        new
                        {
                            id = 2,
                            name = "Aqaba"
                        },
                        new
                        {
                            id = 3,
                            name = "Madaba"
                        },
                        new
                        {
                            id = 4,
                            name = "Irbid"
                        },
                        new
                        {
                            id = 5,
                            name = "Salt"
                        },
                        new
                        {
                            id = 6,
                            name = "Zarqa"
                        },
                        new
                        {
                            id = 7,
                            name = "Jerash"
                        },
                        new
                        {
                            id = 8,
                            name = "Ma'an"
                        },
                        new
                        {
                            id = 9,
                            name = "Al-Mafraq"
                        });
                });

            modelBuilder.Entity("Thrifty.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("int");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("outOfStock")
                        .HasColumnType("bit");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("userId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Thrifty.Models.ItemCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ItemCategory");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "T-Shirt"
                        },
                        new
                        {
                            id = 2,
                            name = "Pants"
                        },
                        new
                        {
                            id = 3,
                            name = "Shorts"
                        },
                        new
                        {
                            id = 4,
                            name = "Dresses"
                        },
                        new
                        {
                            id = 5,
                            name = "Shoes"
                        });
                });

            modelBuilder.Entity("Thrifty.Models.ItemImages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("imageBase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("itemId")
                        .HasColumnType("int");

                    b.Property<bool>("mainImage")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("itemId");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("Thrifty.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ToralPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("statusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Thrifty.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("buyerId")
                        .HasColumnType("int");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("buyerId");

                    b.HasIndex("statusId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Thrifty.Models.OrderStatus", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            id = 2,
                            Name = "Accepted"
                        },
                        new
                        {
                            id = 3,
                            Name = "On The Way"
                        },
                        new
                        {
                            id = 4,
                            Name = "Delivered"
                        },
                        new
                        {
                            id = 5,
                            Name = "Canceled"
                        },
                        new
                        {
                            id = 6,
                            Name = "Rejected"
                        });
                });

            modelBuilder.Entity("Thrifty.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "Seller"
                        },
                        new
                        {
                            id = 2,
                            name = "Admin"
                        },
                        new
                        {
                            id = 3,
                            name = "Customer"
                        });
                });

            modelBuilder.Entity("Thrifty.Models.ShopingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CartNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ShopingCart");
                });

            modelBuilder.Entity("Thrifty.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("cityId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("mobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("cityId");

                    b.HasIndex("roleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            id = 1,
                            cityId = 1,
                            email = "Admin@Thrifty.com",
                            fullName = "Admin",
                            mobileNumber = 10000000000L,
                            password = "123",
                            roleId = 2
                        });
                });

            modelBuilder.Entity("Thrifty.Models.Item", b =>
                {
                    b.HasOne("Thrifty.Models.ItemCategory", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thrifty.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Thrifty.Models.ItemImages", b =>
                {
                    b.HasOne("Thrifty.Models.Item", "item")
                        .WithMany("itemImages")
                        .HasForeignKey("itemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");
                });

            modelBuilder.Entity("Thrifty.Models.Order", b =>
                {
                    b.HasOne("Thrifty.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thrifty.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Thrifty.Models.OrderDetails", b =>
                {
                    b.HasOne("Thrifty.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thrifty.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("buyerId");

                    b.HasOne("Thrifty.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Thrifty.Models.ShopingCart", b =>
                {
                    b.HasOne("Thrifty.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Thrifty.Models.User", b =>
                {
                    b.HasOne("Thrifty.Models.City", "city")
                        .WithMany()
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Thrifty.Models.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");

                    b.Navigation("role");
                });

            modelBuilder.Entity("Thrifty.Models.Item", b =>
                {
                    b.Navigation("itemImages");
                });

            modelBuilder.Entity("Thrifty.Models.ItemCategory", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
