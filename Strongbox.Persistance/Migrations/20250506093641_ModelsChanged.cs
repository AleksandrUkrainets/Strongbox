using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strongbox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ModelsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Decisions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Decisions",
                newName: "DecidedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Decisions",
                newName: "IsApproved");

            migrationBuilder.RenameColumn(
                name: "DecidedAt",
                table: "Decisions",
                newName: "CreatedAt");
        }
    }
}
