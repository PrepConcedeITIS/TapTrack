using System;
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

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_CreatorId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Issues");
            
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Issues",
                type: "uuid",
                nullable: false);

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");
            
            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Issues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssigneeId1",
                table: "Issues",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId1",
                table: "Issues",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId1",
                table: "Issues",
                column: "AssigneeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatorId1",
                table: "Issues",
                column: "CreatorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId1",
                table: "Issues",
                column: "AssigneeId1",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId1",
                table: "Issues",
                column: "CreatorId1",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_AssigneeId1",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_TeamMembers_CreatorId1",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId1",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_CreatorId1",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeId1",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatorId1",
                table: "Issues");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Issues",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatorId",
                table: "Issues",
                column: "CreatorId");

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
    }
}
