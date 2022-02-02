using ConcertTicketAPI.Models.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertTicketAPI.Data.Configs;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasMany(p => p.Events)
            .WithOne(e => e.Person)
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}