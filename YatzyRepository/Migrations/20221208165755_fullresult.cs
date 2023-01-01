using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YatzyRepository.Migrations
{
    public partial class fullresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "twos",
                table: "Scoreboards",
                newName: "Twos");

            migrationBuilder.RenameColumn(
                name: "threes",
                table: "Scoreboards",
                newName: "Threes");

            migrationBuilder.RenameColumn(
                name: "sixes",
                table: "Scoreboards",
                newName: "Sixes");

            migrationBuilder.RenameColumn(
                name: "ones",
                table: "Scoreboards",
                newName: "Ones");

            migrationBuilder.RenameColumn(
                name: "fours",
                table: "Scoreboards",
                newName: "Fours");

            migrationBuilder.RenameColumn(
                name: "fives",
                table: "Scoreboards",
                newName: "Fives");

            migrationBuilder.AlterColumn<int>(
                name: "Twos",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Threes",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sixes",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Ones",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Fours",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Fives",
                table: "Scoreboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Chance",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FourSame",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GreatStraight",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "House",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LittleStraight",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pair",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreeSame",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TwoPairs",
                table: "Scoreboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Yatzy",
                table: "Scoreboards",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chance",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "FourSame",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "GreatStraight",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "House",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "LittleStraight",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "Pair",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "ThreeSame",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "TwoPairs",
                table: "Scoreboards");

            migrationBuilder.DropColumn(
                name: "Yatzy",
                table: "Scoreboards");

            migrationBuilder.RenameColumn(
                name: "Twos",
                table: "Scoreboards",
                newName: "twos");

            migrationBuilder.RenameColumn(
                name: "Threes",
                table: "Scoreboards",
                newName: "threes");

            migrationBuilder.RenameColumn(
                name: "Sixes",
                table: "Scoreboards",
                newName: "sixes");

            migrationBuilder.RenameColumn(
                name: "Ones",
                table: "Scoreboards",
                newName: "ones");

            migrationBuilder.RenameColumn(
                name: "Fours",
                table: "Scoreboards",
                newName: "fours");

            migrationBuilder.RenameColumn(
                name: "Fives",
                table: "Scoreboards",
                newName: "fives");

            migrationBuilder.AlterColumn<int>(
                name: "twos",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "threes",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "sixes",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ones",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fours",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fives",
                table: "Scoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
