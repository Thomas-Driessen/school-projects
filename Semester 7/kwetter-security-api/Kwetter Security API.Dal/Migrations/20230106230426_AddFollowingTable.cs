using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwetter_Security_API.Dal.Migrations
{
    public partial class AddFollowingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Following",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowingUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UnfollowDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsFollowing = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Following", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Following_Users_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Following_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowingUserId",
                table: "Following",
                column: "FollowingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Following_UserId",
                table: "Following",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Following");
        }
    }
}
