using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownBites.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuItemV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "MenuItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "MenuItems");
        }
    }
}
