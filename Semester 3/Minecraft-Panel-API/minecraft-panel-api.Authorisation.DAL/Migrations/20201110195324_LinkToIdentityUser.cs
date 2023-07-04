using Microsoft.EntityFrameworkCore.Migrations;

namespace minecraft_panel_api.Authorisation.DAL.Migrations
{
    public partial class LinkToIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisteredAccountId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegisteredAccountId",
                table: "Users",
                column: "RegisteredAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_RegisteredAccountId",
                table: "Users",
                column: "RegisteredAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_RegisteredAccountId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RegisteredAccountId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredAccountId",
                table: "Users");
        }
    }
}
