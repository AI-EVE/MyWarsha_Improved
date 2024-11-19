using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductBoughtProductsRestockingBillRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought",
                column: "ProductsRestockingBillId",
                principalTable: "ProductsRestockingBill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_ProductsRestockingBill_ProductsRestockingBillId",
                table: "ProductBought",
                column: "ProductsRestockingBillId",
                principalTable: "ProductsRestockingBill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
