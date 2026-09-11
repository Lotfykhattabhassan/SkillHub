using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence.Configurations
{
    public class StudentProfileConfiguration : IEntityTypeConfiguration<Domain.Entities.StudentProfile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.StudentProfile> builder)
        {
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.Bio).IsRequired();
            builder.Property(x => x.AcademicYear).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Department).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Faculty).HasMaxLength(50).IsRequired();
            builder.Property(x => x.University).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.GPA).IsRequired();
            builder.Property(x => x.IsPublic).IsRequired();
            builder.Property(x => x.ProfileImageUrl).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();

        }
    
    }
}
