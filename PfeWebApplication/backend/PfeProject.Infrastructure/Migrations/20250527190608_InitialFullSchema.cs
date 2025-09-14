using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialFullSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventory_Inventory_InventoryId",
                table: "DetailInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventory_Locations_LocationId",
                table: "DetailInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventory_Sap_SapId",
                table: "DetailInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventory_Users_UserId",
                table: "DetailInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklist_Article_ArticleId",
                table: "DetailPicklist");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklist_Picklists_PicklistId",
                table: "DetailPicklist");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklist_Status_StatusId",
                table: "DetailPicklist");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MovementTraces_DetailPicklist_DetailPicklistId",
                table: "MovementTraces");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Line_LineId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Status_StatusId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Warehouse_WarehouseId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_DetailPicklist_DetailPicklistId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_Status_StatusId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Article_ArticleId",
                table: "ReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Status_StatusId",
                table: "ReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStocks_Locations_LocationId",
                table: "UnitStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_IsActive",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sap",
                table: "Sap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Line",
                table: "Line");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailPicklist",
                table: "DetailPicklist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailInventory",
                table: "DetailInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Sap",
                newName: "Saps");

            migrationBuilder.RenameTable(
                name: "Line",
                newName: "Lines");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameTable(
                name: "DetailPicklist",
                newName: "DetailPicklists");

            migrationBuilder.RenameTable(
                name: "DetailInventory",
                newName: "DetailInventories");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklist_StatusId",
                table: "DetailPicklists",
                newName: "IX_DetailPicklists_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklist_PicklistId",
                table: "DetailPicklists",
                newName: "IX_DetailPicklists_PicklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklist_ArticleId",
                table: "DetailPicklists",
                newName: "IX_DetailPicklists_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventory_UserId",
                table: "DetailInventories",
                newName: "IX_DetailInventories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventory_SapId",
                table: "DetailInventories",
                newName: "IX_DetailInventories_SapId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventory_LocationId",
                table: "DetailInventories",
                newName: "IX_DetailInventories_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventory_InventoryId",
                table: "DetailInventories",
                newName: "IX_DetailInventories_InventoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "UnitStocks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Locations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Warehouses",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Statuses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UsCode",
                table: "Saps",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                table: "Saps",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Inventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Inventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateInventaire",
                table: "Inventories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "UsCode",
                table: "DetailInventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleCode",
                table: "DetailInventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Saps",
                table: "Saps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lines",
                table: "Lines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailPicklists",
                table: "DetailPicklists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailInventories",
                table: "DetailInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Inventories_InventoryId",
                table: "DetailInventories",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Locations_LocationId",
                table: "DetailInventories",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Saps_SapId",
                table: "DetailInventories",
                column: "SapId",
                principalTable: "Saps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventories_Users_UserId",
                table: "DetailInventories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_DetailPicklists_Statuses_StatusId",
                table: "DetailPicklists",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouses_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MovementTraces_DetailPicklists_DetailPicklistId",
                table: "MovementTraces",
                column: "DetailPicklistId",
                principalTable: "DetailPicklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picklists_Lines_LineId",
                table: "Picklists",
                column: "LineId",
                principalTable: "Lines",
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
                name: "FK_Picklists_Warehouses_WarehouseId",
                table: "Picklists",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUs_DetailPicklists_DetailPicklistId",
                table: "PicklistUs",
                column: "DetailPicklistId",
                principalTable: "DetailPicklists",
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
                name: "FK_ReturnLines_Articles_ArticleId",
                table: "ReturnLines",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLines_Statuses_StatusId",
                table: "ReturnLines",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStocks_Locations_LocationId",
                table: "UnitStocks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles",
                column: "AssignedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Inventories_InventoryId",
                table: "DetailInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Locations_LocationId",
                table: "DetailInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Saps_SapId",
                table: "DetailInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailInventories_Users_UserId",
                table: "DetailInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Articles_ArticleId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Picklists_PicklistId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPicklists_Statuses_StatusId",
                table: "DetailPicklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouses_WarehouseId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MovementTraces_DetailPicklists_DetailPicklistId",
                table: "MovementTraces");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Lines_LineId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Statuses_StatusId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_Picklists_Warehouses_WarehouseId",
                table: "Picklists");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_DetailPicklists_DetailPicklistId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUs_Statuses_StatusId",
                table: "PicklistUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Articles_ArticleId",
                table: "ReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLines_Statuses_StatusId",
                table: "ReturnLines");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitStocks_Locations_LocationId",
                table: "UnitStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Saps",
                table: "Saps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lines",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailPicklists",
                table: "DetailPicklists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailInventories",
                table: "DetailInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Saps",
                newName: "Sap");

            migrationBuilder.RenameTable(
                name: "Lines",
                newName: "Line");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameTable(
                name: "DetailPicklists",
                newName: "DetailPicklist");

            migrationBuilder.RenameTable(
                name: "DetailInventories",
                newName: "DetailInventory");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklists_StatusId",
                table: "DetailPicklist",
                newName: "IX_DetailPicklist_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklists_PicklistId",
                table: "DetailPicklist",
                newName: "IX_DetailPicklist_PicklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPicklists_ArticleId",
                table: "DetailPicklist",
                newName: "IX_DetailPicklist_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventories_UserId",
                table: "DetailInventory",
                newName: "IX_DetailInventory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventories_SapId",
                table: "DetailInventory",
                newName: "IX_DetailInventory_SapId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventories_LocationId",
                table: "DetailInventory",
                newName: "IX_DetailInventory_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailInventories_InventoryId",
                table: "DetailInventory",
                newName: "IX_DetailInventory_InventoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "UnitStocks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Locations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouse",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Warehouse",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Status",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UsCode",
                table: "Sap",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                table: "Sap",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Inventory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Inventory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateInventaire",
                table: "Inventory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "UsCode",
                table: "DetailInventory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ArticleCode",
                table: "DetailInventory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sap",
                table: "Sap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Line",
                table: "Line",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailPicklist",
                table: "DetailPicklist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailInventory",
                table: "DetailInventory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_IsActive",
                table: "UserRoles",
                column: "IsActive");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventory_Inventory_InventoryId",
                table: "DetailInventory",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventory_Locations_LocationId",
                table: "DetailInventory",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventory_Sap_SapId",
                table: "DetailInventory",
                column: "SapId",
                principalTable: "Sap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInventory_Users_UserId",
                table: "DetailInventory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklist_Article_ArticleId",
                table: "DetailPicklist",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklist_Picklists_PicklistId",
                table: "DetailPicklist",
                column: "PicklistId",
                principalTable: "Picklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPicklist_Status_StatusId",
                table: "DetailPicklist",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MovementTraces_DetailPicklist_DetailPicklistId",
                table: "MovementTraces",
                column: "DetailPicklistId",
                principalTable: "DetailPicklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picklists_Line_LineId",
                table: "Picklists",
                column: "LineId",
                principalTable: "Line",
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
                name: "FK_Picklists_Warehouse_WarehouseId",
                table: "Picklists",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUs_DetailPicklist_DetailPicklistId",
                table: "PicklistUs",
                column: "DetailPicklistId",
                principalTable: "DetailPicklist",
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
                name: "FK_ReturnLines_Article_ArticleId",
                table: "ReturnLines",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLines_Status_StatusId",
                table: "ReturnLines",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitStocks_Locations_LocationId",
                table: "UnitStocks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_AssignedById",
                table: "UserRoles",
                column: "AssignedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
