using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Infrastructure.DAL.Configurations;

internal sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GroupId(x));
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AdminId);
        builder.Property(x => x.AdminId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new GroupName(x))
            .IsRequired()
            .HasMaxLength(64);
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new GroupDescription(x))
            .HasMaxLength(255);
        builder.Property(x => x.GroupType)
            .IsRequired();
        builder.HasMany(x => x.Members)
            .WithMany();
    }
}