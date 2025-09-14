using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameStatusesToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Statuses_StatusId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Statuses_StatusId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_Statuses_StatusId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Statuses_StatusId",
                table: "ReturnLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picklists_Status_StatusId",
                table: "Picklists",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUs_Status_StatusId",
                table: "PicklistUs",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLines_Status_StatusId",
                table: "ReturnLines",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Status_StatusId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_Status_StatusId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Status_StatusId",
                table: "ReturnLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Statuses_StatusId",
                table: "DetailPicklists",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picklists_Statuses_StatusId",
                table: "Picklists",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUs_Statuses_StatusId",
                table: "PicklistUs",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLines_Statuses_StatusId",
                table: "ReturnLines",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
