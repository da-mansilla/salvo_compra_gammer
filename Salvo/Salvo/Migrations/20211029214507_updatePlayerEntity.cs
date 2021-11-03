using Microsoft.EntityFrameworkCore.Migrations;

namespace Salvo.Migrations
{
    public partial class updatePlayerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Players",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "Nombre");
        }
    }
}
