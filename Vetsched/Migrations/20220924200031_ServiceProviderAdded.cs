using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class ServiceProviderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceUserProfile");

            migrationBuilder.CreateTable(
                name: "services_providers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    created_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: false),
                    modified_when = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services_providers", x => x.id);
                    table.ForeignKey(
                        name: "FK_services_providers_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_services_providers_UserProfile_profile_id",
                        column: x => x.profile_id,
                        principalTable: "UserProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_services_providers_profile_id",
                table: "services_providers",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_services_providers_service_id",
                table: "services_providers",
                column: "service_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "services_providers");

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
        }
    }
}
