using Domain.Common;
using Domain.Tasks.Events;

namespace Domain.Tasks;

public sealed class TaskItem : IHasDomainEvents
{
    private TaskItem() { } // EF

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; private set; }
    public int Priority { get; private set; }
    public TaskStatus Status { get; private set; } = TaskStatus.New;

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public static TaskItem Create(string title, DateTime? dueDate, int priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));
        if (priority < 0) throw new ArgumentOutOfRangeException(nameof(priority));

        var task = new TaskItem
        {
            Title = title.Trim(),
            DueDate = dueDate,
            Priority = priority
        };

        task.AddDomainEvent(new TaskCreatedDomainEvent(task.Id));
        return task;
    }

    public void MarkAsDone()
    {
        if (Status == TaskStatus.Done) return;                // idempotente
        if (Status == TaskStatus.Archived)
            throw new InvalidOperationException("Archived tasks cannot change status.");

        Status = TaskStatus.Done;
        AddDomainEvent(new TaskCompletedDomainEvent(Id));
    }

    private void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
