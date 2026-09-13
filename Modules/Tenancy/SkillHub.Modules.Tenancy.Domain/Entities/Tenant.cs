using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Domain.Entities
{
    public class Tenant : BaseEntity<int>
    {
        public string Name { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public TenantStatus Status { get; private set; }

        private Tenant() { }
        private Tenant( string name, string slug)
        {
            Rename(name);
            ChangeSlug(slug);

            Status = TenantStatus.Pending;
        }
        public static Tenant Create(
            string name,
            string slug
            )
        {
            return new Tenant(name, slug);
        }
        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.All(char.IsDigit))
                throw new ArgumentException("Name is required and not containing only digits");

            Name = name;
        }
        public void Activate()
        {
            if (Status == TenantStatus.Active)
                return;

            Status = TenantStatus.Active;
        }

        public void Suspend()
        {
            if (Status == TenantStatus.Suspended)
                return;

            Status = TenantStatus.Suspended;
        }
        public void Deactivate()
        {
            if (Status == TenantStatus.Deactivated)
                return;

            Status = TenantStatus.Deactivated;
        }

        public void ChangeSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required");

            Slug = slug;
        }
    }
}
