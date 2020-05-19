using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTemplateCore.Data.Migrations
{
    public partial class RegStatus_Column_Renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Persons",
                newName: "IsRegistered");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRegistered",
                table: "Persons",
                newName: "Gender");
        }
    }
}
