using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class Codeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "restorationCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "restorationCodes");
        }
    }
}
