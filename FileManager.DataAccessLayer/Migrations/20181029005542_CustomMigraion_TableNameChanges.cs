using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.DataAccessLayer.Migrations
{
    public partial class CustomMigraion_TableNameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Episodes", newName: "Episode");
            migrationBuilder.RenameTable(name: "Movies", newName: "Movie");
            migrationBuilder.RenameTable(name: "Seasons", newName: "Season");
            migrationBuilder.RenameTable(name: "Shows", newName: "Show");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Shows", newName: "Show");
            migrationBuilder.RenameTable(name: "Seasons", newName: "Season");
            migrationBuilder.RenameTable(name: "Movies", newName: "Movie");
            migrationBuilder.RenameTable(name: "Episode", newName: "Episodes");
        }
    }
}
