using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactureAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifModelFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProduitPanierId",
                table: "Factures",
                newName: "PanierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PanierId",
                table: "Factures",
                newName: "ProduitPanierId");
        }
    }
}
