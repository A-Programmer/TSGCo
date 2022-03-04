using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Infrastructure.Migrations
{
    public partial class TurningOffLazyLoading3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostVotes",
                table: "PostVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostViews",
                table: "PostViews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostVotes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostViews");

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_PostId",
                table: "PostViews",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes");

            migrationBuilder.DropIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes");

            migrationBuilder.DropIndex(
                name: "IX_PostViews_PostId",
                table: "PostViews");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostVotes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostViews",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostVotes",
                table: "PostVotes",
                columns: new[] { "PostId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostViews",
                table: "PostViews",
                columns: new[] { "PostId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
