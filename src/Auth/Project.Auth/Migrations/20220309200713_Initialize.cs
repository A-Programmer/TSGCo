using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Auth.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    SubjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Users",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Users",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "SubjectId", "IsActive", "Password", "Username" },
                values: new object[] { "b7539694-97e7-4dfe-84da-b4256e1ff5c7", true, "XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=", "u2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "SubjectId", "IsActive", "Password", "Username" },
                values: new object[] { "d860efca-22d9-47fd-8249-791ba61b07c7", true, "XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=", "u1" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "SubjectId" },
                values: new object[,]
                {
                    { 1, "given_name", "Kamran", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 2, "family_name", "Sadin", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 3, "address", "Iran, Golestan, Gonbade Kavous", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 4, "role", "admin", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 5, "role", "manager", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 6, "subscriptionlevel", "Gold", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 7, "country", "iran", "d860efca-22d9-47fd-8249-791ba61b07c7" },
                    { 8, "given_name", "John", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 9, "family_name", "Doe", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 10, "address", "Iran, Tehran", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 11, "role", "user", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 12, "role", "manager", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 13, "subscriptionlevel", "Silver", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" },
                    { 14, "country", "USA", "b7539694-97e7-4dfe-84da-b4256e1ff5c7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_SubjectId_ClaimType",
                table: "UserClaims",
                columns: new[] { "SubjectId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_SubjectId_LoginProvider",
                table: "UserLogins",
                columns: new[] { "SubjectId", "LoginProvider" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
