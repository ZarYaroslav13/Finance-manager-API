using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "f13ae899443405d0356fa8327559ce2415d6eca5d6b475bd79bb4a19a5c03f03");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "9fad83113b7f41052f614e3ad6d99e025a404635268e372fd73ab195aa57ef4b");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "0d03517e6dfa1b5d4bfb0f627e25b3f6699ff31a0a5c31a3fb098fd5495b4779");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "363b9c3766022ad67ff6a75491fdd501cc2e8e39b994c4f36363f0d26e82a12c");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "a9517c1b6c910ebc2d7c6c59ff08e14e14e511301ac7683b3538405b047ed6d3");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 6, "mr.admin.number1@gmail.com", "Admin", "Your best", "daee8940d0fbf19005e13de32ce11a5f56dbdf6516c6c527e66a67a815cecce6" });

            migrationBuilder.InsertData(
                table: "FinanceOperations",
                columns: new[] { "Id", "Amount", "Date", "TypeId" },
                values: new object[,]
                {
                    { 101, 100L, new DateTime(2024, 4, 11, 3, 11, 24, 0, DateTimeKind.Unspecified), 1 },
                    { 102, 2000L, new DateTime(2024, 4, 11, 11, 2, 53, 0, DateTimeKind.Unspecified), 1 },
                    { 103, 300L, new DateTime(2024, 4, 11, 7, 27, 37, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FinanceOperations",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "FinanceOperations",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "FinanceOperations",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "c6ba91b90d922e159893f46c387e5dc1b3dc5c101a5a4522f03b987177a24a91");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "5efc2b017da4f7736d192a74dde5891369e0685d4d38f2a455b6fcdab282df9c");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "5c773b22ea79d367b38810e7e9ad108646ed62e231868cefb0b1280ea88ac4f0");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "3233c5e43b1a0d2a03c8a6fc981fe3bad46b9fee5ca59d53f6a531325cd3a433");
        }
    }
}
