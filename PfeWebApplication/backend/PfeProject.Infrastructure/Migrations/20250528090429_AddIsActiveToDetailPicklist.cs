using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToDetailPicklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Articles_ArticleId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Picklists_PicklistId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists");

            migrationBuilder.AlterColumn<string>(
                name: "Quantite",
                table: "DetailPicklists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Emplacement",
                table: "DetailPicklists",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DetailPicklists",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Articles_ArticleId",
                table: "DetailPicklists",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Picklists_PicklistId",
                table: "DetailPicklists",
                column: "PicklistId",
                principalTable: "Picklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Articles_ArticleId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Picklists_PicklistId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DetailPicklists");

            migrationBuilder.AlterColumn<string>(
                name: "Quantite",
                table: "DetailPicklists",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Emplacement",
                table: "DetailPicklists",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Articles_ArticleId",
                table: "DetailPicklists",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Picklists_PicklistId",
                table: "DetailPicklists",
                column: "PicklistId",
                principalTable: "Picklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Status_StatusId",
                table: "DetailPicklists",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
