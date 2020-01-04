using Microsoft.EntityFrameworkCore.Migrations;

namespace CC.Blog.Migrations
{
    public partial class lll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkType",
                table: "FriendshipLinks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkType",
                table: "FriendshipLinks");
        }
    }
}
