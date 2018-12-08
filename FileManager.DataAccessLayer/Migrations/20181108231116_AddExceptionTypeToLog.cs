using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.DataAccessLayer.Migrations
{
    public partial class AddExceptionTypeToLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionType",
                table: "Log",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionType",
                table: "Log");
        }
    }
}
