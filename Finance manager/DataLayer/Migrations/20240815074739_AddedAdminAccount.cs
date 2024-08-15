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
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 6, "mr.admin.number1@gmail.com", "Admin", "Your best", "6c3f1783c349b50ba2a0f7bd1a6e75f626fdd1ce9a2e1b2215203a72ac20f30a" });

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
        }
    }
}
