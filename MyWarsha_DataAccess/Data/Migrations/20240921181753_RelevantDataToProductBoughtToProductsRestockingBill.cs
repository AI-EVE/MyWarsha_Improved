using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelevantDataToProductBoughtToProductsRestockingBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_RelevantDataToTheBought_RelevantDataToTheBoughtId",
                table: "ProductBought");

            migrationBuilder.DropTable(
                name: "RelevantDataToTheBought");

            migrationBuilder.DropColumn(
                name: "BoughtFor",
                table: "ProductBought");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "ProductBought",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "RelevantDataToTheBoughtId",
                table: "ProductBought",
                newName: "ProductsRestockingBillId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBought_RelevantDataToTheBoughtId",
                table: "ProductBought",
                newName: "IX_ProductBought_ProductsRestockingBillId");

            migrationBuilder.CreateTable(
                name: "ProductsRestockingBill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfOrder = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsRestockingBill", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought",
                column: "ProductsRestockingBillId",
                principalTable: "ProductsRestockingBill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought");

            migrationBuilder.DropTable(
                name: "ProductsRestockingBill");

            migrationBuilder.RenameColumn(
                name: "ProductsRestockingBillId",
                table: "ProductBought",
                newName: "RelevantDataToTheBoughtId");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "ProductBought",
                newName: "Total");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBought_ProductsRestockingBillId",
                table: "ProductBought",
                newName: "IX_ProductBought_RelevantDataToTheBoughtId");

            migrationBuilder.AddColumn<decimal>(
                name: "BoughtFor",
                table: "ProductBought",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RelevantDataToTheBought",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfOrder = table.Column<DateOnly>(type: "date", nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelevantDataToTheBought", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_RelevantDataToTheBought_RelevantDataToTheBoughtId",
                table: "ProductBought",
                column: "RelevantDataToTheBoughtId",
                principalTable: "RelevantDataToTheBought",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
