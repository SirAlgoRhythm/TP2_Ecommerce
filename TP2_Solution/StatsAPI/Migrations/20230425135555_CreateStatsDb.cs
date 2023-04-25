using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateStatsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatistiqueClients",
                columns: table => new
                {
                    StatistiqueClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCashSpent = table.Column<double>(type: "float", nullable: false),
                    TotalArticleBought = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistiqueClients", x => x.StatistiqueClientId);
                });

            migrationBuilder.CreateTable(
                name: "StatistiqueVendeurs",
                columns: table => new
                {
                    StatistiqueVendeurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCashReceved = table.Column<double>(type: "float", nullable: false),
                    Profit = table.Column<double>(type: "float", nullable: false),
                    TotalArticleSold = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistiqueVendeurs", x => x.StatistiqueVendeurId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatistiqueClients");

            migrationBuilder.DropTable(
                name: "StatistiqueVendeurs");
        }
    }
}
