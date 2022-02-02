using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace ConcertTicketAPI.Data.Migrations
{
    public partial class AddInitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    InHeader = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true),
                    Icon = table.Column<string>(type: "varchar(255)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Iso2Code = table.Column<string>(type: "char(2)", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    Popular = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformerCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    InHeader = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSubCategories_EventCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Abbreviation = table.Column<string>(type: "varchar(6)", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationStates_LocationCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    Comments = table.Column<string>(type: "varchar(255)", nullable: true),
                    UtcOffset = table.Column<string>(type: "varchar(10)", nullable: false),
                    UtcDstOffset = table.Column<string>(type: "varchar(10)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeZones_LocationCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformerSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformerSubCategories_PerformerCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PerformerCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Iata = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    Popular = table.Column<bool>(type: "boolean", nullable: false),
                    TimeZoneId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCities", x => x.Id);
                    table.UniqueConstraint("AK_LocationCities_StateId_Slug", x => new { x.StateId, x.Slug });
                    table.ForeignKey(
                        name: "FK_LocationCities_LocationCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationCities_LocationStates_StateId",
                        column: x => x.StateId,
                        principalTable: "LocationStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationCities_TimeZones_TimeZoneId",
                        column: x => x.TimeZoneId,
                        principalTable: "TimeZones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Article = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Popular = table.Column<bool>(type: "boolean", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Likes = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performers_Performers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Performers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Performers_PerformerSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "PerformerSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "varchar(255)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Photo = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(255)", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    About = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: true),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(5)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    HouseLetter = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_LocationCities_CityId",
                        column: x => x.CityId,
                        principalTable: "LocationCities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_LocationCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_LocationStates_StateId",
                        column: x => x.StateId,
                        principalTable: "LocationStates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerformerImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Filename = table.Column<string>(type: "varchar(255)", nullable: false),
                    Folder = table.Column<string>(type: "varchar(255)", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    PerformerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformerImages_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Type = table.Column<string>(type: "varchar(255)", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Likes = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCompanies_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPerformerFavoritesMiddle",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPerformerFavoritesMiddle", x => new { x.PersonId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_PersonPerformerFavoritesMiddle_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPerformerFavoritesMiddle_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPerformerFollowingMiddle",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPerformerFollowingMiddle", x => new { x.PersonId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_PersonPerformerFollowingMiddle_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPerformerFollowingMiddle_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompaniesImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Filename = table.Column<string>(type: "varchar(255)", nullable: false),
                    Folder = table.Column<string>(type: "varchar(255)", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    PersonCompaniesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCompaniesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCompaniesImages_PersonCompanies_PersonCompaniesId",
                        column: x => x.PersonCompaniesId,
                        principalTable: "PersonCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    PersonOwnerId = table.Column<int>(type: "integer", nullable: true),
                    CompanyOwnerId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Article = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(5)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    HouseLetter = table.Column<string>(type: "text", nullable: true),
                    Coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    Capacity = table.Column<string>(type: "text", nullable: true),
                    SquareSize = table.Column<float>(type: "real", nullable: true),
                    NearWater = table.Column<bool>(type: "boolean", nullable: false),
                    Opened = table.Column<DateOnly>(type: "date", nullable: true),
                    Phone1 = table.Column<string>(type: "text", nullable: true),
                    Phone2 = table.Column<string>(type: "text", nullable: true),
                    Phone3 = table.Column<string>(type: "text", nullable: true),
                    OtherPhones = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venues_LocationCities_CityId",
                        column: x => x.CityId,
                        principalTable: "LocationCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venues_LocationCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venues_LocationStates_StateId",
                        column: x => x.StateId,
                        principalTable: "LocationStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Venues_PersonCompanies_CompanyOwnerId",
                        column: x => x.CompanyOwnerId,
                        principalTable: "PersonCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Venues_Persons_PersonOwnerId",
                        column: x => x.PersonOwnerId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    VenueId = table.Column<int>(type: "integer", nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false),
                    LikesCount = table.Column<long>(type: "bigint", nullable: true),
                    LocationCityId = table.Column<int>(type: "integer", nullable: true),
                    LocationCountryId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "EventSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_LocationCities_LocationCityId",
                        column: x => x.LocationCityId,
                        principalTable: "LocationCities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_LocationCountries_LocationCountryId",
                        column: x => x.LocationCountryId,
                        principalTable: "LocationCountries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_PersonCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "PersonCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    Folder = table.Column<string>(type: "text", nullable: false),
                    VenueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueImages_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueWorkTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeekDay = table.Column<int>(type: "integer", nullable: false),
                    LunchBreak = table.Column<TimeSpan>(type: "interval", nullable: false),
                    WorkTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    VenueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueWorkTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueWorkTimes_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VenueId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    ZoneId = table.Column<int>(type: "integer", nullable: true),
                    SeatsCount = table.Column<int>(type: "integer", nullable: false),
                    TicketsCount = table.Column<int>(type: "integer", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueZones_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Filename = table.Column<string>(type: "varchar(255)", nullable: false),
                    Folder = table.Column<string>(type: "varchar(255)", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLikes",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLikes", x => new { x.EventId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_EventLikes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLikes_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventPerformerMiddle",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    PerformerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerformerMiddle", x => new { x.EventId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_EventPerformerMiddle_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPerformerMiddle_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTagMiddle",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTagMiddle", x => new { x.EventId, x.TagId });
                    table.ForeignKey(
                        name: "FK_EventTagMiddle_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTagMiddle_EventsTags_TagId",
                        column: x => x.TagId,
                        principalTable: "EventsTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    VenueId = table.Column<int>(type: "integer", nullable: false),
                    IsVip = table.Column<bool>(type: "boolean", nullable: false),
                    IsRefundable = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: true),
                    PerformerId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parking_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parking_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parking_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEventFavoritesMiddle",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEventFavoritesMiddle", x => new { x.EventId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonEventFavoritesMiddle_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEventFavoritesMiddle_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEventFollowingsMiddle",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEventFollowingsMiddle", x => new { x.EventId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonEventFollowingsMiddle_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEventFollowingsMiddle_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VenueZonesId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    IsVip = table.Column<bool>(type: "boolean", nullable: false),
                    IsRefundable = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Seat = table.Column<string>(type: "varchar(255)", nullable: true),
                    PerformerId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_VenueZones_VenueZonesId",
                        column: x => x.VenueZonesId,
                        principalTable: "VenueZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_Slug",
                table: "EventCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLikes_PersonId",
                table: "EventLikes",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPerformerMiddle_PerformerId",
                table: "EventPerformerMiddle",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CompanyId",
                table: "Events",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationCityId",
                table: "Events",
                column: "LocationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationCountryId",
                table: "Events",
                column: "LocationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PersonId",
                table: "Events",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Slug",
                table: "Events",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_SubCategoryId",
                table: "Events",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsTags_Slug",
                table: "EventsTags",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSubCategories_CategoryId",
                table: "EventSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubCategories_Slug",
                table: "EventSubCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventTagMiddle_TagId",
                table: "EventTagMiddle",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCities_CountryId",
                table: "LocationCities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCities_Slug",
                table: "LocationCities",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCities_TimeZoneId",
                table: "LocationCities",
                column: "TimeZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCountries_Slug",
                table: "LocationCountries",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationStates_CountryId",
                table: "LocationStates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationStates_Slug",
                table: "LocationStates",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parking_EventId",
                table: "Parking",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_PerformerId",
                table: "Parking",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_Slug",
                table: "Parking",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parking_VenueId",
                table: "Parking",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerCategories_Slug",
                table: "PerformerCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformerImages_PerformerId",
                table: "PerformerImages",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_ParentId",
                table: "Performers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_Slug",
                table: "Performers",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performers_SubCategoryId",
                table: "Performers",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerSubCategories_CategoryId",
                table: "PerformerSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformerSubCategories_Slug",
                table: "PerformerSubCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanies_PersonId",
                table: "PersonCompanies",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompaniesImages_PersonCompaniesId",
                table: "PersonCompaniesImages",
                column: "PersonCompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEventFavoritesMiddle_PersonId",
                table: "PersonEventFavoritesMiddle",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEventFollowingsMiddle_PersonId",
                table: "PersonEventFollowingsMiddle",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPerformerFavoritesMiddle_PerformerId",
                table: "PersonPerformerFavoritesMiddle",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPerformerFollowingMiddle_PerformerId",
                table: "PersonPerformerFollowingMiddle",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Phone",
                table: "Persons",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_StateId",
                table: "Persons",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PerformerId",
                table: "Tickets",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Slug",
                table: "Tickets",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VenueZonesId",
                table: "Tickets",
                column: "VenueZonesId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeZones_CountryId",
                table: "TimeZones",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeZones_Slug",
                table: "TimeZones",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VenueImages_VenueId",
                table: "VenueImages",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_CityId",
                table: "Venues",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_CompanyOwnerId",
                table: "Venues",
                column: "CompanyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_CountryId",
                table: "Venues",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_PersonOwnerId",
                table: "Venues",
                column: "PersonOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_Slug",
                table: "Venues",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_StateId",
                table: "Venues",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueWorkTimes_VenueId",
                table: "VenueWorkTimes",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueZones_VenueId",
                table: "VenueZones",
                column: "VenueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventImages");

            migrationBuilder.DropTable(
                name: "EventLikes");

            migrationBuilder.DropTable(
                name: "EventPerformerMiddle");

            migrationBuilder.DropTable(
                name: "EventTagMiddle");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "PerformerImages");

            migrationBuilder.DropTable(
                name: "PersonCompaniesImages");

            migrationBuilder.DropTable(
                name: "PersonEventFavoritesMiddle");

            migrationBuilder.DropTable(
                name: "PersonEventFollowingsMiddle");

            migrationBuilder.DropTable(
                name: "PersonPerformerFavoritesMiddle");

            migrationBuilder.DropTable(
                name: "PersonPerformerFollowingMiddle");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "VenueImages");

            migrationBuilder.DropTable(
                name: "VenueWorkTimes");

            migrationBuilder.DropTable(
                name: "EventsTags");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "VenueZones");

            migrationBuilder.DropTable(
                name: "EventSubCategories");

            migrationBuilder.DropTable(
                name: "PerformerSubCategories");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "PerformerCategories");

            migrationBuilder.DropTable(
                name: "PersonCompanies");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "LocationCities");

            migrationBuilder.DropTable(
                name: "LocationStates");

            migrationBuilder.DropTable(
                name: "TimeZones");

            migrationBuilder.DropTable(
                name: "LocationCountries");
        }
    }
}
