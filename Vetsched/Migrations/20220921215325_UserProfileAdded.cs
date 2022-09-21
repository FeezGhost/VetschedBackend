using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class UserProfileAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pets_AspNetUsers_PetLoverId",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfPet",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    NumberOfPet = table.Column<int>(type: "integer", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserProfile_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceUserProfile",
                columns: table => new
                {
                    ProvidersId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUserProfile", x => new { x.ProvidersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ServiceUserProfile_services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUserProfile_UserProfile_ProvidersId",
                        column: x => x.ProvidersId,
                        principalTable: "UserProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserProfile_ServicesId",
                table: "ServiceUserProfile",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ApplicationUserId",
                table: "UserProfile",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_pets_UserProfile_PetLoverId",
                table: "pets",
                column: "PetLoverId",
                principalTable: "UserProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pets_UserProfile_PetLoverId",
                table: "pets");

            migrationBuilder.DropTable(
                name: "ServiceUserProfile");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPet",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_pets_AspNetUsers_PetLoverId",
                table: "pets",
                column: "PetLoverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
