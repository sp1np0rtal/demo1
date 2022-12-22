using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoMVCCore.Migrations
{
    public partial class SeedEmployeesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Dept", "Email", "Name", "age" },
                values: new object[] { 1, 1, "mark@goo.com", "Mark", "22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
