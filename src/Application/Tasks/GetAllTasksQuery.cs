using MediatR;

namespace Application.Tasks;

public sealed record GetAllTasksQuery : IRequest<IReadOnlyCollection<TaskItemDto>>;
