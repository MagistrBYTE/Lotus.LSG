using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Web.LSG.Migrations
{
    public partial class RenamePostIs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspUserPost_PostID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "AspNetUsers",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PostID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspUserPost_PostId",
                table: "AspNetUsers",
                column: "PostId",
                principalTable: "AspUserPost",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspUserPost_PostId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "AspNetUsers",
                newName: "PostID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PostId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspUserPost_PostID",
                table: "AspNetUsers",
                column: "PostID",
                principalTable: "AspUserPost",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
