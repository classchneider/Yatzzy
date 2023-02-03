using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatzyRepository.Migrations
{
    public partial class Game5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameEnded",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GameEnded",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
