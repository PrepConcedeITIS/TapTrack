﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class Comment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TeamMembers_AuthorId",
                table: "Comments");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TeamMembers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TeamMembers_AuthorId",
                table: "Comments");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Comments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TeamMembers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
