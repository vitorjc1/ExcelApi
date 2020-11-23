﻿// <auto-generated />
using System;
using Capgemini.PMESP.SpreadsheetImport.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Capgemini.PMESP.SpreadsheetImport.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201121000253_ProductUnitPriceType")]
    partial class ProductUnitPriceType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Capgemini.PMESP.SpreadsheetImport.Models.Import", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Imports");
                });

            modelBuilder.Entity("Capgemini.PMESP.SpreadsheetImport.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ImportId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ImportId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Capgemini.PMESP.SpreadsheetImport.Models.Product", b =>
                {
                    b.HasOne("Capgemini.PMESP.SpreadsheetImport.Models.Import", "Import")
                        .WithMany("Products")
                        .HasForeignKey("ImportId");

                    b.Navigation("Import");
                });

            modelBuilder.Entity("Capgemini.PMESP.SpreadsheetImport.Models.Import", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
