using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations.CRepositoryDatabaseMigrations
{
    public partial class AddPublicAuthority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "subject_civil",
                columns: new[] { "id", "Discriminator", "inn", "kpp", "leader_name", "leader_post", "names", "ogrn", "okpo", "okved", "public_type", "sname", "civil_type" },
                values: new object[,]
                {
                    { 1000L, "CPublicAuthority", "7427003567", "745801001", null, null, "Администрация Андреевского сельского поселения", "1027401514436", null, null, 0, "Администрация Андреевского СП", 2 },
                    { 1002L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Атамановского сельского поселения", "1027401514425", null, null, 0, "Администрация Атамановского СП", 2 },
                    { 1003L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Белокаменского сельского поселения", "1027401514425", null, null, 0, "Администрация Белокаменского СП", 2 },
                    { 1004L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Боровского сельского поселения", "1027401514425", null, null, 0, "Администрация Боровского СП", 2 },
                    { 1005L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Брединского сельского поселения", "1027401514425", null, null, 0, "Администрация Брединского СП", 2 },
                    { 1006L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Калининского сельского поселения", "1027401514425", null, null, 0, "Администрация Калининского СП", 2 },
                    { 1007L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Княженского сельского поселения", "1027401514425", null, null, 0, "Администрация Княженского СП", 2 },
                    { 1008L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Комсомольского сельского поселения", "1027401514425", null, null, 0, "Администрация Комсомольского СП", 2 },
                    { 1009L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Наследницкого сельского поселения", "1027401514425", null, null, 0, "Администрация Наследницкого СП", 2 },
                    { 1010L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Павловского сельского поселения", "1027401514425", null, null, 0, "Администрация Павловского СП", 2 },
                    { 1011L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Рымникского сельского поселения", "1027401514425", null, null, 0, "Администрация Рымникского СП", 2 },
                    { 1012L, "CPublicAuthority", "7427004708", "745801001", null, null, "Администрация Брединского муниципального района", "1027401514425", null, null, 0, "Администрация района", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1000L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1002L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1003L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1004L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1005L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1006L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1007L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1008L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1009L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1010L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1011L);

            migrationBuilder.DeleteData(
                table: "subject_civil",
                keyColumn: "id",
                keyValue: 1012L);
        }
    }
}
