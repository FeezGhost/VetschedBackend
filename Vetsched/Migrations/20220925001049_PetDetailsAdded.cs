using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vetsched.Migrations
{
    public partial class PetDetailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<JsonElement>(
                name: "Details",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Vaccination",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "age",
                table: "pets",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<JsonElement>(
                name: "allergies",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "breed",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "due_vaccine",
                table: "pets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_vist_description",
                table: "pets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<JsonElement>(
                name: "medications",
                table: "pets",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "microchiped",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sepcies",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "sex",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "title",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "vaccine_recieved",
                table: "pets",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "Vaccination",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "age",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "allergies",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "breed",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "due_vaccine",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "last_vist_description",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "medications",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "microchiped",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "sepcies",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "title",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "vaccine_recieved",
                table: "pets");
        }
    }
}
