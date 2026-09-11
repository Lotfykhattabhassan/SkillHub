using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Configurations;

public class StudentSocialLinkConfiguration : IEntityTypeConfiguration<Domain.Entities.StudentSocialLink>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.StudentSocialLink> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StudentProfileId).IsRequired();
        builder.Property(x => x.Platform).IsRequired();
        builder.Property(x => x.Url).HasMaxLength(150).IsRequired();

        builder.HasOne(x => x.StudentProfile)
            .WithMany(x => x.SocialLinks)
            .HasForeignKey(x => x.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
