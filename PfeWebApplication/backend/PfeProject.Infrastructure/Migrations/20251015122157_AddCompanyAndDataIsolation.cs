using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyAndDataIsolation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Warehouses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Status",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Saps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Picklists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Lines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Inventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });
            // Backfill CompanyId for existing rows and ensure a default company exists
            migrationBuilder.Sql(@"
DO $$
DECLARE cid int;
BEGIN
  -- Ensure a default company exists
  SELECT ""Id"" INTO cid FROM ""Companies"" WHERE ""IsActive"" = TRUE LIMIT 1;
  IF cid IS NULL THEN
    INSERT INTO ""Companies"" (""Name"", ""Description"", ""Code"", ""CreationDate"", ""UpdateDate"", ""IsActive"")
    VALUES ('Default Company', 'Auto-created by migration', 'DEFAULT', NOW(), NOW(), TRUE)
    RETURNING ""Id"" INTO cid;
  END IF;

  -- Backfill all CompanyId FKs that were added with default 0
  UPDATE ""Articles""    SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Inventories"" SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Lines""       SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Picklists""   SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Saps""        SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Status""      SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Users""       SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
  UPDATE ""Warehouses""  SET ""CompanyId"" = cid WHERE ""CompanyId"" = 0;
END $$;
");
            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CompanyId",
                table: "Warehouses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_CompanyId",
                table: "Status",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Saps_CompanyId",
                table: "Saps",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Picklists_CompanyId",
                table: "Picklists",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_CompanyId",
                table: "Lines",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CompanyId",
                table: "Inventories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CompanyId",
                table: "Articles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Code",
                table: "Companies",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Companies_CompanyId",
                table: "Articles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Companies_CompanyId",
                table: "Inventories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Companies_CompanyId",
                table: "Lines",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picklists_Companies_CompanyId",
                table: "Picklists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Saps_Companies_CompanyId",
                table: "Saps",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Companies_CompanyId",
                table: "Status",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Companies_CompanyId",
                table: "Warehouses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Companies_CompanyId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Companies_CompanyId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Companies_CompanyId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Companies_CompanyId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Saps_Companies_CompanyId",
                table: "Saps");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Companies_CompanyId",
                table: "Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Companies_CompanyId",
                table: "Warehouses");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CompanyId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Status_CompanyId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Saps_CompanyId",
                table: "Saps");

            migrationBuilder.DropIndex(
                name: "IX_Picklists_CompanyId",
                table: "Picklists");

            migrationBuilder.DropIndex(
                name: "IX_Lines_CompanyId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_CompanyId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CompanyId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Saps");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Picklists");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Articles");
        }

    }
}
