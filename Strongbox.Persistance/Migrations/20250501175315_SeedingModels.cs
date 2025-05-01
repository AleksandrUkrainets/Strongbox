using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Strongbox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class SeedingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Content", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("4d1e8913-68fe-44cb-8462-34d6fe852489"), "Content of Document 2", new DateTime(2025, 5, 1, 17, 53, 14, 897, DateTimeKind.Utc).AddTicks(6739), "Document 2" },
                    { new Guid("691406d1-6aea-4755-adce-2f64f1e64f4a"), "Content of Document 3", new DateTime(2025, 5, 1, 17, 53, 14, 897, DateTimeKind.Utc).AddTicks(6742), "Document 3" },
                    { new Guid("c29a3eea-6aff-4680-a735-971d0c069bb1"), "Content of Document 1", new DateTime(2025, 5, 1, 17, 53, 14, 897, DateTimeKind.Utc).AddTicks(6576), "Document 1" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("851cbae3-84c3-4418-a6e9-bfc3974c6cae"), "Regular User", 0 },
                    { new Guid("b6c9254f-db7c-46b2-a7cc-5424771390d0"), "Approver User", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("4d1e8913-68fe-44cb-8462-34d6fe852489"));

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("691406d1-6aea-4755-adce-2f64f1e64f4a"));

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("c29a3eea-6aff-4680-a735-971d0c069bb1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("851cbae3-84c3-4418-a6e9-bfc3974c6cae"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b6c9254f-db7c-46b2-a7cc-5424771390d0"));
        }
    }
}
