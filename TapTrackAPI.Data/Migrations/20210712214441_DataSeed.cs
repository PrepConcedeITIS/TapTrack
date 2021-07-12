using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapTrackAPI.Data.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae"), 0, "1a115436-503c-422f-8427-b8ddd4616f5e", "example@taptrack.tech", false, false, null, "EXAMPLE@TAPTRACK.TECH", "EXAMPLE", "AQAAAAEAACcQAAAAEILt4WXyXV8oQjM1ybmErHuKOBpQ+bxrOwtiC3lBVJzB9hAfcH/95duGwXFS48P3gA==", null, false, null, "II4PFFKVHNQ7AYZOOGQ5SV2OQI4W5FS5", false, "example" },
                    { new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3"), 0, "dc8ac0d0-4ba1-484b-94f5-094d8ce265fa", "example-common-user@taptrack.tech", false, false, null, "EXAMPLE-COMMON-USER@TAPTRACK.TECH", "EXAMPLE-COMMON-USER", "AQAAAAEAACcQAAAAEKKYCgXWYdO+ZCtStUFqA/eZQobyoMwKA5cW67EQAs/CXinWl5WcriOmzTtPVIqpOQ==", null, false, null, "PPC522W7XTGSLYXUEQKT5MR4JNGOHNT7", false, "example-common-user" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatorId", "Description", "IdVisible", "LogoUrl", "Name" },
                values: new object[] { new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"), new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae"), "Some description", "EXM", "https://www.gravatar.com/avatar/bce7a6deb01d3e6aef54e2e7344c4816?s=256&d=identicon&r=PG", "Example project" });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "ProjectId", "Role", "UserId" },
                values: new object[,]
                {
                    { 1L, new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"), "Admin", new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae") },
                    { 2L, new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"), "User", new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3") }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AssigneeId", "Created", "CreatorId", "Description", "Estimation", "IdVisible", "IssueType", "LastUpdated", "Priority", "ProjectId", "Spent", "State", "Title" },
                values: new object[] { new Guid("7cd6f23a-900e-4229-a3f0-6e5aaeda3d86"), null, new DateTime(2021, 7, 12, 21, 44, 41, 137, DateTimeKind.Utc).AddTicks(6012), 1L, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer libero, vel commodo risus luctus sed. Maecenas vitae nisi vel ex pulvinar maximus. Duis lectus et tellus volutpat, vitae laoreet metus molestie. Morbi orci orci, volutpat id congue id, consequat a lectus. Nam fermentum, odio sit amet iaculis aliquam, dui lorem rutrum nunc, mollis sollicitudin metus nisi nec libero. Ut fringilla, lorem eu vulputate sollicitudin, ipsum turpis scelerisque justo, eu tincidunt felis lectus volutpat lacus. Aenean justo leo, blandit eget dignissim eget, dignissim sit amet urna. Morbi volutpat  sed viverra . Etiam quis lacus nulla. Morbi porttitor aliquet lacus et rutrum. Etiam venenatis ex lacus, et finibus dui imperdiet non.", new TimeSpan(0, 0, 0, 0, 0), "EXM-1", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"), new TimeSpan(0, 0, 0, 0, 0), 0, "Example issue title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: new Guid("7cd6f23a-900e-4229-a3f0-6e5aaeda3d86"));

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3ce27b2f-b6c0-4a40-81e8-7a7aa12b68d3"));

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("f2f5adfe-6fe4-48e5-a1b9-3b1b1b4b2a06"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c9e155d4-c374-4ff4-a198-4559a76e99ae"));
        }
    }
}
