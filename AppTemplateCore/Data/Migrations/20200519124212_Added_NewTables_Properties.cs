using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTemplateCore.Data.Migrations
{
    public partial class Added_NewTables_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Province",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Religion",
                table: "Persons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Persons");
        }
    }
}
