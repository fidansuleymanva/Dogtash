using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Persistence.Migrations
{
    public partial class CollectionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Languages_LanguageId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_LanguageId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Collections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_LanguageId",
                table: "Collections",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Languages_LanguageId",
                table: "Collections",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
