#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Persistence.Data.Migrations.StoreDb;

public partial class AddIndexAndMakeNameUniqueInProductTable : Migration
{

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            "IX_Products_Name",
            "Products",
            "Name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            "IX_Products_Name",
            "Products");
    }

}