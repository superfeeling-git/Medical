using Microsoft.EntityFrameworkCore.Migrations;

namespace Medical.EntityFrameworkCore.Migrations
{
    public partial class _08201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParnetId",
                table: "medical_Menu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParnetId",
                table: "medical_Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
