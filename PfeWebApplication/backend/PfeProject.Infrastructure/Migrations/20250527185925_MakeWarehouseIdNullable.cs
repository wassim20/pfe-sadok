﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeWarehouseIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Locations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
