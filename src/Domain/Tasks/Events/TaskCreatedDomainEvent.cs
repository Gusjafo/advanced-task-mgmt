using Domain.Common;

namespace Domain.Tasks.Events;

public sealed class TaskCreatedDomainEvent(Guid taskId) : IDomainEvent
{
    public Guid TaskId { get; } = taskId;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
