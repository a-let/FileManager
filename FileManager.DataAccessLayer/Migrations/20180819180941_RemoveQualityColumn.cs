using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.DataAccessLayer.Migrations
{
    public partial class RemoveQualityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "Movies",
                nullable: true);
        }
    }
}
