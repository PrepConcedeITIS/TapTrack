using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class CreateIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues");

            migrationBuilder.AlterColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues");

            migrationBuilder.AlterColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
