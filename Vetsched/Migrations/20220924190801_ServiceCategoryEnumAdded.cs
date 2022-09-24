using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class ServiceCategoryEnumAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .Annotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default")
                .Annotation("Npgsql:Enum:service_category", "default,doctor")
                .OldAnnotation("Npgsql:Enum:gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default");

            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "services",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "services");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .Annotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default")
                .OldAnnotation("Npgsql:Enum:gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:profile_type", "pet_lover,service_provider,default")
                .OldAnnotation("Npgsql:Enum:service_category", "default,doctor");
        }
    }
}
