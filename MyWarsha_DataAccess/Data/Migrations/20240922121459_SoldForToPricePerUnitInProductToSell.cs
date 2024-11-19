using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoldForToPricePerUnitInProductToSell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ServiceFee");

            migrationBuilder.RenameColumn(
                name: "SoldFor",
                table: "ProductToSell",
                newName: "PricePerUnit");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ServiceFee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "ServiceFee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceStatusId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFee_CategoryId",
                table: "ServiceFee",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service",
                column: "ServiceStatusId",
                principalTable: "ServiceStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceFee_Category_CategoryId",
                table: "ServiceFee",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceFee_Category_CategoryId",
                table: "ServiceFee");

            migrationBuilder.DropIndex(
                name: "IX_ServiceFee_CategoryId",
                table: "ServiceFee");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ServiceFee");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "ServiceFee");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "ProductToSell",
                newName: "SoldFor");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ServiceFee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceStatusId",
                table: "Service",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service",
                column: "ServiceStatusId",
                principalTable: "ServiceStatus",
                principalColumn: "Id");
        }
    }
}
