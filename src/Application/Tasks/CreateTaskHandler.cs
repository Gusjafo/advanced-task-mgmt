using Application.Common;
using Domain.Tasks;
using MediatR;

namespace Application.Tasks;

public sealed class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
{
    private readonly IAppDbContext _context;

    public CreateTaskHandler(IAppDbContext context) => _context = context;

    public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = TaskItem.Create(request.Title, request.DueDate, request.Priority);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return TaskItemDto.FromEntity(task);
    }
}
