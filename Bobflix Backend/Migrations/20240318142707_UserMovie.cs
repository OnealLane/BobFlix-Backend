using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bobflix_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UserMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserMovie",
                columns: table => new
                {
                    MoviesImdbId = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMovie", x => new { x.MoviesImdbId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_movies_MoviesImdbId",
                        column: x => x.MoviesImdbId,
                        principalTable: "movies",
                        principalColumn: "imdbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMovie_UsersId",
                table: "ApplicationUserMovie",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMovie");
        }
    }
}
