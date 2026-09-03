using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.BuildingBlocks.Domain.Common.Events;

namespace SkillHub.BuildingBlocks.Domain.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; private set; } = default!;

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
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

        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

}
