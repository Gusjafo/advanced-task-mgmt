using MediatR;

namespace Application.Tasks;

public sealed record CreateTaskCommand(string Title, DateTime? DueDate, int Priority) : IRequest<TaskItemDto>;
