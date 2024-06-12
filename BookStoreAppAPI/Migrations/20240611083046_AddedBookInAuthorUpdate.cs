using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookInAuthorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2515af1f-70d1-4b2d-a624-844a885fa35f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ef93845-1abb-4d6c-8f56-ab0617dc5b68", "AQAAAAIAAYagAAAAEKiaNhDoS1egFsODrT7oQUo3tpGK7NDlVBP4RJuwMyr4kzblUrmua8ohMzaDYOmTBg==", "cbeb120d-48a5-48d0-a00e-c751346cdd24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4263b6b2-0615-466c-9fcd-93e43e9ea399", "AQAAAAIAAYagAAAAEETfdnIjA+pXL0iZZk40nTpYFoNE945vbYSmTjMyzPprJ7ayd8zBVP629dypiLUuZQ==", "79e336e1-5b40-4146-b08e-5d5c644551ca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2515af1f-70d1-4b2d-a624-844a885fa35f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aaf1d253-7573-445b-9689-e7eac475e7b1", "AQAAAAIAAYagAAAAEBtE9Gu+kQgHzAGESIRtGWmoUnxbL8rznzSwlu9bFp3at3FulZosOOFsHxypAmqXFA==", "97628b9a-3c99-45ae-948b-0fe1f5a40dfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbaa2eb2-e9b3-4beb-8eed-dc2fe2bbf2b8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aff66bf8-df77-45ca-9c42-c810a76a12d3", "AQAAAAIAAYagAAAAENTF73weZY12wikhTAWGqhZzTvXqJB4bcDWurBUhf8pbpmMyhPzXmt98UWIK3Pz3Ww==", "9587ebd9-36c8-43bb-ba8e-c11eb8e72ab0" });
        }
    }
}
