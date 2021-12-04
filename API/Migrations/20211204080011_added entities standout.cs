using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addedentitiesstandout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sroleone",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sroletwo",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sroleone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sroletwo",
                table: "Users");
        }
    }
}
