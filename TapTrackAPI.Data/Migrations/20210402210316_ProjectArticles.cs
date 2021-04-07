using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class ProjectArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BelongsToId",
                table: "Articles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Articles_BelongsToId",
                table: "Articles",
                column: "BelongsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Projects_BelongsToId",
                table: "Articles",
                column: "BelongsToId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Projects_BelongsToId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_BelongsToId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "BelongsToId",
                table: "Articles");
        }
    }
}
