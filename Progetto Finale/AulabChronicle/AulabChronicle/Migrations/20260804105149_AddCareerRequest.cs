using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AulabChronicle.Migrations
{
    /// <inheritdoc />
    public partial class AddCareerRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareerRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsChecked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerRequests_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a71bdca4-500b-4bd4-9a40-4ce084883d6a", null, "Admin", "ADMIN" },
                    { "b845423f-422d-4235-9f6b-76f2d22d2f2d", null, "Writer", "WRITER" },
                    { "c577002b-a010-449e-990c-99c0d10c1d1a", null, "Revisor", "REVISOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b1234567-xxxx-xxxx-xxxx-xxxxxxxxxxxx", 0, "62dcb489-3b83-4a7f-a0d0-af72aefd8e5b", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAELGKcpfoeXlFqBqTz+kkzIymV615SzxfyoWuyUia2PFpsIaSBxIMc4YwIKvNukTqjA==", null, false, "ecc42e58-fc03-4d88-8fe8-1690d6ec9c67", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a71bdca4-500b-4bd4-9a40-4ce084883d6a", "b1234567-xxxx-xxxx-xxxx-xxxxxxxxxxxx" });

            migrationBuilder.CreateIndex(
                name: "IX_CareerRequests_RoleId",
                table: "CareerRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerRequests_UserId",
                table: "CareerRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b845423f-422d-4235-9f6b-76f2d22d2f2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c577002b-a010-449e-990c-99c0d10c1d1a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a71bdca4-500b-4bd4-9a40-4ce084883d6a", "b1234567-xxxx-xxxx-xxxx-xxxxxxxxxxxx" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a71bdca4-500b-4bd4-9a40-4ce084883d6a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b1234567-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
        }
    }
}
