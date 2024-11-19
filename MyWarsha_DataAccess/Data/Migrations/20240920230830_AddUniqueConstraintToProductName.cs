using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsName",
                table: "CarInfoProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_Product_ProductName",
                table: "ProductBought");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductName",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToSell_Product_ProductName",
                table: "ProductToSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductToSell_ProductName",
                table: "ProductToSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductName",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductBought_ProductName",
                table: "ProductBought");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarInfoProduct",
                table: "CarInfoProduct");

            migrationBuilder.DropIndex(
                name: "IX_CarInfoProduct_ProductsName",
                table: "CarInfoProduct");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductToSell");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductBought");

            migrationBuilder.DropColumn(
                name: "ProductsName",
                table: "CarInfoProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductToSell",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductBought",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "CarInfoProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarInfoProduct",
                table: "CarInfoProduct",
                columns: new[] { "CarInfosId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductToSell_ProductId",
                table: "ProductToSell",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBought_ProductId",
                table: "ProductBought",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarInfoProduct_ProductsId",
                table: "CarInfoProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsId",
                table: "CarInfoProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToSell_Product_ProductId",
                table: "ProductToSell",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsId",
                table: "CarInfoProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBought_Product_ProductId",
                table: "ProductBought");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToSell_Product_ProductId",
                table: "ProductToSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductToSell_ProductId",
                table: "ProductToSell");

            migrationBuilder.DropIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_ProductBought_ProductId",
                table: "ProductBought");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarInfoProduct",
                table: "CarInfoProduct");

            migrationBuilder.DropIndex(
                name: "IX_CarInfoProduct_ProductsId",
                table: "CarInfoProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductToSell");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductBought");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "CarInfoProduct");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductToSell",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductImage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductBought",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductsName",
                table: "CarInfoProduct",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarInfoProduct",
                table: "CarInfoProduct",
                columns: new[] { "CarInfosId", "ProductsName" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductToSell_ProductName",
                table: "ProductToSell",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductName",
                table: "ProductImage",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBought_ProductName",
                table: "ProductBought",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_CarInfoProduct_ProductsName",
                table: "CarInfoProduct",
                column: "ProductsName");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsName",
                table: "CarInfoProduct",
                column: "ProductsName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBought_Product_ProductName",
                table: "ProductBought",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductName",
                table: "ProductImage",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToSell_Product_ProductName",
                table: "ProductToSell",
                column: "ProductName",
                principalTable: "Product",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
