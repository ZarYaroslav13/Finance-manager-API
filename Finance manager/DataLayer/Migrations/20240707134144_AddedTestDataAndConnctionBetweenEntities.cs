using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestDataAndConnctionBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionsType_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionsType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "password123" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "password456" },
                    { 3, "michael.johnson@example.com", "Michael", "Johnson", "password789" },
                    { 4, "emily.davis@example.com", "Emily", "Davis", "password101" },
                    { 5, "chris.brown@example.com", "Chris", "Brown", "password102" }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "AccountId", "Balance" },
                values: new object[,]
                {
                    { 1, 1, 1000 },
                    { 2, 1, 1500 },
                    { 3, 2, 2000 },
                    { 4, 5, 2500 },
                    { 5, 3, 3000 },
                    { 6, 4, 3500 },
                    { 7, 5, 4000 }
                });

            migrationBuilder.InsertData(
                table: "TransactionsType",
                columns: new[] { "Id", "Description", "EntryType", "Name", "WalletId" },
                values: new object[,]
                {
                    { 1, "Monthly salary", 0, "Salary", 1 },
                    { 2, "Weekly groceries", 1, "Groceries", 1 },
                    { 3, "Monthly rent payment", 1, "Rent", 2 },
                    { 4, "Monthly electricity bill", 1, "Electricity Bill", 2 },
                    { 5, "Freelance project payment", 0, "Freelance", 3 },
                    { 6, "Dinner at restaurant", 1, "Dining Out", 3 },
                    { 7, "Monthly gym membership", 1, "Gym Membership", 4 },
                    { 8, "Birthday gift", 1, "Gift", 4 },
                    { 9, "Consulting services", 0, "Consulting", 5 },
                    { 10, "Travel expenses", 1, "Travel", 5 },
                    { 11, "Stock market investment", 1, "Investment", 6 },
                    { 12, "Stock dividends", 0, "Dividends", 6 },
                    { 13, "Monthly savings", 0, "Savings", 7 },
                    { 14, "Car repair and maintenance", 1, "Car Maintenance", 7 },
                    { 15, "Monthly internet bill", 1, "Internet Bill", 1 },
                    { 16, "Annual bonus", 0, "Bonus", 1 },
                    { 17, "Health insurance payment", 1, "Insurance", 2 },
                    { 18, "Income from book sales", 0, "Book Sales", 2 },
                    { 19, "Purchase of clothing", 1, "Clothing", 3 },
                    { 20, "Bank account interest", 0, "Interest", 3 },
                    { 21, "Payment for medical services", 1, "Medical Bills", 4 },
                    { 22, "Payment for education", 1, "Tuition", 4 },
                    { 23, "Income from software sales", 0, "Software Sales", 5 },
                    { 24, "Purchase of household supplies", 1, "Household Supplies", 5 },
                    { 25, "Income from freelance writing", 0, "Freelance Writing", 6 },
                    { 26, "Donation to charity", 1, "Charity", 6 },
                    { 27, "Purchase of furniture", 1, "Furniture", 7 },
                    { 28, "Income from music sales", 0, "Music Sales", 7 },
                    { 29, "Income from tax refund", 0, "Tax Refund", 1 },
                    { 30, "Monthly subscriptions", 1, "Subscriptions", 2 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Date", "TypeId", "WalletId" },
                values: new object[,]
                {
                    { 1, 500, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, -100, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 3, -750, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 4, -120, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 5, 1500, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 6, -60, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 7, -40, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 8, -100, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 9, 2000, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 10, -300, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 11, -150, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 12, 200, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 13, 300, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 14, -70, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 15, -50, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 16, 1000, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 17, -200, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 18, 250, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 19, -80, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 20, 50, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 21, -300, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 22, -500, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 23, 1500, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 24, -40, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 25, 600, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 26, -100, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 27, -400, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 28, 300, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 29, 500, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 30, -20, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 31, 500, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 32, -100, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 33, -750, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 34, -120, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 35, 1500, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 36, -60, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 37, -40, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 38, -100, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 39, 2000, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 40, -300, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 41, -150, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 42, 200, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 43, 300, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 44, -70, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 45, -50, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 46, 1000, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 47, -200, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 48, 250, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 49, -80, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 50, 50, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 51, -300, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 52, -500, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 53, 1500, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 54, -40, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 55, 600, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 56, -100, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 57, -400, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 58, 300, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 59, 500, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 60, -20, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 61, 500, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 62, -100, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 63, -750, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 64, -120, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 65, 1500, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 66, -60, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 67, -40, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 68, -100, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 69, 2000, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 70, -300, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 71, -150, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 72, 200, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 73, 300, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 74, -70, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 75, -50, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 76, 1000, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 77, -200, new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 78, 250, new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 79, -80, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 80, 50, new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 81, -300, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 82, -500, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 83, 1500, new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 84, -40, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 85, 600, new DateTime(2024, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 86, -100, new DateTime(2024, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 87, -400, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 88, 300, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 89, 500, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 90, -20, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 91, 500, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 92, -100, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 93, -750, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 94, -120, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 95, 1500, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 96, -60, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 97, -40, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 98, -100, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 99, 2000, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 100, -300, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsType_WalletId",
                table: "TransactionsType",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_AccountId",
                table: "Wallets",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionsType");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
