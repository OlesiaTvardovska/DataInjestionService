using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScrapper.DAL.Migrations
{
    public partial class AddMissingColumnToNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "MarkedNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "MarkedNews");
        }
    }
}
