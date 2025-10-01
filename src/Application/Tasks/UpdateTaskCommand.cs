using Domain.Tasks;
using MediatR;

namespace Application.Tasks;

public sealed record UpdateTaskCommand(
    Guid Id,
    string Title,
    DateTime? DueDate,
    int Priority,
    TaskStatus Status) : IRequest<TaskItemDto?>;
