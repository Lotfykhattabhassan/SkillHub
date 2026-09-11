using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Configurations;

public class StudentSkillConfiguration : IEntityTypeConfiguration<Domain.Entities.StudentSkill>
{
    
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.StudentSkill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SkillName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ProficiencyLevel).IsRequired();
        builder.HasOne(x => x.StudentProfile)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.StudentProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
