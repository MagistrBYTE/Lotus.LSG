using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    public partial class AddVillageSettlement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address_village_settlement",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sname = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    village_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_village_settlement", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address_village",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    village_type = table.Column<int>(type: "int", nullable: false),
                    oktmo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    okato = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    village_sett_id = table.Column<long>(type: "bigint", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_village", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_village_address_village_settlement_village_sett_id",
                        column: x => x.village_sett_id,
                        principalTable: "address_village_settlement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address_street",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    street_type = table.Column<int>(type: "int", nullable: false),
                    village_id = table.Column<long>(type: "bigint", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_street", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_street_address_village_village_id",
                        column: x => x.village_id,
                        principalTable: "address_village",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address_item",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    element_type = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cadastral_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    сode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_item_address_street_street_id",
                        column: x => x.street_id,
                        principalTable: "address_street",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "address_village_settlement",
                columns: new[] { "id", "names", "sname", "village_type" },
                values: new object[,]
                {
                    { 1L, "Андреевское сельское поселение", "Андреевское СП", "Cельское поселение" },
                    { 2L, "Атамановское сельское поселение", "Атамановское СП", "Cельское поселение" },
                    { 3L, "Белокаменское сельское поселение", "Белокаменское СП", "Cельское поселение" },
                    { 4L, "Боровское сельское поселение", "Боровское СП", "Cельское поселение" },
                    { 5L, "Брединское сельское поселение", "Брединское СП", "Cельское поселение" },
                    { 6L, "Калининское сельское поселение", "Калининское СП", "Cельское поселение" },
                    { 7L, "Княженское сельское поселение", "Княженское СП", "Cельское поселение" },
                    { 8L, "Комсомольское сельское поселение", "Комсомольское СП", "Cельское поселение" },
                    { 9L, "Наследницкое сельское поселение", "Наследницкое СП", "Cельское поселение" },
                    { 10L, "Павловское сельское поселение", "Павловское СП", "Cельское поселение" },
                    { 11L, "Рымникское сельское поселение", "Рымникское СП", "Cельское поселение" }
                });

            migrationBuilder.InsertData(
                table: "address_village",
                columns: new[] { "id", "names", "okato", "oktmo", "PostalCode", "village_sett_id", "village_type" },
                values: new object[,]
                {
                    { 100L, "Андреевский", null, null, null, 1L, 0 },
                    { 101L, "Мариинский", null, null, null, 1L, 0 },
                    { 200L, "Атамановский", null, null, null, 2L, 0 },
                    { 201L, "Степной", null, null, null, 2L, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_item_id",
                table: "address_item",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_address_item_street_id",
                table: "address_item",
                column: "street_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_street_id",
                table: "address_street",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_address_street_village_id",
                table: "address_street",
                column: "village_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_village_id",
                table: "address_village",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_address_village_village_sett_id",
                table: "address_village",
                column: "village_sett_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_village_settlement_id",
                table: "address_village_settlement",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address_item");

            migrationBuilder.DropTable(
                name: "address_street");

            migrationBuilder.DropTable(
                name: "address_village");

            migrationBuilder.DropTable(
                name: "address_village_settlement");
        }
    }
}
