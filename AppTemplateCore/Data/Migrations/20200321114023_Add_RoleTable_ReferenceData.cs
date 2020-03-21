using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTemplateCore.Data.Migrations
{
    public partial class Add_RoleTable_ReferenceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c6e94c2-e03b-4c65-ace7-69b258d4f30a", "2f857335-be5a-4554-bf3a-c8f6d5e96506", "AdminUser", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2c27802-a2ab-49f1-844e-592ed77c9e4b", "9d8879c2-8030-46e2-a31b-ab7efa1781c7", "DeptFooUser", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c6e94c2-e03b-4c65-ace7-69b258d4f30a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2c27802-a2ab-49f1-844e-592ed77c9e4b");
        }
    }
}
