using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowersWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedRolesMIG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00ae760f-b9d8-4d39-ae0b-c3bf311d144e", null, "Admin", "ADMIN" },
                    { "e09214d9-094f-473f-9ffa-dda26a4d68be", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00ae760f-b9d8-4d39-ae0b-c3bf311d144e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e09214d9-094f-473f-9ffa-dda26a4d68be");
        }
    }
}
