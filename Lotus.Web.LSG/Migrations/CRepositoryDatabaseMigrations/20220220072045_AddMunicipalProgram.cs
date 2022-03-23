using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    public partial class AddMunicipalProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CMunicipalProgramActivityId",
                table: "contract",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "municipal_program",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sname = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    desc = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    begin_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    edition_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    edition_document = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    not_calc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_program", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "municipal_sub_program",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sname = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    desc = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    program_id = table.Column<long>(type: "bigint", nullable: true),
                    not_calc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_sub_program", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipal_sub_program_municipal_program_program_id",
                        column: x => x.program_id,
                        principalTable: "municipal_program",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "municipal_indicator",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    desc = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    program_id = table.Column<long>(type: "bigint", nullable: false),
                    sub_program_id = table.Column<long>(type: "bigint", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_indicator", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipal_indicator_municipal_program_program_id",
                        column: x => x.program_id,
                        principalTable: "municipal_program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_indicator_municipal_sub_program_sub_program_id",
                        column: x => x.sub_program_id,
                        principalTable: "municipal_sub_program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "municipal_activity",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stage = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    desc = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    group = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subgroup = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    begin_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    executor_id = table.Column<long>(type: "bigint", nullable: false),
                    program_id = table.Column<long>(type: "bigint", nullable: false),
                    sub_program_id = table.Column<long>(type: "bigint", nullable: false),
                    indicator_id = table.Column<long>(type: "bigint", nullable: false),
                    activity_id = table.Column<long>(type: "bigint", nullable: true),
                    planed_value = table.Column<double>(type: "double", nullable: false),
                    price_local = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_regional = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_federal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_extra = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    not_calc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    names = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipal_activity", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipal_activity_municipal_activity_activity_id",
                        column: x => x.activity_id,
                        principalTable: "municipal_activity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_municipal_activity_municipal_indicator_indicator_id",
                        column: x => x.indicator_id,
                        principalTable: "municipal_indicator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_activity_municipal_program_program_id",
                        column: x => x.program_id,
                        principalTable: "municipal_program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_activity_municipal_sub_program_sub_program_id",
                        column: x => x.sub_program_id,
                        principalTable: "municipal_sub_program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_municipal_activity_subject_civil_executor_id",
                        column: x => x.executor_id,
                        principalTable: "subject_civil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_contract_CMunicipalProgramActivityId",
                table: "contract",
                column: "CMunicipalProgramActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_activity_id",
                table: "municipal_activity",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_executor_id",
                table: "municipal_activity",
                column: "executor_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_id",
                table: "municipal_activity",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_indicator_id",
                table: "municipal_activity",
                column: "indicator_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_program_id",
                table: "municipal_activity",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_activity_sub_program_id",
                table: "municipal_activity",
                column: "sub_program_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_indicator_id",
                table: "municipal_indicator",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipal_indicator_program_id",
                table: "municipal_indicator",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_indicator_sub_program_id",
                table: "municipal_indicator",
                column: "sub_program_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipal_program_id",
                table: "municipal_program",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipal_sub_program_id",
                table: "municipal_sub_program",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipal_sub_program_program_id",
                table: "municipal_sub_program",
                column: "program_id");

            migrationBuilder.AddForeignKey(
                name: "FK_contract_municipal_activity_CMunicipalProgramActivityId",
                table: "contract",
                column: "CMunicipalProgramActivityId",
                principalTable: "municipal_activity",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contract_municipal_activity_CMunicipalProgramActivityId",
                table: "contract");

            migrationBuilder.DropTable(
                name: "municipal_activity");

            migrationBuilder.DropTable(
                name: "municipal_indicator");

            migrationBuilder.DropTable(
                name: "municipal_sub_program");

            migrationBuilder.DropTable(
                name: "municipal_program");

            migrationBuilder.DropIndex(
                name: "IX_contract_CMunicipalProgramActivityId",
                table: "contract");

            migrationBuilder.DropColumn(
                name: "CMunicipalProgramActivityId",
                table: "contract");
        }
    }
}
