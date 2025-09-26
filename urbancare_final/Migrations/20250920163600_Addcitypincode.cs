using Microsoft.EntityFrameworkCore.Migrations;

namespace urbancare_final.Migrations
{
    public partial class Addcitypincode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Problems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pincode",
                table: "Problems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "pincode",
                table: "Problems");
        }
    }
}
