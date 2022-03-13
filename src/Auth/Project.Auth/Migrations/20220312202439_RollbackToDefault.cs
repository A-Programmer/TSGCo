using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Auth.Migrations
{
    public partial class RollbackToDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityResources",
                keyColumn: "Id",
                keyValue: new Guid("789361cb-01c5-4245-858f-c4488d787ab0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2689d5d5-2508-4568-900b-ba03ff8103a3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e617647a-e5fd-43f2-aa45-0bd3a1ea563e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserLogins",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserLogins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "UserClaims",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "UserClaims",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserClaims",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityResources",
                columns: new[] { "Id", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "Required", "ShowInDiscoveryDocument" },
                values: new object[] { new Guid("57a6db13-1489-498c-89ba-5c72df94e8bb"), "Your roles", "Roles", false, true, "roles", true, true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsActive", "UserName" },
                values: new object[] { new Guid("3a4dce42-8a05-462d-8307-d9caaa0c094e"), "BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=", true, "user" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsActive", "UserName" },
                values: new object[] { new Guid("bd2ba071-4e06-4df8-8d40-4d1ab3c098ca"), "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", true, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DeleteData(
                table: "IdentityResources",
                keyColumn: "Id",
                keyValue: new Guid("57a6db13-1489-498c-89ba-5c72df94e8bb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3a4dce42-8a05-462d-8307-d9caaa0c094e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bd2ba071-4e06-4df8-8d40-4d1ab3c098ca"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserClaims");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "UserClaims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "UserClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.InsertData(
                table: "IdentityResources",
                columns: new[] { "Id", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "Required", "ShowInDiscoveryDocument" },
                values: new object[] { new Guid("789361cb-01c5-4245-858f-c4488d787ab0"), "Your roles", "Roles", false, true, "roles", true, true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsActive", "UserName" },
                values: new object[] { new Guid("2689d5d5-2508-4568-900b-ba03ff8103a3"), "BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=", true, "user" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "IsActive", "UserName" },
                values: new object[] { new Guid("e617647a-e5fd-43f2-aa45-0bd3a1ea563e"), "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", true, "admin" });
        }
    }
}
