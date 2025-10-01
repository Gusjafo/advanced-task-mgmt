using MediatR;

namespace Application.Tasks;

public sealed record GetTaskByIdQuery(Guid Id) : IRequest<TaskItemDto?>;
