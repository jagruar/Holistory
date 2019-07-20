using MediatR;
using System;
using System.Collections.Generic;

namespace Holistory.Domain.Seedwork
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        private List<INotification> _domainEvents = new List<INotification>();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// The identity value for the entity.
        /// </summary>
        public virtual int Id { get; protected set; }

        public DateTime? UtcDateDeleted { get; protected set; }

        /// <summary>
        /// Call this method once to delete an entity. if it is already deleted nothing happens.
        /// </summary>
        /// <param name="utcDateDeleted">The datetime the entity was deleted.</param>
        public void SetDeleted(DateTime utcDateDeleted)
        {
            // Do nothing if already deleted
            if (UtcDateDeleted == null)
            {
                UtcDateDeleted = utcDateDeleted;
            }
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public bool IsTransient()
        {
            return Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                }

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (object.Equals(left, null))
            {
                return object.Equals(right, null);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return left != right;
        }
    }
}