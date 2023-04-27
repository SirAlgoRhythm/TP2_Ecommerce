using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangerPKStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatistiqueVendeurs",
                table: "StatistiqueVendeurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatistiqueClients",
                table: "StatistiqueClients");

            migrationBuilder.DropColumn(
                name: "StatistiqueVendeurId",
                table: "StatistiqueVendeurs");

            migrationBuilder.DropColumn(
                name: "StatistiqueClientId",
                table: "StatistiqueClients");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StatistiqueVendeurs",
                newName: "UtilisateurId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StatistiqueClients",
                newName: "UtilisateurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatistiqueVendeurs",
                table: "StatistiqueVendeurs",
                column: "UtilisateurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatistiqueClients",
                table: "StatistiqueClients",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatistiqueVendeurs",
                table: "StatistiqueVendeurs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatistiqueClients",
                table: "StatistiqueClients");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "StatistiqueVendeurs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "StatistiqueClients",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "StatistiqueVendeurId",
                table: "StatistiqueVendeurs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatistiqueClientId",
                table: "StatistiqueClients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatistiqueVendeurs",
                table: "StatistiqueVendeurs",
                column: "StatistiqueVendeurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatistiqueClients",
                table: "StatistiqueClients",
                column: "StatistiqueClientId");
        }
    }
}
