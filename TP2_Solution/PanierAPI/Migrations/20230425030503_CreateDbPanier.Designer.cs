﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PanierAPI;

#nullable disable

namespace PanierAPI.Migrations
{
    [DbContext(typeof(PanierDbContext))]
    [Migration("20230425030503_CreateDbPanier")]
    partial class CreateDbPanier
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PanierAPI.Models.Panier", b =>
                {
                    b.Property<Guid>("PanierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PanierId");

                    b.ToTable("Paniers");
                });
#pragma warning restore 612, 618
        }
    }
}
