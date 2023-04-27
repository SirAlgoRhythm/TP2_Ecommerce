﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StatsAPI;

#nullable disable

namespace StatsAPI.Migrations
{
    [DbContext(typeof(StatsDbContext))]
    [Migration("20230427150013_ChangerPKStats")]
    partial class ChangerPKStats
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StatsAPI.Models.StatistiqueClient", b =>
                {
                    b.Property<Guid>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalArticleBought")
                        .HasColumnType("int");

                    b.Property<double>("TotalCashSpent")
                        .HasColumnType("float");

                    b.HasKey("UtilisateurId");

                    b.ToTable("StatistiqueClients");
                });

            modelBuilder.Entity("StatsAPI.Models.StatistiqueVendeur", b =>
                {
                    b.Property<Guid>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Profit")
                        .HasColumnType("float");

                    b.Property<int>("TotalArticleSold")
                        .HasColumnType("int");

                    b.Property<double>("TotalCashReceved")
                        .HasColumnType("float");

                    b.HasKey("UtilisateurId");

                    b.ToTable("StatistiqueVendeurs");
                });
#pragma warning restore 612, 618
        }
    }
}
