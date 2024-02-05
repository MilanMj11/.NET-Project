using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewApp.Migrations
{
    public partial class Repair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reptuation",
                table: "Profiles",
                newName: "Reputation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reputation",
                table: "Profiles",
                newName: "Reptuation");
        }
    }
}
