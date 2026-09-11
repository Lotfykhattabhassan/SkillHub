using System;
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.StudentProfile.Domain.Entities;
namespace SkillHub.Modules.StudentProfile.Infrastructure.Persistence;

public class StudentProfileDbContext : DbContext
{
    public StudentProfileDbContext(DbContextOptions<StudentProfileDbContext> options)
        : base(options)
    {  
    }
    public DbSet<Domain.Entities.StudentProfile> StudentProfiles { get; set; }
    public DbSet<StudentSkill> StudentSkills { get; set; }
    public DbSet<StudentLanguage> StudentLanguages { get; set; }
    public DbSet<StudentSocialLink> StudentSocialLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentProfileDbContext).Assembly);
        
    }
}
