using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class ProfileBasicExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveAccount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimeZoneInfo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPet",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    PetLoverId = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.id);
                    table.ForeignKey(
                        name: "FK_pets_AspNetUsers_PetLoverId",
                        column: x => x.PetLoverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pets_PetLoverId",
                table: "pets",
                column: "PetLoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropColumn(
                name: "NumberOfPet",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveAccount",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneInfo",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
