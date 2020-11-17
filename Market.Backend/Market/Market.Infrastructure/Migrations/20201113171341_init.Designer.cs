﻿// <auto-generated />
using System;
using Market.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Market.Infrastructure.Migrations
{
    [DbContext(typeof(MarketDb))]
    [Migration("20201113171341_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Market.ReadModels.Models.ProductReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ForBaby")
                        .HasColumnType("bit");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ProductReadModels");
                });

            modelBuilder.Entity("Market.ReadModels.Models.ProductReadModel", b =>
                {
                    b.OwnsMany("Market.Domain.Products.ValueObjects.Image", "Images", b1 =>
                        {
                            b1.Property<Guid>("ProductReadModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<string>("ImageUrl")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("IsMainImage")
                                .HasColumnType("bit");

                            b1.HasKey("ProductReadModelId", "Id");

                            b1.ToTable("Image");

                            b1.WithOwner()
                                .HasForeignKey("ProductReadModelId");
                        });

                    b.OwnsOne("Market.Domain.Products.ValueObjects.Weight", "Weight", b1 =>
                        {
                            b1.Property<Guid>("ProductReadModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<byte>("WeightType")
                                .HasColumnType("tinyint");

                            b1.HasKey("ProductReadModelId");

                            b1.ToTable("ProductReadModels");

                            b1.WithOwner()
                                .HasForeignKey("ProductReadModelId");
                        });

                    b.Navigation("Images");

                    b.Navigation("Weight");
                });
#pragma warning restore 612, 618
        }
    }
}
