using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwetter_Security_API.Dal.Migrations
{
    public partial class ChangeToUserIdInsteadOfClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Following_Users_FollowingUserId",
                table: "Following");

            migrationBuilder.DropForeignKey(
                name: "FK_Following_Users_UserId",
                table: "Following");

            migrationBuilder.DropIndex(
                name: "IX_Following_FollowingUserId",
                table: "Following");

            migrationBuilder.DropIndex(
                name: "IX_Following_UserId",
                table: "Following");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowingUserId",
                table: "Following",
                column: "FollowingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Following_UserId",
                table: "Following",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Following_Users_FollowingUserId",
                table: "Following",
                column: "FollowingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Following_Users_UserId",
                table: "Following",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
