﻿// <auto-generated />
using System;
using CashRegister.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CashRegister.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("CashRegister.Models.Domain.Barcode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Barcodes");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SessionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BarcodeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PictureId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BarcodeId");

                    b.HasIndex("PictureId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InitialBalance")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Started")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Order", b =>
                {
                    b.HasOne("CashRegister.Models.Domain.Session", "Session")
                        .WithMany("Orders")
                        .HasForeignKey("SessionId");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.OrderProduct", b =>
                {
                    b.HasOne("CashRegister.Models.Domain.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId");

                    b.HasOne("CashRegister.Models.Domain.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Product", b =>
                {
                    b.HasOne("CashRegister.Models.Domain.Barcode", "Barcode")
                        .WithMany()
                        .HasForeignKey("BarcodeId");

                    b.HasOne("CashRegister.Models.Domain.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Barcode");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("CashRegister.Models.Domain.Session", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
