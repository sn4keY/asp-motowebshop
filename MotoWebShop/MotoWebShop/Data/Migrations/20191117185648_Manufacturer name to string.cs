using Microsoft.EntityFrameworkCore.Migrations;

namespace MotoWebShop.Data.Migrations
{
    public partial class Manufacturernametostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Manufacturers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Manufacturers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
