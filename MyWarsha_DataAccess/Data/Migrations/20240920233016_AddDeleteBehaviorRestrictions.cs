using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteBehaviorRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarGeneration_CarGenerationId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarMaker_CarMakerId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarModel_CarModelId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Client_ClientId",
                table: "Service");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarGeneration_CarGenerationId",
                table: "CarInfo",
                column: "CarGenerationId",
                principalTable: "CarGeneration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarMaker_CarMakerId",
                table: "CarInfo",
                column: "CarMakerId",
                principalTable: "CarMaker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarModel_CarModelId",
                table: "CarInfo",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Client_ClientId",
                table: "Service",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarGeneration_CarGenerationId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarMaker_CarMakerId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CarInfo_CarModel_CarModelId",
                table: "CarInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Client_ClientId",
                table: "Service");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarGeneration_CarGenerationId",
                table: "CarInfo",
                column: "CarGenerationId",
                principalTable: "CarGeneration",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarMaker_CarMakerId",
                table: "CarInfo",
                column: "CarMakerId",
                principalTable: "CarMaker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarInfo_CarModel_CarModelId",
                table: "CarInfo",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBrand_ProductBrandId",
                table: "Product",
                column: "ProductBrandId",
                principalTable: "ProductBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Client_ClientId",
                table: "Service",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }
    }
}
