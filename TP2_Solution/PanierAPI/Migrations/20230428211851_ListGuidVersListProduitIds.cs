using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PanierAPI.Migrations
{
    /// <inheritdoc />
    public partial class ListGuidVersListProduitIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProduitIds",
                columns: table => new
                {
                    ProduitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PanierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitIds", x => x.ProduitId);
                    table.ForeignKey(
                        name: "FK_ProduitIds_Paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Paniers",
                        principalColumn: "PanierId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduitIds_PanierId",
                table: "ProduitIds",
                column: "PanierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduitIds");
        }
    }
}
