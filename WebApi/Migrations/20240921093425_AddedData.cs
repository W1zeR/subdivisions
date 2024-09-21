using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subdivisions",
                columns: new[] { "Id", "IsActive", "MainId", "Name" },
                values: new object[,]
                {
                    { 1, true, null, "Подразделение" },
                    { 4, false, null, "Отдел" },
                    { 2, false, 1, "Часть подразделения" },
                    { 3, true, 4, "Часть отдела" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
