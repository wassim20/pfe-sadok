using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToMovementTrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MovementTraces",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MovementTraces");
        }
    }
}
