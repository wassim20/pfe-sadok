using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitStocks");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "DetailInventories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventories_LocationId1",
                table: "DetailInventories",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Locations_LocationId1",
                table: "DetailInventories",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Locations_LocationId1",
                table: "DetailInventories");

            migrationBuilder.DropIndex(
                name: "IX_DetailInventories_LocationId1",
                table: "DetailInventories");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "DetailInventories");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Locations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "UnitStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    AssignedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitStocks_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitStocks_LocationId",
                table: "UnitStocks",
                column: "LocationId");
        }
    }
}
