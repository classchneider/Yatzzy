using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatzyRepository.Migrations
{
    public partial class Game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Scoreboards_ResultId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ResultId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ScoreboardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Scoreboards_ScoreboardId",
                        column: x => x.ScoreboardId,
                        principalTable: "Scoreboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ScoreboardId",
                table: "Games",
                column: "ScoreboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ResultId",
                table: "Players",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Scoreboards_ResultId",
                table: "Players",
                column: "ResultId",
                principalTable: "Scoreboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
