using Microsoft.EntityFrameworkCore.Migrations;

namespace urbancare_final.Migrations
{
    public partial class SeedDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DepartmentMasters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Health" },
                    { 5, "Public Works" },
                    { 6, "Transportation" },
                    { 7, "Education" },
                    { 8, "Others" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentMasters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DepartmentMasters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DepartmentMasters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DepartmentMasters",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DepartmentMasters",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
