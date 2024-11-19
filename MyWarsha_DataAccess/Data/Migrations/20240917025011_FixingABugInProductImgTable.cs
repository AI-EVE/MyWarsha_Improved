using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingABugInProductImgTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImage");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductImage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductName",
                table: "ProductImage",
                column: "ProductName");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductName",
                table: "ProductImage",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductName",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductName",
                table: "ProductImage");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ProductImage",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Name");
        }
    }
}
