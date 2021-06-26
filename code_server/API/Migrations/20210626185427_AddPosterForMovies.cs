using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddPosterForMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Poster",
                table: "Films",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Films");
        }
    }
}
