using Application.Common;
using Domain.Tasks;
using Domain.Tasks.Events;
using MediatR;

namespace Application.Tasks;

public sealed class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IPublisher _publisher;

    public CreateTaskHandler(IAppDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = TaskItem.Create(request.Title, request.DueDate, request.Priority);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new TaskCreatedDomainEvent(task.Id), cancellationToken);

        return task.Id;
    }
}
