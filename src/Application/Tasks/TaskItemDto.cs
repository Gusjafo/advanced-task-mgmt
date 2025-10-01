using Domain.Tasks;

namespace Application.Tasks;

public sealed record TaskItemDto(
    Guid Id,
    string Title,
    DateTime CreatedAt,
    DateTime? DueDate,
    int Priority,
    TaskStatus Status)
{
    public static TaskItemDto FromEntity(TaskItem task) =>
        new(task.Id, task.Title, task.CreatedAt, task.DueDate, task.Priority, task.Status);
}
