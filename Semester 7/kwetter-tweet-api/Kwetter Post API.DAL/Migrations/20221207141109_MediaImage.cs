using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwetter_Post_API.DAL.Migrations
{
    public partial class MediaImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaImage",
                table: "Posts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaImage",
                table: "Posts");
        }
    }
}
