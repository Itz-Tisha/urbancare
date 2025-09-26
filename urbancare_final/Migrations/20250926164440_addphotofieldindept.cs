using Microsoft.EntityFrameworkCore.Migrations;

namespace urbancare_final.Migrations
{
    public partial class addphotofieldindept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Departments");
        }
    }
}
