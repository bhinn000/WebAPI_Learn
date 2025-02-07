using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_Learn.Migrations
{
    /// <inheritdoc />
    public partial class PopulateWithSomeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Address", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Nepal", "b@gmail.com", "Bhintuna" },
                    { 2, "Nepal2", "b@gmail.com", "Shakya" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
