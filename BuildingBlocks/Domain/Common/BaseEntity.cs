using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.BuildingBlocks.Domain.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; private set; } = default!;

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }
        protected BaseEntity() { }
        protected BaseEntity(TId id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
        public void MarkAsDeleted()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
        public void Restore()
        {
            if (!IsDeleted)
                return;

            IsDeleted = false;
            DeletedAt = null;
        }
        public void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
