using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertTicketAPI.Data.Migrations
{
    public partial class RemoveCityColumnFromCountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationCities_LocationCountries_CountryId",
                table: "LocationCities");

            migrationBuilder.DropIndex(
                name: "IX_LocationCities_CountryId",
                table: "LocationCities");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "LocationCities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "LocationCities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LocationCities_CountryId",
                table: "LocationCities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationCities_LocationCountries_CountryId",
                table: "LocationCities",
                column: "CountryId",
                principalTable: "LocationCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
