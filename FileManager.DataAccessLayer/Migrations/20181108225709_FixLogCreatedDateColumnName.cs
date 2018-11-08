using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.DataAccessLayer.Migrations
{
    public partial class FixLogCreatedDateColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Log",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Log",
                newName: "CreateDate");
        }
    }
}
