using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PfeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPicklistModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PicklistUS_Picklists_PicklistId",
                table: "PicklistUS");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PicklistUS",
                table: "PicklistUS");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Picklists");

            migrationBuilder.RenameTable(
                name: "PicklistUS",
                newName: "PicklistUs");

            migrationBuilder.RenameColumn(
                name: "USCode",
                table: "PicklistUs",
                newName: "Nom");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PicklistUs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PicklistId",
                table: "PicklistUs",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_PicklistUS_PicklistId",
                table: "PicklistUs",
                newName: "IX_PicklistUs_StatusId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Picklists",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Picklists",
                newName: "StatusId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PicklistUs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "DetailPicklistId",
                table: "PicklistUs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Quantite",
                table: "PicklistUs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LineId",
                table: "Picklists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Picklists",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Picklists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Picklists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PicklistUs",
                table: "PicklistUs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeProduit = table.Column<string>(type: "text", nullable: false),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    DateAjout = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DateInventaire = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Article = table.Column<string>(type: "text", nullable: false),
                    UsCode = table.Column<string>(type: "text", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsCode = table.Column<string>(type: "text", nullable: false),
                    ArticleCode = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SapId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailInventory_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailInventory_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailInventory_Sap_SapId",
                        column: x => x.SapId,
                        principalTable: "Sap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailInventory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailPicklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Emplacement = table.Column<string>(type: "text", nullable: false),
                    Quantite = table.Column<string>(type: "text", nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    PicklistId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPicklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailPicklist_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailPicklist_Picklists_PicklistId",
                        column: x => x.PicklistId,
                        principalTable: "Picklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailPicklist_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateRetour = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Quantite = table.Column<string>(type: "text", nullable: false),
                    UsCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnLines_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnLines_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnLines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovementTraces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsNom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateMouvement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Quantite = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DetailPicklistId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementTraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementTraces_DetailPicklist_DetailPicklistId",
                        column: x => x.DetailPicklistId,
                        principalTable: "DetailPicklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovementTraces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicklistUs_DetailPicklistId",
                table: "PicklistUs",
                column: "DetailPicklistId");

            migrationBuilder.CreateIndex(
                name: "IX_PicklistUs_UserId",
                table: "PicklistUs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Picklists_LineId",
                table: "Picklists",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Picklists_StatusId",
                table: "Picklists",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Picklists_WarehouseId",
                table: "Picklists",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_WarehouseId",
                table: "Locations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventory_InventoryId",
                table: "DetailInventory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventory_LocationId",
                table: "DetailInventory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventory_SapId",
                table: "DetailInventory",
                column: "SapId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInventory_UserId",
                table: "DetailInventory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPicklist_ArticleId",
                table: "DetailPicklist",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPicklist_PicklistId",
                table: "DetailPicklist",
                column: "PicklistId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPicklist_StatusId",
                table: "DetailPicklist",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementTraces_DetailPicklistId",
                table: "MovementTraces",
                column: "DetailPicklistId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementTraces_UserId",
                table: "MovementTraces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_ArticleId",
                table: "ReturnLines",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_StatusId",
                table: "ReturnLines",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLines_UserId",
                table: "ReturnLines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouse",
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
                name: "FK_PicklistUs_Users_UserId",
                table: "PicklistUs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouse_WarehouseId",
                table: "Locations");

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
                name: "FK_PicklistUs_Users_UserId",
                table: "PicklistUs");

            migrationBuilder.DropTable(
                name: "DetailInventory");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "MovementTraces");

            migrationBuilder.DropTable(
                name: "ReturnLines");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Sap");

            migrationBuilder.DropTable(
                name: "DetailPicklist");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PicklistUs",
                table: "PicklistUs");

            migrationBuilder.DropIndex(
                name: "IX_PicklistUs_DetailPicklistId",
                table: "PicklistUs");

            migrationBuilder.DropIndex(
                name: "IX_PicklistUs_UserId",
                table: "PicklistUs");

            migrationBuilder.DropIndex(
                name: "IX_Picklists_LineId",
                table: "Picklists");

            migrationBuilder.DropIndex(
                name: "IX_Picklists_StatusId",
                table: "Picklists");

            migrationBuilder.DropIndex(
                name: "IX_Picklists_WarehouseId",
                table: "Picklists");

            migrationBuilder.DropIndex(
                name: "IX_Locations_WarehouseId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PicklistUs");

            migrationBuilder.DropColumn(
                name: "DetailPicklistId",
                table: "PicklistUs");

            migrationBuilder.DropColumn(
                name: "Quantite",
                table: "PicklistUs");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "Picklists");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Picklists");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Picklists");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Picklists");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "PicklistUs",
                newName: "PicklistUS");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PicklistUS",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "PicklistUS",
                newName: "PicklistId");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "PicklistUS",
                newName: "USCode");

            migrationBuilder.RenameIndex(
                name: "IX_PicklistUs_StatusId",
                table: "PicklistUS",
                newName: "IX_PicklistUS_PicklistId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Picklists",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Picklists",
                newName: "CreatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Picklists",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PicklistUS",
                table: "PicklistUS",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApproValidatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    QualityComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    QualityValidatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    USCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ValidatedByAppro = table.Column<bool>(type: "boolean", nullable: true),
                    ValidatedByQuality = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitStockId = table.Column<int>(type: "integer", nullable: false),
                    FromLocationCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MovedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MovementDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ToLocationCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_UnitStocks_UnitStockId",
                        column: x => x.UnitStockId,
                        principalTable: "UnitStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_UnitStockId",
                table: "StockMovements",
                column: "UnitStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_PicklistUS_Picklists_PicklistId",
                table: "PicklistUS",
                column: "PicklistId",
                principalTable: "Picklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
