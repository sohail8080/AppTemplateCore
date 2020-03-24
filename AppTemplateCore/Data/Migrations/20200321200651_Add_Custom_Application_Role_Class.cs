using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTemplateCore.Data.Migrations
{
    public partial class Add_Custom_Application_Role_Class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3e489df-35a4-44bc-9591-978a219eb947", "bb153a6a-ed34-4eec-8960-c64f96b672de", "AdminUser", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5347cc3b-44bf-4ecb-84f4-1be9fbc8949f", "20d97b72-a764-4ea5-8717-db11a60a929b", "DeptFooUser", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5347cc3b-44bf-4ecb-84f4-1be9fbc8949f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3e489df-35a4-44bc-9591-978a219eb947");
           
        }
    }
}
