using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertTicketAPI.Data.Migrations
{
    public partial class AddIsCapitalColumnsToLocationCitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCountryCapital",
                table: "LocationCities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStateCapital",
                table: "LocationCities",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCountryCapital",
                table: "LocationCities");

            migrationBuilder.DropColumn(
                name: "IsStateCapital",
                table: "LocationCities");
        }
    }
}
