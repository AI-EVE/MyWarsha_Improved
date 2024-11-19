using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWarsha_DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitsSoldColumnFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsSold",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsSold",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
