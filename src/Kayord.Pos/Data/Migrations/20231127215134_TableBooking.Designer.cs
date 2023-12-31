﻿// <auto-generated />
using System;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231127215134_TableBooking")]
    partial class TableBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kayord.Pos.Entities.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Clock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SalesPeriodId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SalesPeriodId");

                    b.HasIndex("StaffId");

                    b.ToTable("Clock");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("Orders")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int?>("TableId")
                        .HasColumnType("integer");

                    b.HasKey("CustomerId");

                    b.HasIndex("TableId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OutletId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MenuItemId"));

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("MenuItemId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItem");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Outlet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Outlet");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.SalesPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OutletId");

                    b.ToTable("SalesPeriod");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OutletId");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OutletId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.HasKey("TableId");

                    b.HasIndex("SectionId");

                    b.ToTable("Table");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BookingName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SalesPeriodId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalesPeriodId");

                    b.HasIndex("StaffId");

                    b.HasIndex("TableId");

                    b.ToTable("TableBooking");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableCashUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CashUpDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OutletId")
                        .HasColumnType("integer");

                    b.Property<decimal>("SalesAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("TableBookingId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OutletId");

                    b.HasIndex("TableBookingId");

                    b.ToTable("TableCashUp");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableOrder", b =>
                {
                    b.Property<int>("TableOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TableOrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TableBookingId")
                        .HasColumnType("integer");

                    b.HasKey("TableOrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("TableOrder");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Clock", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.SalesPeriod", "SalesPeriod")
                        .WithMany()
                        .HasForeignKey("SalesPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kayord.Pos.Entities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesPeriod");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Customer", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Table", null)
                        .WithMany("Customers")
                        .HasForeignKey("TableId");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Menu", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Outlet", "Outlet")
                        .WithMany()
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outlet");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.MenuItem", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.OrderItem", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Outlet", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Business", "Business")
                        .WithMany("Outlets")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.SalesPeriod", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Outlet", "Outlet")
                        .WithMany()
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outlet");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Section", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Outlet", "Outlet")
                        .WithMany("Sections")
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outlet");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Staff", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Outlet", "Outlet")
                        .WithMany()
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outlet");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Table", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Section", "Section")
                        .WithMany("Tables")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableBooking", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.SalesPeriod", "SalesPeriod")
                        .WithMany()
                        .HasForeignKey("SalesPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kayord.Pos.Entities.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kayord.Pos.Entities.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesPeriod");

                    b.Navigation("Staff");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableCashUp", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Outlet", "Outlet")
                        .WithMany()
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kayord.Pos.Entities.TableBooking", "TableBooking")
                        .WithMany()
                        .HasForeignKey("TableBookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outlet");

                    b.Navigation("TableBooking");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.TableOrder", b =>
                {
                    b.HasOne("Kayord.Pos.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Business", b =>
                {
                    b.Navigation("Outlets");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Outlet", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Section", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Kayord.Pos.Entities.Table", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
