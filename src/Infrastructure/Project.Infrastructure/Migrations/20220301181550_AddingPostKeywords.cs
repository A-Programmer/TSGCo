using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Infrastructure.Migrations
{
    public partial class AddingPostKeywords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PostKeywords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostKeywords",
                table: "PostKeywords",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostKeywords",
                table: "PostKeywords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostKeywords");
        }
    }
}
