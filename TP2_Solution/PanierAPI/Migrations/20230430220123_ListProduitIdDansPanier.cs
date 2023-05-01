using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PanierAPI.Migrations
{
    /// <inheritdoc />
    public partial class ListProduitIdDansPanier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProduitIds_Paniers_PanierId",
                table: "ProduitIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduitIds",
                table: "ProduitIds");

            migrationBuilder.DropIndex(
                name: "IX_ProduitIds_PanierId",
                table: "ProduitIds");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "ProduitIds");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProduitIds",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduitIds",
                table: "ProduitIds",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PanierProduit",
                columns: table => new
                {
                    PanierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProduitIdListeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanierProduit", x => new { x.PanierId, x.ProduitIdListeId });
                    table.ForeignKey(
                        name: "FK_PanierProduit_Paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Paniers",
                        principalColumn: "PanierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PanierProduit_ProduitIds_ProduitIdListeId",
                        column: x => x.ProduitIdListeId,
                        principalTable: "ProduitIds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PanierProduit_ProduitIdListeId",
                table: "PanierProduit",
                column: "ProduitIdListeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PanierProduit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProduitIds",
                table: "ProduitIds");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProduitIds");

            migrationBuilder.AddColumn<Guid>(
                name: "PanierId",
                table: "ProduitIds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProduitIds",
                table: "ProduitIds",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitIds_PanierId",
                table: "ProduitIds",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProduitIds_Paniers_PanierId",
                table: "ProduitIds",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "PanierId");
        }
    }
}
