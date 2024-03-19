using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bobflix_Backend.Migrations
{
    /// <inheritdoc />
    public partial class StringImdbIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_movies_MoviesImdbId",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_MoviesImdbId",
                table: "UserMovie");

            migrationBuilder.DropColumn(
                name: "MoviesImdbId",
                table: "UserMovie");

            migrationBuilder.AlterColumn<string>(
                name: "imdbId",
                table: "UserMovie",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_imdbId",
                table: "UserMovie",
                column: "imdbId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_movies_imdbId",
                table: "UserMovie",
                column: "imdbId",
                principalTable: "movies",
                principalColumn: "imdbId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_movies_imdbId",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_imdbId",
                table: "UserMovie");

            migrationBuilder.AlterColumn<int>(
                name: "imdbId",
                table: "UserMovie",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "MoviesImdbId",
                table: "UserMovie",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_MoviesImdbId",
                table: "UserMovie",
                column: "MoviesImdbId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_movies_MoviesImdbId",
                table: "UserMovie",
                column: "MoviesImdbId",
                principalTable: "movies",
                principalColumn: "imdbId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
