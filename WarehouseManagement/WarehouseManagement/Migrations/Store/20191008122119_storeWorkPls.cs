using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseManagement.Migrations.Store
{
    public partial class storeWorkPls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "storeCathegories",
                columns: table => new
                {
                    storeCathegoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storeCathegories", x => x.storeCathegoryId);
                });

            migrationBuilder.CreateTable(
                name: "storeItems",
                columns: table => new
                {
                    storeItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    cathegoriesstoreCathegoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storeItems", x => x.storeItemId);
                    table.ForeignKey(
                        name: "FK_storeItems_storeCathegories_cathegoriesstoreCathegoryId",
                        column: x => x.cathegoriesstoreCathegoryId,
                        principalTable: "storeCathegories",
                        principalColumn: "storeCathegoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_storeItems_cathegoriesstoreCathegoryId",
                table: "storeItems",
                column: "cathegoriesstoreCathegoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storeItems");

            migrationBuilder.DropTable(
                name: "storeCathegories");
        }
    }
}
