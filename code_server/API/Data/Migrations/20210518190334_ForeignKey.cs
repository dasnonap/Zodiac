using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "Types",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Types_TypeId",
                table: "Users",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Types_TypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Types",
                newName: "UserTypeId");
        }
    }
}
