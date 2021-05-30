using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Project.Data.Migrations
{
    public partial class AddTableExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSpending = table.Column<float>(type: "real", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryExpense",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ExpensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpense", x => new { x.CategoriesId, x.ExpensesId });
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseProduct",
                columns: table => new
                {
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseProduct", x => new { x.ExpensesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ExpenseProduct_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExpense_ExpensesId",
                table: "CategoryExpense",
                column: "ExpensesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseProduct_ProductsId",
                table: "ExpenseProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryExpense");

            migrationBuilder.DropTable(
                name: "ExpenseProduct");

            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
