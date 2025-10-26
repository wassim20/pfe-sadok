using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCopanyIdToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. First, ensure CompanyId = 1 exists in Companies table
            migrationBuilder.Sql(@"
                INSERT INTO ""Companies"" (""Id"", ""Name"", ""Description"", ""Code"", ""CreationDate"", ""UpdateDate"", ""IsActive"")
                VALUES (1, 'Default Company', 'Default company for existing data', 'DEFAULT', NOW(), NOW(), true)
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // 2. Add nullable CompanyId columns first
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Roles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ReturnLines",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "PicklistUs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MovementTraces",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Locations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DetailPicklists",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "DetailInventories",
                type: "integer",
                nullable: true);

            // 3. Update existing records with CompanyId = 1
            migrationBuilder.Sql("UPDATE \"UserRoles\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"Roles\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"ReturnLines\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"PicklistUs\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"MovementTraces\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"Locations\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"DetailPicklists\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");
            migrationBuilder.Sql("UPDATE \"DetailInventories\" SET \"CompanyId\" = 1 WHERE \"CompanyId\" IS NULL");

            // 4. Make columns non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "UserRoles",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Roles",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "ReturnLines",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "PicklistUs",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "MovementTraces",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Locations",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DetailPicklists",
                type: "integer",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DetailInventories",
                type: "integer",
                nullable: false);

            // 5. Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_CompanyId",
                table: "UserRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CompanyId",
                table: "Roles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_CompanyId",
                table: "ReturnLines",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PicklistUs_CompanyId",
                table: "PicklistUs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementTraces_CompanyId",
                table: "MovementTraces",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CompanyId",
                table: "Locations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPicklists_CompanyId",
                table: "DetailPicklists",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventories_CompanyId",
                table: "DetailInventories",
                column: "CompanyId");

            // 6. Add foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Companies_CompanyId",
                table: "DetailInventories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklists_Companies_CompanyId",
                table: "DetailPicklists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Companies_CompanyId",
                table: "Locations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovementTraces_Companies_CompanyId",
                table: "MovementTraces",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUs_Companies_CompanyId",
                table: "PicklistUs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLines_Companies_CompanyId",
                table: "ReturnLines",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Companies_CompanyId",
                table: "Roles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Companies_CompanyId",
                table: "UserRoles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Companies_CompanyId",
                table: "DetailInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Companies_CompanyId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Companies_CompanyId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MovementTraces_Companies_CompanyId",
                table: "MovementTraces");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_Companies_CompanyId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Companies_CompanyId",
                table: "ReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Companies_CompanyId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Companies_CompanyId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_CompanyId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_CompanyId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_ReturnLines_CompanyId",
                table: "ReturnLines");

            migrationBuilder.DropIndex(
                name: "IX_PicklistUs_CompanyId",
                table: "PicklistUs");

            migrationBuilder.DropIndex(
                name: "IX_MovementTraces_CompanyId",
                table: "MovementTraces");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CompanyId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_DetailPicklists_CompanyId",
                table: "DetailPicklists");

            migrationBuilder.DropIndex(
                name: "IX_DetailInventories_CompanyId",
                table: "DetailInventories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ReturnLines");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PicklistUs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MovementTraces");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DetailPicklists");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "DetailInventories");
        }
    }
}
