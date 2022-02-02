using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertTicketAPI.Data.Migrations
{
    public partial class AddTokenAndExpireColumnsToPersonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Persons",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpire",
                table: "Persons",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TokenExpire",
                table: "Persons");
        }
    }
}
