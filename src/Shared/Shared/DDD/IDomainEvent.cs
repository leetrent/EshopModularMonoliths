using MediatR;

namespace Shared.DDD
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredIn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
