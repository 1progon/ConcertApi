using ConcertTicketAPI.Models.Event;
using ConcertTicketAPI.Models.Middle;
using ConcertTicketAPI.Models.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertTicketAPI.Data.Configs;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .HasMany(e => e.PersonFavorites)
            .WithMany(p => p.EventFavorites)
            .UsingEntity<PersonEventFavoritesMiddle>(
                j => j
                    .HasOne<Person>(pef => pef.Person)
                    .WithMany(p => p.PersonEventFavorites)
                    .HasForeignKey(pev => pev.PersonId),
                j => j
                    .HasOne<Event>(pef => pef.Event)
                    .WithMany(e => e.PersonEventFavorites)
                    .HasForeignKey(pev => pev.EventId),
                j => j
                    .HasKey(pev => new {pev.EventId, pev.PersonId}));


        builder
            .HasMany(e => e.PersonFollowers)
            .WithMany(p => p.EventFollowings)
            .UsingEntity<PersonEventFollowingsMiddle>(
                j => j
                    .HasOne<Person>(pef => pef.Person)
                    .WithMany(p => p.PersonEventFollowings)
                    .HasForeignKey(pev => pev.PersonId),
                j => j
                    .HasOne<Event>(pef => pef.Event)
                    .WithMany(e => e.PersonEventFollowings)
                    .HasForeignKey(pev => pev.EventId),
                j => j
                    .HasKey(f => new {f.EventId, f.PersonId}));

        builder
            .HasMany(e => e.Tags)
            .WithMany(t => t.Events)
            .UsingEntity<EventTagMiddle>(
                j => j
                    .HasOne<EventsTag>(etm => etm.Tag)
                    .WithMany(t => t.EventTagMiddles)
                    .HasForeignKey(etm => etm.TagId),
                j => j
                    .HasOne<Event>(etm => etm.Event)
                    .WithMany(t => t.EventTagMiddles)
                    .HasForeignKey(etm => etm.EventId),
                j => j
                    .HasKey(etm => new {etm.EventId, etm.TagId}));


        builder
            .HasMany(e => e.Likes)
            .WithOne(l => l.Event)
            .HasForeignKey(el => el.EventId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}