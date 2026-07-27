using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownBites.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MenuItems");
        }
    }
}
