using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialUserRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Description",
                table: "Roles",
                newName: "IX_Roles_Name");

            migrationBuilder.AddColumn<int>(
                name: "AssignedById",
                table: "UserRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRoles",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "UserRoles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_AssignedById",
                table: "UserRoles",
                column: "AssignedById");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles",
                column: "AssignedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_AssignedById",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "AssignedById",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                newName: "IX_Roles_Description");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
