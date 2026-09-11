using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Configurations;

public class StudentLanguageConfiguration : IEntityTypeConfiguration<Domain.Entities.StudentLanguage>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.StudentLanguage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LanguageName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ProficiencyLevel).IsRequired();
        builder.HasOne(x => x.StudentProfile)
            .WithMany(x => x.Languages)
            .HasForeignKey(x => x.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
