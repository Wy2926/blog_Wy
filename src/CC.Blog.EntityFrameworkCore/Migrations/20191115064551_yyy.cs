using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Blog.Migrations
{
    public partial class yyy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Spiders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "Spiders",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Spiders");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "Spiders");
        }
    }
}
