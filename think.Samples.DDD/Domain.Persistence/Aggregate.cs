using System;
using System.Collections.Generic;
using Domain.Persistence.Events;

namespace Domain.Persistence
{
    public abstract class Aggregate
    {
        public Aggregate()
        {
            RegisterHandlers();
        }
        
        // For indexing our event streams
        public Guid Id { get; protected set; }
        
        // For protecting the state, i.e. conflict prevention
        public int Version { get; private set; }

        private readonly List<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();
        private readonly Dictionary<Type, Action<IDomainEvent>> _handlers = new Dictionary<Type, Action<IDomainEvent>>();
    
        // Get the deltas, i.e. events that make up the state, not yet persisted
        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        // Mark the deltas as persisted.
        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();            
        }

        // Infrastructure for raising events & registering handlers
        protected abstract void RegisterHandlers();
        
        protected void Handle<T>(Action<T> handle)
        {
            _handlers[typeof(T)] = e => handle((T)e);
        } 

        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            ApplyEvent(domainEvent);
            _uncommittedEvents.Add(domainEvent);
        }

        private void ApplyEvent(IDomainEvent domainEvent)
        {
            _handlers[domainEvent.GetType()](domainEvent);
            
            // Each event bumps our version
            Version++;
        }    
    }
}