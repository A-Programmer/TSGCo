using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Auth.Migrations
{
    public partial class ModifyingPersistedGrantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersistedGrants",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersistedGrants");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "PersistedGrants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersistedGrants",
                table: "PersistedGrants",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersistedGrants",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "PersistedGrants");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersistedGrants",
                type: "uniqueidentifier",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersistedGrants",
                table: "PersistedGrants",
                column: "Id");
        }
    }
}
