using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedServiceStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceStatusId",
                table: "Service",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ServiceStatus",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Pending" },
                    { 2, null, "InProgress" },
                    { 3, null, "Done" },
                    { 4, null, "Canceled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceStatusId",
                table: "Service",
                column: "ServiceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service",
                column: "ServiceStatusId",
                principalTable: "ServiceStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceStatus_ServiceStatusId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "ServiceStatus");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceStatusId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceStatusId",
                table: "Service");
        }
    }
}
