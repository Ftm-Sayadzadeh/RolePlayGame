using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rpg_game.Migrations
{
    /// <inheritdoc />
    public partial class UserCharacterRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_userId",
                table: "Characters",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_userId",
                table: "Characters",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_userId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_userId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Characters");
        }
    }
}
