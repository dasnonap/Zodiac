using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ChangePrimaryKeysIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Types_AppUserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "Types",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Types_Id",
                table: "Users",
                column: "Id",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Types_Id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Types",
                newName: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Types_AppUserId",
                table: "Users",
                column: "AppUserId",
                principalTable: "Types",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
