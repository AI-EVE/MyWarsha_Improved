using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditCarInfoProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_CarInfo_CarInfosId",
                table: "CarInfoProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsId",
                table: "CarInfoProduct");

            migrationBuilder.DropTable(
                name: "CarInfoProducts");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "CarInfoProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CarInfosId",
                table: "CarInfoProduct",
                newName: "CarInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarInfoProduct_ProductsId",
                table: "CarInfoProduct",
                newName: "IX_CarInfoProduct_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_CarInfo_CarInfoId",
                table: "CarInfoProduct",
                column: "CarInfoId",
                principalTable: "CarInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_Product_ProductId",
                table: "CarInfoProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_CarInfo_CarInfoId",
                table: "CarInfoProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfoProduct_Product_ProductId",
                table: "CarInfoProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CarInfoProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "CarInfoId",
                table: "CarInfoProduct",
                newName: "CarInfosId");

            migrationBuilder.RenameIndex(
                name: "IX_CarInfoProduct_ProductId",
                table: "CarInfoProduct",
                newName: "IX_CarInfoProduct_ProductsId");

            migrationBuilder.CreateTable(
                name: "CarInfoProducts",
                columns: table => new
                {
                    CarInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInfoProducts", x => new { x.CarInfoId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CarInfoProducts_CarInfo_CarInfoId",
                        column: x => x.CarInfoId,
                        principalTable: "CarInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarInfoProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarInfoProducts_ProductId",
                table: "CarInfoProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_CarInfo_CarInfosId",
                table: "CarInfoProduct",
                column: "CarInfosId",
                principalTable: "CarInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfoProduct_Product_ProductsId",
                table: "CarInfoProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
