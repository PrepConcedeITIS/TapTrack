using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class IdVisibleUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_IdVisible",
                table: "Projects",
                column: "IdVisible",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IdVisible",
                table: "Issues",
                column: "IdVisible",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdVisible",
                table: "Comments",
                column: "IdVisible",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_IdVisible",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Issues_IdVisible",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IdVisible",
                table: "Comments");
        }
    }
}
