using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Persistence.Migrations
{
    public partial class SubCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "MenuSliders");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "MenuSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "MenuSliders");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "MenuSliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
