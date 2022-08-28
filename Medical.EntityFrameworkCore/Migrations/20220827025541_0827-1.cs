using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medical.EntityFrameworkCore.Migrations
{
    public partial class _08271 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BedNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_Room_medical_Menu_RegionId",
                        column: x => x.RegionId,
                        principalTable: "medical_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medical_Room_RegionId",
                table: "medical_Room",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medical_Room");
        }
    }
}
