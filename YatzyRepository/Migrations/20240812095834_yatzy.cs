using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatzyRepository.Migrations
{
    public partial class yatzy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextPlayerScoreIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scoreboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ones = table.Column<int>(type: "int", nullable: true),
                    Twos = table.Column<int>(type: "int", nullable: true),
                    Threes = table.Column<int>(type: "int", nullable: true),
                    Fours = table.Column<int>(type: "int", nullable: true),
                    Fives = table.Column<int>(type: "int", nullable: true),
                    Sixes = table.Column<int>(type: "int", nullable: true),
                    Pair = table.Column<int>(type: "int", nullable: true),
                    TwoPairs = table.Column<int>(type: "int", nullable: true),
                    ThreeSame = table.Column<int>(type: "int", nullable: true),
                    FourSame = table.Column<int>(type: "int", nullable: true),
                    LittleStraight = table.Column<int>(type: "int", nullable: true),
                    GreatStraight = table.Column<int>(type: "int", nullable: true),
                    House = table.Column<int>(type: "int", nullable: true),
                    Chance = table.Column<int>(type: "int", nullable: true),
                    Yatzy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoreboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ScoreboardId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerScores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerScores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerScores_Scoreboards_ScoreboardId",
                        column: x => x.ScoreboardId,
                        principalTable: "Scoreboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_GameId",
                table: "PlayerScores",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_PlayerId",
                table: "PlayerScores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_ScoreboardId",
                table: "PlayerScores",
                column: "ScoreboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerScores");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Scoreboards");
        }
    }
}
