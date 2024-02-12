#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Data.Migrations.StoreDb;

public partial class AddIndexAndMakeNameUniqueInCategoryTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            "IX_Categories_Name",
            "Categories",
            "Name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            "IX_Categories_Name",
            "Categories");
    }
}