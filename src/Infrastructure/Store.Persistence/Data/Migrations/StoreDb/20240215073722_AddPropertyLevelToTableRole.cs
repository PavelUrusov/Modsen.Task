using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Persistence.Data.Migrations.StoreDb
{
    public partial class AddPropertyLevelToTableRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Roles");
        }
    }
}
