using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShop.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRatingToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Products",
                type: "numeric(2,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(2,1)");
        }
    }
}
