using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Configurations;

public class StudentLanguageConfiguration : IEntityTypeConfiguration<Domain.Entities.StudentLanguage>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.StudentLanguage> builder)
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
