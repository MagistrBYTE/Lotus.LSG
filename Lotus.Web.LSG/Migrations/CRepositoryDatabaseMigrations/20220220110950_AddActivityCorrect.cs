using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    public partial class AddActivityCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_indicator_indicator_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_program_program_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_sub_program_sub_program_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_subject_civil_executor_id",
                table: "municipal_activity");

            migrationBuilder.AlterColumn<string>(
                name: "subgroup",
                table: "municipal_activity",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "sub_program_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "program_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "municipal_activity",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "indicator_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "municipal_activity",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "executor_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "desc",
                table: "municipal_activity",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_indicator_indicator_id",
                table: "municipal_activity",
                column: "indicator_id",
                principalTable: "municipal_indicator",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_program_program_id",
                table: "municipal_activity",
                column: "program_id",
                principalTable: "municipal_program",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_sub_program_sub_program_id",
                table: "municipal_activity",
                column: "sub_program_id",
                principalTable: "municipal_sub_program",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_subject_civil_executor_id",
                table: "municipal_activity",
                column: "executor_id",
                principalTable: "subject_civil",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_indicator_indicator_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_program_program_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_municipal_sub_program_sub_program_id",
                table: "municipal_activity");

            migrationBuilder.DropForeignKey(
                name: "FK_municipal_activity_subject_civil_executor_id",
                table: "municipal_activity");

            migrationBuilder.UpdateData(
                table: "municipal_activity",
                keyColumn: "subgroup",
                keyValue: null,
                column: "subgroup",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "subgroup",
                table: "municipal_activity",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "sub_program_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "program_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "municipal_activity",
                keyColumn: "number",
                keyValue: null,
                column: "number",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "municipal_activity",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "indicator_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "municipal_activity",
                keyColumn: "group",
                keyValue: null,
                column: "group",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "municipal_activity",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "executor_id",
                table: "municipal_activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "municipal_activity",
                keyColumn: "desc",
                keyValue: null,
                column: "desc",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "desc",
                table: "municipal_activity",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_indicator_indicator_id",
                table: "municipal_activity",
                column: "indicator_id",
                principalTable: "municipal_indicator",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_program_program_id",
                table: "municipal_activity",
                column: "program_id",
                principalTable: "municipal_program",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_municipal_sub_program_sub_program_id",
                table: "municipal_activity",
                column: "sub_program_id",
                principalTable: "municipal_sub_program",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_municipal_activity_subject_civil_executor_id",
                table: "municipal_activity",
                column: "executor_id",
                principalTable: "subject_civil",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
