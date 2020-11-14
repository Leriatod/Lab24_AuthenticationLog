using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab23.Migrations
{
    public partial class SeedRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT TotalRequests ON");
            migrationBuilder.Sql("INSERT INTO TotalRequests(Id, TotalRequestCount, DateTillUsersBlocked) VALUES (1, 0, '2020/11/14')");
            migrationBuilder.Sql("SET IDENTITY_INSERT TotalRequests OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
