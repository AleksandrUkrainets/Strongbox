using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Strongbox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DocumentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRequests_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Decisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApproverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessRequestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decisions_AccessRequests_AccessRequestId",
                        column: x => x.AccessRequestId,
                        principalTable: "AccessRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decisions_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Content", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Content of Document 1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Document 1" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Content of Document 2", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Document 2" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Content of Document 3", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Document 3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Approver User", new byte[] { 220, 107, 221, 150, 190, 60, 149, 239, 44, 12, 74, 149, 157, 2, 101, 86, 29, 175, 143, 160, 145, 213, 147, 46, 164, 25, 216, 60, 200, 28, 179, 59, 248, 58, 170, 88, 76, 28, 161, 64, 131, 133, 107, 159, 86, 213, 169, 146, 53, 209, 4, 203, 138, 1, 204, 121, 39, 200, 186, 137, 209, 160, 184, 2 }, new byte[] { 195, 234, 125, 97, 119, 143, 192, 105, 154, 48, 50, 29, 97, 106, 47, 190, 47, 51, 48, 107, 135, 243, 142, 117, 47, 86, 192, 200, 187, 157, 84, 197, 12, 174, 101, 182, 147, 250, 180, 53, 0, 3, 190, 9, 227, 63, 209, 177, 157, 166, 182, 50, 67, 120, 1, 210, 65, 243, 58, 49, 230, 29, 226, 70, 112, 157, 191, 186, 53, 48, 27, 70, 54, 86, 11, 171, 176, 39, 204, 204, 59, 43, 56, 43, 248, 210, 62, 59, 140, 2, 124, 110, 238, 143, 112, 200, 164, 26, 144, 98, 148, 152, 158, 175, 46, 20, 183, 226, 123, 135, 143, 64, 141, 49, 174, 233, 22, 33, 68, 76, 34, 55, 247, 213, 195, 66, 253, 23 }, 1, "ApproverUser" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequests_DocumentId",
                table: "AccessRequests",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequests_UserId",
                table: "AccessRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Decisions_AccessRequestId",
                table: "Decisions",
                column: "AccessRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decisions_ApproverId",
                table: "Decisions",
                column: "ApproverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decisions");

            migrationBuilder.DropTable(
                name: "AccessRequests");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
