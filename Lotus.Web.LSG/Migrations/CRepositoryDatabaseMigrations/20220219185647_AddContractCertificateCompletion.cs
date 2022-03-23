using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    public partial class AddContractCertificateCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    subject = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    group = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subgroup = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    value_unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_conclusion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deadline = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: true),
                    contractor_id = table.Column<long>(type: "bigint", nullable: true),
                    price_local = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_regional = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_federal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_extra = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    not_calc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.id);
                    table.ForeignKey(
                        name: "FK_contract_subject_civil_contractor_id",
                        column: x => x.contractor_id,
                        principalTable: "subject_civil",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_contract_subject_civil_customer_id",
                        column: x => x.customer_id,
                        principalTable: "subject_civil",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "certificate_completion",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    group = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    value_unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    begin_period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_period = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    contract_id = table.Column<long>(type: "bigint", nullable: true),
                    price_local = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_regional = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_federal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    price_extra = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    not_calc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    names = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificate_completion", x => x.id);
                    table.ForeignKey(
                        name: "FK_certificate_completion_contract_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contract",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_certificate_completion_contract_id",
                table: "certificate_completion",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificate_completion_id",
                table: "certificate_completion",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contract_contractor_id",
                table: "contract",
                column: "contractor_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_customer_id",
                table: "contract",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_id",
                table: "contract",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificate_completion");

            migrationBuilder.DropTable(
                name: "contract");
        }
    }
}
