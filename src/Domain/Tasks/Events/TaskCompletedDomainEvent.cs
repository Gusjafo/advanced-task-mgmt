using Domain.Common;

namespace Domain.Tasks.Events;

public sealed class TaskCompletedDomainEvent : IDomainEvent
{
    public Guid TaskId { get; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public TaskCompletedDomainEvent(Guid taskId) => TaskId = taskId;
}
