#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Store.Persistence.Data.Migrations.StoreDb;

public partial class AddRefreshTokenToUser : Migration
{

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "RefreshTokens",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Token = table.Column<string>("character varying(255)", maxLength: 255, nullable: false),
                ExpiryOn = table.Column<DateTime>("timestamp with time zone", nullable: false),
                CreatedOn = table.Column<DateTime>("timestamp with time zone", nullable: false),
                CreatedByIp = table.Column<string>("character varying(63)", maxLength: 63, nullable: false),
                RevokedOn = table.Column<DateTime>("timestamp with time zone", nullable: true),
                RevokedByIp = table.Column<string>("character varying(63)", maxLength: 63, nullable: true),
                UserId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshTokens", x => x.Id);

                table.ForeignKey(
                    "FK_RefreshTokens_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_RefreshTokens_Token",
            "RefreshTokens",
            "Token",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_RefreshTokens_UserId",
            "RefreshTokens",
            "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "RefreshTokens");
    }

}