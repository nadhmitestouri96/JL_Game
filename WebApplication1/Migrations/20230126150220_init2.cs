using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserGame_GameId",
                table: "UserGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_UserId",
                table: "UserGame",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlateformGame_GameId",
                table: "PlateformGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlateformGame_PlatformId",
                table: "PlateformGame",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlateformGame_Game_GameId",
                table: "PlateformGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlateformGame_Platform_PlatformId",
                table: "PlateformGame",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Game_GameId",
                table: "UserGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_User_UserId",
                table: "UserGame",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlateformGame_Game_GameId",
                table: "PlateformGame");

            migrationBuilder.DropForeignKey(
                name: "FK_PlateformGame_Platform_PlatformId",
                table: "PlateformGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Game_GameId",
                table: "UserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_User_UserId",
                table: "UserGame");

            migrationBuilder.DropIndex(
                name: "IX_UserGame_GameId",
                table: "UserGame");

            migrationBuilder.DropIndex(
                name: "IX_UserGame_UserId",
                table: "UserGame");

            migrationBuilder.DropIndex(
                name: "IX_PlateformGame_GameId",
                table: "PlateformGame");

            migrationBuilder.DropIndex(
                name: "IX_PlateformGame_PlatformId",
                table: "PlateformGame");
        }
    }
}
