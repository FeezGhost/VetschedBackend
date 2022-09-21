using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class ProfileTypeInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "AspNetUsers");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .Annotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default")
                .OldAnnotation("Npgsql:Enum:gender", "male,female,other");

            migrationBuilder.AddColumn<int>(
                name: "ProfileType",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileType",
                table: "AspNetUsers");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
