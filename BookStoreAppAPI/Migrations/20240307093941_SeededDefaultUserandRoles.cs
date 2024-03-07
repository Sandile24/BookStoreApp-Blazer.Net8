using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00177bda-de97-4a85-9058-675612e8e1a6", null, "Administrator", "ADMINISTARTOR" },
                    { "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2515af1f-70d1-4b2d-a624-844a885fa35f", 0, "aaf1d253-7573-445b-9689-e7eac475e7b1", "skhomo@beier.co.za", false, "Eric", "Khomo", false, null, "SKHOMO@BEIER.CO.ZA", "SKHOMO@BEIER.CO.ZA", "AQAAAAIAAYagAAAAEBtE9Gu+kQgHzAGESIRtGWmoUnxbL8rznzSwlu9bFp3at3FulZosOOFsHxypAmqXFA==", null, false, "97628b9a-3c99-45ae-948b-0fe1f5a40dfb", false, "skhomo@beier.co.za" },
                    { "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8", 0, "aff66bf8-df77-45ca-9c42-c810a76a12d3", "smboweni@beier.co.za", false, "Zanele", "Mboweni", false, null, "SMBOWENI@BEIER.CO.ZA", "SMBOWENI@BEIER.CO.ZA", "AQAAAAIAAYagAAAAENTF73weZY12wikhTAWGqhZzTvXqJB4bcDWurBUhf8pbpmMyhPzXmt98UWIK3Pz3Ww==", null, false, "9587ebd9-36c8-43bb-ba8e-c11eb8e72ab0", false, "skhomo@beier.co.za" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00177bda-de97-4a85-9058-675612e8e1a6", "2515af1f-70d1-4b2d-a624-844a885fa35f" },
                    { "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1", "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00177bda-de97-4a85-9058-675612e8e1a6", "2515af1f-70d1-4b2d-a624-844a885fa35f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1", "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00177bda-de97-4a85-9058-675612e8e1a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cee0008b - bcf2 - 4b71 - 8fd4 - f90e0e1548c1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2515af1f-70d1-4b2d-a624-844a885fa35f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8");
        }
    }
}
