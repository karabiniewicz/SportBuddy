using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Infrastructure.DAL.Configurations;

internal sealed class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new MatchId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new MatchName(x))
            .IsRequired()
            .HasMaxLength(64);
        builder.Property(x => x.Location)
            .HasConversion(x => x.Value, x => new Location(x))
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Charge)
            .HasConversion(x => x.Value, x => new Charge(x))
            .IsRequired();
        builder.Property(x => x.Limit)
            .HasConversion(x => x.Value, x => new MatchLimit(x))
            .IsRequired();
        builder.Property(x => x.GroupId)
            .HasConversion(x => x.Value, x => new GroupId(x))
            .IsRequired();
        builder.HasMany(x => x.Members)
            .WithMany();
    }
}