using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab23.Migrations
{
    public partial class AddedTableForCountingTotalRequestAndBlockedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TotalRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalRequestCount = table.Column<int>(type: "int", nullable: false),
                    DateTillUsersBlocked = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalRequests");
        }
    }
}
