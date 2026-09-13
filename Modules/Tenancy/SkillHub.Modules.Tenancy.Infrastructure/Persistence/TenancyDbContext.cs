
using Microsoft.EntityFrameworkCore;
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Infrastructure.Persistence
{
    public class TenancyDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantInvitation> Invitations { get; set; }
        public DbSet<TenantMembership> Memberships { get; set; }
        public TenancyDbContext(DbContextOptions<TenancyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenancyDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
