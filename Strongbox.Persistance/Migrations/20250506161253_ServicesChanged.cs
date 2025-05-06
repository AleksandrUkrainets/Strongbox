using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strongbox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ServicesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DecidedAt",
                table: "Decisions",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Decisions",
                newName: "DecidedAt");
        }
    }
}
