using System.Reflection.Metadata.Ecma335;
using ConcertTicketAPI.Data.Configs;
using ConcertTicketAPI.Models.Event;
using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Performer;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Models.Phone;
using ConcertTicketAPI.Models.Venue;
using Microsoft.EntityFrameworkCore;
using TimeZone = ConcertTicketAPI.Models.Location.TimeZone;

namespace ConcertTicketAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Events Likes Configure
        builder.Entity<EventsLike>()
            .HasKey(l => new {l.EventId, l.PersonId});

        builder.Entity<LocationCity>()
            .HasAlternateKey(c => new {c.StateId, c.Slug});

        builder.Entity<LocationCity>()
            .HasIndex(c => c.Slug)
            .IsUnique(false);

        builder.ApplyConfiguration(new EventConfiguration());
        builder.ApplyConfiguration(new PersonConfiguration());
        builder.ApplyConfiguration(new PerformerConfiguration());
        builder.ApplyConfiguration(new VenueConfiguration());
    }

    // Locations
    public DbSet<LocationCountry> LocationCountries { get; set; } = null!;
    public DbSet<LocationState> LocationStates { get; set; } = null!;
    public DbSet<LocationCity> LocationCities { get; set; } = null!;
    public DbSet<TimeZone> TimeZones { get; set; } = null!;


    // Performers
    public DbSet<PerformerCategory> PerformerCategories { get; set; } = null!;
    public DbSet<PerformerSubCategory> PerformerSubCategories { get; set; } = null!;
    public DbSet<Performer> Performers { get; set; } = null!;
    public DbSet<PerformerImages> PerformerImages { get; set; } = null!;


    // Venues
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<VenueImages> VenueImages { get; set; } = null!;
    public DbSet<VenueWorkTime> VenueWorkTimes { get; set; } = null!;

    // Events
    public DbSet<EventCategory> EventCategories { get; set; } = null!;
    public DbSet<EventSubCategory> EventSubCategories { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventsLike> EventLikes { get; set; } = null!;
    public DbSet<EventsTag> EventsTags { get; set; } = null!;
    public DbSet<EventImages> EventImages { get; set; } = null!;
    public DbSet<VenueTicket> Tickets { get; set; } = null!;
    public DbSet<VenueParking> Parking { get; set; } = null!;


    // User
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<PersonCompanies> PersonCompanies { get; set; } = null!;
    public DbSet<PersonCompaniesImages> PersonCompaniesImages { get; set; } = null!;

    // Phone
    public DbSet<CountryPhoneCode> PhoneCodesCountries { get; set; } = null!;
}