using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class AddUserContact : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IdVisible = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactInfo = table.Column<string>(type: "text", nullable: true),
                    NotificationEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdVisible = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserContacts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserContacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "IdVisible", "Name" },
                values: new object[,]
                {
                    { new Guid("90ebd5b2-100c-4437-8e02-dd88b8798af5"), null, "Telegram" },
                    { new Guid("b09f894e-db3a-4be1-a147-6efe5c154149"), null, "Discord" },
                    { new Guid("e22ec568-f17f-446c-8765-de2de54a8de2"), null, "Skype" },
                    { new Guid("325b38a1-494c-478f-a6f4-5a8ee5f12b36"), null, "GitHub" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypes_IdVisible",
                table: "ContactTypes",
                column: "IdVisible",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_ContactTypeId",
                table: "UserContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_IdVisible",
                table: "UserContacts",
                column: "IdVisible",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_UserId",
                table: "UserContacts",
                column: "UserId");

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
                name: "UserContacts");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers");

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
