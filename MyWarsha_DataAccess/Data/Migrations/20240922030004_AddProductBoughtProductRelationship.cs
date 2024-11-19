using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductBoughtProductRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
