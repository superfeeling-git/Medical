using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medical.EntityFrameworkCore.Migrations
{
    public partial class _08202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParnetId",
                table: "medical_Menu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParnetId",
                table: "medical_Menu");
        }
    }
}
