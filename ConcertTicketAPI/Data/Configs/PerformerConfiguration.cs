using ConcertTicketAPI.Models.Event;
using ConcertTicketAPI.Models.Middle;
using ConcertTicketAPI.Models.Performer;
using ConcertTicketAPI.Models.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertTicketAPI.Data.Configs;

public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
{
    public void Configure(EntityTypeBuilder<Performer> builder)
    {
        builder
            .HasMany(p => p.Events)
            .WithMany(e => e.Performers)
            .UsingEntity<EventPerformerMiddle>(
                j => j
                    .HasOne<Event>(ep => ep.Event)
                    .WithMany(e => e.EventPerformers)
                    .HasForeignKey(ep => ep.EventId),
                j => j
                    .HasOne<Performer>(ep => ep.Performer)
                    .WithMany(p => p.EventPerformers)
                    .HasForeignKey(ep => ep.PerformerId),
                j => j
                    .HasKey(ep => new {ep.EventId, ep.PerformerId})
            );

        builder
            .HasMany<Person>(p => p.PersonFavorites)
            .WithMany(p => p.PerformerFavorites)
            .UsingEntity<PersonPerformerFavoritesMiddle>(
                j => j
                    .HasOne<Person>(ppf => ppf.Person)
                    .WithMany(p => p.PersonPerformerFavorites)
                    .HasForeignKey(ppf => ppf.PersonId),
                j => j
                    .HasOne<Performer>(ppf => ppf.Performer)
                    .WithMany(p => p.PersonPerformerFavorites)
                    .HasForeignKey(ppf => ppf.PerformerId),
                j => j
                    .HasKey(ppf => new {ppf.PersonId, ppf.PerformerId})
            );

        builder
            .HasMany<Person>(p => p.PersonFollowers)
            .WithMany(p => p.PerformerFollowings)
            .UsingEntity<PersonPerformerFollowingMiddle>(
                j => j
                    .HasOne<Person>(ppf => ppf.Person)
                    .WithMany(p => p.PersonPerformerFollowings)
                    .HasForeignKey(ppf => ppf.PersonId),
                j => j
                    .HasOne<Performer>(ppf => ppf.Performer)
                    .WithMany(p => p.PersonPerformerFollowings)
                    .HasForeignKey(ppf => ppf.PerformerId),
                j => j
                    .HasKey(ppf => new {ppf.PersonId, ppf.PerformerId})
            );
    }
}