using MediatR;

namespace Application.Tasks;

public sealed record DeleteTaskCommand(Guid Id) : IRequest<bool>;
