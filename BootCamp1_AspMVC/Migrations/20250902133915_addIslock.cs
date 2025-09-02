using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCamp1_AspMVC.Migrations
{
    /// <inheritdoc />
    public partial class addIslock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Islock",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Islock",
                table: "Employees");
        }
    }
}
