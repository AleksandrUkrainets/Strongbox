using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Strongbox.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Approver User", new byte[] { 220, 107, 221, 150, 190, 60, 149, 239, 44, 12, 74, 149, 157, 2, 101, 86, 29, 175, 143, 160, 145, 213, 147, 46, 164, 25, 216, 60, 200, 28, 179, 59, 248, 58, 170, 88, 76, 28, 161, 64, 131, 133, 107, 159, 86, 213, 169, 146, 53, 209, 4, 203, 138, 1, 204, 121, 39, 200, 186, 137, 209, 160, 184, 2 }, new byte[] { 195, 234, 125, 97, 119, 143, 192, 105, 154, 48, 50, 29, 97, 106, 47, 190, 47, 51, 48, 107, 135, 243, 142, 117, 47, 86, 192, 200, 187, 157, 84, 197, 12, 174, 101, 182, 147, 250, 180, 53, 0, 3, 190, 9, 227, 63, 209, 177, 157, 166, 182, 50, 67, 120, 1, 210, 65, 243, 58, 49, 230, 29, 226, 70, 112, 157, 191, 186, 53, 48, 27, 70, 54, 86, 11, 171, 176, 39, 204, 204, 59, 43, 56, 43, 248, 210, 62, 59, 140, 2, 124, 110, 238, 143, 112, 200, 164, 26, 144, 98, 148, 152, 158, 175, 46, 20, 183, 226, 123, 135, 143, 64, 141, 49, 174, 233, 22, 33, 68, 76, 34, 55, 247, 213, 195, 66, 253, 23 }, 1, "ApproverUser" });
        }
    }
}
