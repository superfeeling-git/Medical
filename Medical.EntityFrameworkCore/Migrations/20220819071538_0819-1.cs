using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medical.EntityFrameworkCore.Migrations
{
    public partial class _08191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MenuPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MenuNameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ComponentPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    ParnetId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_Menu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medical_Menu");
        }
    }
}
