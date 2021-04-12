using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class Restoration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId",
                table: "Issues");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Issues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "restorationCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdVisible = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restorationCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_restorationCodes_IdVisible",
                table: "restorationCodes",
                column: "IdVisible",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId",
                table: "Issues",
                column: "CreatorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "restorationCodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Issues",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Issues",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId",
                table: "Issues",
                column: "CreatorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
