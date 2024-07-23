using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenamedFinanceOperationAndFinanceOperationTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionsType");

            migrationBuilder.CreateTable(
                name: "FinanceOperationTypes",
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
                    table.PrimaryKey("PK_FinanceOperationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceOperationTypes_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinanceOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceOperations_FinanceOperationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FinanceOperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FinanceOperationTypes",
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
                table: "FinanceOperations",
                columns: new[] { "Id", "Amount", "Date", "TypeId" },
                values: new object[,]
                {
                    { 1, 500L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 100L, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 750L, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 120L, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 1500L, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, 60L, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, 40L, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 8, 100L, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 9, 2000L, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 10, 300L, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 11, 150L, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 12, 200L, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 13, 300L, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 14, 70L, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 15, 50L, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 16, 1000L, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 17, 200L, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 18, 250L, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 19, 80L, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 20, 50L, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 21, 300L, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 22, 500L, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 23, 1500L, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 24, 40L, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 25, 600L, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 26, 100L, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 27, 400L, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 28, 300L, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 29, 500L, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 30, 20L, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 31, 500L, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 32, 100L, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 33, 750L, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 34, 120L, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 35, 1500L, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 36, 60L, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 37, 40L, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 38, 100L, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 39, 2000L, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 40, 300L, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 41, 150L, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 42, 200L, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 43, 300L, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 44, 70L, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 45, 50L, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 46, 1000L, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 47, 200L, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 48, 250L, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 49, 80L, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 50, 50L, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 51, 300L, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 52, 500L, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 53, 1500L, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 54, 40L, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 55, 600L, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 56, 100L, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 57, 400L, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 58, 300L, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 59, 500L, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 60, 20L, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 61, 500L, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 62, 100L, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 63, 750L, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 64, 120L, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 65, 1500L, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 66, 60L, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 67, 40L, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 68, 100L, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 69, 2000L, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 70, 300L, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 71, 150L, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 72, 200L, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 73, 300L, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 74, 70L, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 75, 50L, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 76, 1000L, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 77, 200L, new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 78, 250L, new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 79, 80L, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 80, 50L, new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 81, 300L, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 82, 500L, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 83, 1500L, new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 23 },
                    { 84, 40L, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 24 },
                    { 85, 600L, new DateTime(2024, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 86, 100L, new DateTime(2024, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 87, 400L, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 88, 300L, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 28 },
                    { 89, 500L, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29 },
                    { 90, 20L, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 91, 500L, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 92, 100L, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 93, 750L, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 94, 120L, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 95, 1500L, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 96, 60L, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 97, 40L, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 98, 100L, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 99, 2000L, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 100, 300L, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceOperations_TypeId",
                table: "FinanceOperations",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceOperationTypes_WalletId",
                table: "FinanceOperationTypes",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceOperations");

            migrationBuilder.DropTable(
                name: "FinanceOperationTypes");

            migrationBuilder.CreateTable(
                name: "TransactionsType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    { 1, 500L, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, 100L, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 3, 750L, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 4, 120L, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 5, 1500L, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 6, 60L, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 7, 40L, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 8, 100L, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 9, 2000L, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 10, 300L, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 11, 150L, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 12, 200L, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 13, 300L, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 14, 70L, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 15, 50L, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 16, 1000L, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 17, 200L, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 18, 250L, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 19, 80L, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 20, 50L, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 21, 300L, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 22, 500L, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 23, 1500L, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 24, 40L, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 25, 600L, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 26, 100L, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 27, 400L, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 28, 300L, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 29, 500L, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 30, 20L, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 31, 500L, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 32, 100L, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 33, 750L, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 34, 120L, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 35, 1500L, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 36, 60L, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 37, 40L, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 38, 100L, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 39, 2000L, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 40, 300L, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 41, 150L, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 42, 200L, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 43, 300L, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 44, 70L, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 45, 50L, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 46, 1000L, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 47, 200L, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 48, 250L, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 49, 80L, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 50, 50L, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 51, 300L, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 52, 500L, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 53, 1500L, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 54, 40L, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 55, 600L, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 56, 100L, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 57, 400L, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 58, 300L, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 59, 500L, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 60, 20L, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 61, 500L, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 62, 100L, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 63, 750L, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 64, 120L, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 65, 1500L, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 66, 60L, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 67, 40L, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 68, 100L, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 69, 2000L, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 70, 300L, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null },
                    { 71, 150L, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null },
                    { 72, 200L, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null },
                    { 73, 300L, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null },
                    { 74, 70L, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null },
                    { 75, 50L, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null },
                    { 76, 1000L, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null },
                    { 77, 200L, new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null },
                    { 78, 250L, new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, null },
                    { 79, 80L, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null },
                    { 80, 50L, new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null },
                    { 81, 300L, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, null },
                    { 82, 500L, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null },
                    { 83, 1500L, new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null },
                    { 84, 40L, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, null },
                    { 85, 600L, new DateTime(2024, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null },
                    { 86, 100L, new DateTime(2024, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null },
                    { 87, 400L, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null },
                    { 88, 300L, new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, null },
                    { 89, 500L, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null },
                    { 90, 20L, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null },
                    { 91, 500L, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 92, 100L, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 93, 750L, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 94, 120L, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null },
                    { 95, 1500L, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 96, 60L, new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null },
                    { 97, 40L, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 98, 100L, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null },
                    { 99, 2000L, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 100, 300L, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null }
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
        }
    }
}
